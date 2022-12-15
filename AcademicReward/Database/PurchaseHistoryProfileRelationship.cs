using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     Provides methods for interacting with the profiles/shopitems relationship
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
internal class PurchaseHistoryProfileRelationship : AcademicRewardsDatabase {
    // Method to grab all purchase history items for a given profile
    public static ObservableCollection<PurchaseHistoryItem> GetPurchaseHistory(Profile profile) {
        try {
            int profileId = profile.ProfileId;
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Select SQL query for getting all purchase history for the current user, also grab the title and description columns from the shopitems table
            string sql =
                "SELECT purchasedshopitems.shopitemid, purchasedshopitems.profileid, shopitems.itemtitle, shopitems.itemdescription " +
                "FROM purchasedshopitems " +
                "INNER JOIN shopitems ON purchasedshopitems.shopitemid = shopitems.shopitemid " +
                "WHERE profileid = " + $"{profileId}" + ";";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            //Reading the data
            ObservableCollection<PurchaseHistoryItem> purchaseHistoryItems = new();

            while (reader.Read()) {
                purchaseHistoryItems.Add(new PurchaseHistoryItem(
                    reader.GetInt32(0),  // PurchaseHistoryID
                    reader.GetInt32(1),  // ProfileID
                    reader.GetString(2), // Title
                    reader.GetString(3)  // Description
                ));
            }

            //Closing the connection.
            con.Close();

            return purchaseHistoryItems;
        }
        catch (Exception e) {
            Console.WriteLine("Error while getting all purchase history items from profile: {0}", e);
            return null;
        }
    }
}