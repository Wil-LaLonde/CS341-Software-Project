using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     ShopItemDatabase controls shop items in the database
///     Primary Author: Sean Stille
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public class ShopItemDatabase : AcademicRewardsDatabase, IDatabase {
    /// <summary>
    ///     ShopItemDatabase constructor
    /// </summary>
    public ShopItemDatabase() { }


    /// <summary>
    ///     Method used to add a shop item (database)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType AddItem(object shopItem) {
        DatabaseErrorType dbError;
        ShopItem shopItemToAdd = shopItem as ShopItem;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Insert SQL query for adding a profile
            string sql = "INSERT INTO shopitems (itemtitle, itemdescription, pointcost, levelrequirment, groupid) " +
                $"VALUES ('{shopItemToAdd.Title}', '{shopItemToAdd.Description}', {shopItemToAdd.PointCost}, {shopItemToAdd.LevelRequirement}, {shopItemToAdd.Group.GroupId}) RETURNING shopitemid;";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            //Gathering new shopitemid
            int shopItemId;
            while (reader.Read()) {
                shopItemId = (int)reader[0];
                //Assigning new id to the shopItem object
                shopItemToAdd.Id = shopItemId;
                MauiProgram.Profile.ProfileShop.AddShopItemToShop(shopItemToAdd);
            }

            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            //Error adding shop item
            Console.WriteLine("Error while adding item: {0}", ex);
            dbError = DatabaseErrorType.AddShopItemDbError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while adding item: {0}", ex);
            dbError = DatabaseErrorType.AddShopItemDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to delete a shop item
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType DeleteItem(object shopItem) {
        DatabaseErrorType dbError;
        ShopItem shopItemToDelete = shopItem as ShopItem;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //SQL to delete the shop item
            string sql = "DELETE FROM shopitems " +
                $"WHERE shopitemid = {shopItemToDelete.Id};" +
                "DELETE FROM purchasedshopitems " +
                $"WHERE shopitemid = {shopItemToDelete.Id};";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
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
            dbError = DatabaseErrorType.DeleteShopItemDbError;
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
    ///     Method used to lookup all shop items (database)
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType LookupFullItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileToLookup = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //SQL to lookup shop items for a given profile
            string sql = "";
            if (profileToLookup.IsAdmin)
                //Admins will be able to see all shop items for their groups
                sql = "SELECT * " +
                    "FROM shopitems " +
                    "WHERE groupid IN (SELECT groupid " +
                    "FROM profilegroup " +
                    $"WHERE profileid = {profileToLookup.ProfileId});";
            else
                //Members will be able to see all shop items they have not purhcased yet
                sql = "SELECT * " +
                    "FROM shopitems " +
                    "WHERE groupid IN (SELECT groupid " +
                    "FROM profilegroup " +
                    $"WHERE profileid = {profileToLookup.ProfileId} AND shopitemid NOT IN (SELECT shopitemid " +
                    "FROM purchasedshopitems " +
                    $"WHERE profileid = {profileToLookup.ProfileId})" +
                    ");";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {
                ShopItem shopItem = new((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3],
                    (int)reader[4], profileToLookup.GetGroupUsingGroupId((int)reader[5]));
                //Add shop item to the profile shop
                MauiProgram.Profile.ProfileShop.AddShopItemToShop(shopItem);
            }

            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Something went wrong looking up shop items
            Console.WriteLine("Unexpected error while looking up shopitems: {0}", ex);
            dbError = DatabaseErrorType.LookupAllShopItemsDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType LookupItem(object obj) {
        return DatabaseErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method used to update a shop item (database)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType UpdateItem(object shopItem) {
        DatabaseErrorType dbError;
        try {
            ShopItem shopItemToUpdate = shopItem as ShopItem;
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //SQL to lookup notifications for a group
            string sql = "UPDATE shopitems SET " +
                $"itemtitle = '{shopItemToUpdate.Title}', itemdescription = '{shopItemToUpdate.Description}', pointcost = {shopItemToUpdate.PointCost}, " +
                $"levelrequirment = {shopItemToUpdate.LevelRequirement} WHERE shopitemid = {shopItemToUpdate.Id};";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Something went wrong updating the shop item
            Console.WriteLine("Unexpected error updating the shopitem: {0}", ex);
            dbError = DatabaseErrorType.UpdateShopItemDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to buy a shop item (database)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>DatabaseErrorType</returns>
    public DatabaseErrorType BuyItem(object shopItem) {
        DatabaseErrorType dbError;
        ShopItem shopItemToBuy = shopItem as ShopItem;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Insert SQL query for adding a profile
            string sql = "INSERT INTO purchasedshopitems (profileid, shopitemid)" +
                $"VALUES ({MauiProgram.Profile.ProfileId}, '{shopItemToBuy.Id}');";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //Closing the connection.
            con.Close();
            //Remove shop item from member view
            MauiProgram.Profile.ProfileShop.RemoveShopItemFromShop(shopItemToBuy);
            //Update member points
            MauiProgram.Profile.RemovePointsFromMember(shopItemToBuy.PointCost);
            //Call database to update point values
            IDatabase profileDb = new ProfileDatabase();
            profileDb.UpdateItem(MauiProgram.Profile);
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            Console.WriteLine("Error while buying item: {0}", ex);
            dbError = DatabaseErrorType.BuyItemError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while buying item: {0}", ex);
            dbError = DatabaseErrorType.BuyItemError;
        }

        return dbError;
    }
}