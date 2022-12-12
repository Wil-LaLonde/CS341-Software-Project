using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicReward.ModelClass;
using Npgsql;
using AcademicReward.Logic;
using System.Collections.ObjectModel;
/**
*  Primary Author: Sean Stille
*  Reviewer: TBD
*  Desc: The database implementation for items within the shop, adding them, deleting them, etc.
*/
namespace AcademicReward.Database
{
    internal class ShopItemDatabase : AcademicRewardsDatabase, IDatabase
    {
        ShopLogic logic;
        public ShopItemDatabase(ShopLogic ShopLogic)
        {
            logic = ShopLogic;
        }
        public DatabaseErrorType AddItem(object obj)
        {
            DatabaseErrorType dbError = DatabaseErrorType.NoError;
            try
            {
                ShopItem item = (ShopItem)obj;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO shopitems (shopitemid, itemtitle, itemdescription, pointcost, levelrequirment, groupid)" +
                          $"VALUES ({item.Id}, '{item.Title}', '{item.Description}', {item.PointCost}, {item.LevelRequirement}, {item.Group.GroupID});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex)
            {
                //Username already exists.
                Console.WriteLine("Error while adding item: {0}", ex);
                
            }
            catch (NpgsqlException ex)
            {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while adding item: {0}", ex);
                
            }
            return dbError;
        }

        public DatabaseErrorType DeleteItem(object obj)
        {
            try
            {
                ShopItem item = obj as ShopItem;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup notifications for a group
                var sql = "DELETE FROM shopitems WHERE shopitemid = '" + item.Id + "';"; 
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                //Closing the connection.
                con.Close();
                return DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex)
            {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);

            }
            return DatabaseErrorType.NoError;
        }

        public object FindById(int id)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType LookupFullItem(object obj)
        {
                ObservableCollection<ShopItem> newList = new ObservableCollection<ShopItem>();
                //Group groupNotifications = obj as Group;
                try
                {
                    //Opening the connection
                    using var con = new NpgsqlConnection(InitializeConnectionString());
                    con.Open();
                    //SQL to lookup notifications for a group
                    var sql = "SELECT * " +
                              "FROM shopitems;";
                    //Executing the query.
                    using var cmd = new NpgsqlCommand(sql, con);
                    using NpgsqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        int groupID = (int)reader[5]; //Getting group ID
                        bool idPresent = false;
                        Group gettingGroup = null;

                        foreach (Group g in MauiProgram.Profile.GroupList) //Check the group list for this profile to see if it uses that ID
                        {
                            if(g.GroupID == groupID) //If it does use that ID, mark the corresponding group and mark that the user is part of this group
                            {
                            idPresent = true;   
                            gettingGroup = g;
                            }
                        }
                        if (idPresent)  //If the item is for a group this user is in, add the item to the list the user will see
                        {
                        ShopItem item = new ShopItem((int)reader[0], reader[1] as string, reader[2] as string, 
                            (int)reader[3], (int)reader[4], gettingGroup);

                        newList.Add(item);
                        }
                        
                        
                    }
                    logic.ItemList = newList;
                    //Closing the connection.
                    con.Close();
                return DatabaseErrorType.NoError;
                }
                catch (NpgsqlException ex)
                {
                    //Something went wrong adding the task
                    Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);
                   
                }
                return DatabaseErrorType.NoError;

        }

        public DatabaseErrorType LookupItem(object obj)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType UpdateItem(object obj)
        {
            try
            {
                ShopItem item = obj as ShopItem;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup notifications for a group
                var sql = "UPDATE shopitems SET  " +
                    $"itemtitle='{item.Title}', itemdescription='{item.Description}', pointcost={item.PointCost}, " +
                    $"levelrequirment={item.LevelRequirement} WHERE shopitemid={item.Id};";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                //Closing the connection.
                con.Close();
                return DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex)
            {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);

            }
            return DatabaseErrorType.NoError;
            throw new NotImplementedException();
        }
    }
}
