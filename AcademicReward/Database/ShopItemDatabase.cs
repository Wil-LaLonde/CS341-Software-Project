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
                          $"VALUES ({item.Id}, '{item.Title}', '{item.Description}', {item.PointCost}, {item.LevelRequirement}, {1});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
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
                    
                while (reader.Read()) {
                    ShopItem item = new ShopItem((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3], (int)reader[4], null);
                    newList.Add(item);
                        
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
