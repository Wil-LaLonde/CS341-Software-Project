using AcademicReward.Resources;
using AcademicReward.ModelClass;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {

    /// <summary>
    /// ShopItemDatabase controls shop items in the database
    /// Primary Author: Sean Stille
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class ShopItemDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// ShopItemDatabase constructor
        /// </summary>
        public ShopItemDatabase() { }

        /// <summary>
        /// Method used to buy a shop item (database)
        /// </summary>
        /// <param name="shopItem">object shopItem</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType BuyItem(object shopItem) {
            DatabaseErrorType dbError;
            ShopItem shopItemToBuy = shopItem as ShopItem;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO purchasedshopitems (profileid, shopitemid)" +
                          $"VALUES ({MauiProgram.Profile.ProfileID}, '{shopItemToBuy.Id}');";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                //Remove shop item from member view
                MauiProgram.Profile.ProfileShop.RemoveShopItemFromShop(shopItemToBuy);
                //Update member points
                MauiProgram.Profile.RemovePointsFromMember(shopItemToBuy.PointCost);
                //Call database to update point values
                IDatabase profileDB = new ProfileDatabase();
                profileDB.UpdateItem(MauiProgram.Profile);
                dbError = DatabaseErrorType.NoError;
            } catch (PostgresException ex) {
                Console.WriteLine("Error while buying item: {0}", ex);
                dbError = DatabaseErrorType.BuyItemError;
            } catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while buying item: {0}", ex);
                dbError = DatabaseErrorType.BuyItemError;
            }
            return dbError;
        }


        /// <summary>
        /// Method used to add a shop item (database)
        /// </summary>
        /// <param name="shopItem">object shopItem</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType AddItem(object shopItem) {
            DatabaseErrorType dbError;
            ShopItem shopItemToAdd = shopItem as ShopItem;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO shopitems (itemtitle, itemdescription, pointcost, levelrequirment, groupid) " +
                          $"VALUES ('{shopItemToAdd.Title}', '{shopItemToAdd.Description}', {shopItemToAdd.PointCost}, {shopItemToAdd.LevelRequirement}, {shopItemToAdd.Group.GroupID}) RETURNING shopitemid;";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Gathering new shopitemid
                int shopItemID;
                while(reader.Read()) {
                    shopItemID = (int)reader[0];
                    //Assigning new id to the shopItem object
                    shopItemToAdd.Id = shopItemID;
                    MauiProgram.Profile.ProfileShop.AddShopItemToShop(shopItemToAdd);
                }
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex) {
                //Error adding shop item
                Console.WriteLine("Error while adding item: {0}", ex);
                dbError = DatabaseErrorType.AddShopItemDBError;
            }
            catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while adding item: {0}", ex);
                dbError = DatabaseErrorType.AddShopItemDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to delete a shop item
        /// </summary>
        /// <param name="shopItem">object shopItem</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType DeleteItem(object shopItem) {
            DatabaseErrorType dbError;
            ShopItem shopItemToDelete = shopItem as ShopItem;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to delete the shop item
                var sql = "DELETE FROM shopitems " +
                         $"WHERE shopitemid = {shopItemToDelete.Id};" +
                          "DELETE FROM purchasedshopitems " +
                         $"WHERE shopitemid = {shopItemToDelete.Id};"; 
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                
                //Closing the connection.
                con.Close();
                //Remove item from shopitem list
                MauiProgram.Profile.ProfileShop.RemoveShopItemFromShop(shopItemToDelete);
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Something went wrong deleting the shop item
                Console.WriteLine("Unexpected error while deleting the shop item: {0}", ex);
                dbError = DatabaseErrorType.DeleteShopItemDBError;
            }
            return dbError;
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
        /// <param name="profile">object profile</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType LookupFullItem(object profile) {
            DatabaseErrorType dbError;
            Profile profileToLookup = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup shop items for a given profile
                var sql = "";
                if(profileToLookup.IsAdmin) {
                    //Admins will be able to see all shop items for their groups
                    sql = "SELECT * " +
                          "FROM shopitems " +
                          "WHERE groupid IN (SELECT groupid " +
                                            "FROM profilegroup " +
                                           $"WHERE profileid = {profileToLookup.ProfileID});";
                } else {
                    //Members will be able to see all shop items they have not purhcased yet
                    sql = "SELECT * " +
                          "FROM shopitems " +
                          "WHERE groupid IN (SELECT groupid " +
                                            "FROM profilegroup " +
                                           $"WHERE profileid = {profileToLookup.ProfileID} AND shopitemid NOT IN (SELECT shopitemid " +
                                                                                                                 "FROM purchasedshopitems " +
                                                                                                                $"WHERE profileid = {profileToLookup.ProfileID})" +
                                           ");";
                }
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                    
                while (reader.Read()) {
                    ShopItem shopItem = new ShopItem((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3], (int)reader[4], profileToLookup.GetGroupUsingGroupID((int)reader[5]));
                    //Add shop item to the profile shop
                    MauiProgram.Profile.ProfileShop.AddShopItemToShop(shopItem);
                }
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            } catch (NpgsqlException ex) {
                //Something went wrong looking up shop items
                Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);
                dbError = DatabaseErrorType.LookupAllShopItemsDBError;
            } 
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object obj) {
            return DatabaseErrorType.NotImplemented;
        }

        /// <summary>
        /// Method used to update a shop item (database)
        /// </summary>
        /// <param name="shopItem">object shopItem</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType UpdateItem(object shopItem) {
            DatabaseErrorType dbError;
            try {
                ShopItem shopItemToUpdate = shopItem as ShopItem;
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup notifications for a group
                var sql = "UPDATE shopitems SET " +
                    $"itemtitle = '{shopItemToUpdate.Title}', itemdescription = '{shopItemToUpdate.Description}', pointcost = {shopItemToUpdate.PointCost}, " +
                    $"levelrequirment = {shopItemToUpdate.LevelRequirement} WHERE shopitemid = {shopItemToUpdate.Id};";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Something went wrong updating the shop item
                Console.WriteLine("Unexpected error updating the shopitem: {0}", ex);
                dbError = DatabaseErrorType.UpdateShopItemDBError;
            }
            return dbError;
        }
    }
}
