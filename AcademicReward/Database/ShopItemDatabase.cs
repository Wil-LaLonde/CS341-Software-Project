using AcademicReward.Resources;
using AcademicReward.ModelClass;
using Npgsql;
using AcademicReward.Logic;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {

    /// <summary>
    /// ShopItemDatabase controls shop items in the database
    /// Primary Author: Sean Stille
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class ShopItemDatabase : AcademicRewardsDatabase, IDatabase {
        ShopLogic logic;

        /// <summary>
        /// ShopItemDatabase constructor
        /// </summary>
        /// <param name="ShopLogic">ShopLogic shoplogic</param>
        public ShopItemDatabase(ShopLogic ShopLogic) {
            logic = ShopLogic;
        }
        public DatabaseErrorType BuyItem(object obj)
        {
            DatabaseErrorType dbError = DatabaseErrorType.BuyItemError;
            try
            {
                ShopItem item = (ShopItem)obj;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO purchasedshopitems (profileid, shopitemid)" +
                          $"VALUES ({MauiProgram.Profile.ProfileID}, '{item.Id}');";
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


        /// <summary>
        /// Method used to add a shop item (database)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType AddItem(object obj) {
            DatabaseErrorType dbError = DatabaseErrorType.NoError;
            try {
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
                sql = "INSERT INTO history (profileid, title, description) VALUES " +
                    $"({MauiProgram.Profile.ProfileID}, 'Added a shop item','Added an item to group: {item.Group.GroupName}');";
                using var secondCmd = new NpgsqlCommand(sql, con);

                secondCmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex) {
                //Error adding shop item
                Console.WriteLine("Error while adding item: {0}", ex);
            }
            catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while adding item: {0}", ex);
            }
            return dbError;
        }

        /// <summary>
        /// Method used to delete a shop item
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType DeleteItem(object obj) {
            try {
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
            catch (NpgsqlException ex) {
                //Something went wrong deleting the shop item
                Console.WriteLine("Unexpected error while deleting the shop item: {0}", ex);
            }
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public object FindById(int id) {
            return DatabaseErrorType.NotImplemented;
        }

        //Currently not needed
        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
            return DatabaseErrorType.NotImplemented;
        }

        /// <summary>
        /// Method used to lookup all shop items (database)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType LookupFullItem(object obj) {
            ObservableCollection<ShopItem> newList = new ObservableCollection<ShopItem>();
            try {
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
                        Profile profile = MauiProgram.Profile;
                        int groupID = (int)reader[5]; //Getting group ID
                        bool idPresent = false;
                        Group gettingGroup = null;
                        bool alreadyPurchased = false;
                        foreach (Group g in profile.GroupList) //Check the group list for this profile to see if it uses that ID
                        {
                            if(g.GroupID == groupID) //If it does use that ID, mark the corresponding group and mark that the user is part of this group
                            {
                            idPresent = true;   
                            gettingGroup = g;
                            }
                        }
                        foreach( ShopItem i in profile.PurchaseItems)
                        {
                            if( i.Id == (int)reader[0])
                            {
                                alreadyPurchased = true;
                            }
                        }
                        if (idPresent && ! alreadyPurchased)  //If the item is for a group this user is in,
                                                              //and the item hasn't already been purchased,
                                                              //add the item to the list the user will see
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
            catch (NpgsqlException ex) {
                //Something went wrong looking up shop items
                Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);
                   
            }
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object obj) {
            return DatabaseErrorType.NotImplemented;
        }

        /// <summary>
        /// Method used to update a shop item (database)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType UpdateItem(object obj) {
            try {
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
            catch (NpgsqlException ex) {
                //Something went wrong updating the shop item
                Console.WriteLine("Unexpected error updating the shopitem: {0}", ex);
            }
            return DatabaseErrorType.NoError;
        }
    }
}
