using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;


namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: Wil LaLonde
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HistoryPage : ContentPage
{
    // New observable collection of HistoryItem
    public ObservableCollection<HistoryItem> HistoryItems = new ObservableCollection<HistoryItem>();

    public HistoryPage()
    {
        InitializeComponent();
        HistoryItemsLV.ItemsSource = HistoryItems;
        LoadHistoryItems();
    }

    // Load the history items from the database
    public DatabaseErrorType LoadHistoryItems()
    {
        int userID = MauiProgram.Profile.ProfileID;

        DatabaseErrorType dbError;
        try
        {
            using var con = new NpgsqlConnection(Database.AcademicRewardsDatabase.InitializeConnectionString());
            con.Open();
            var sql = "SELECT * FROM history WHERE profileid = " + $"'{userID}';";
            using var cmd = new NpgsqlCommand(sql, con);
            var HistoryItemsReader = cmd.ExecuteReader();

            // Add all the items from HistoryItemsReader to HistoryItems
            while (HistoryItemsReader.Read())
            {
                HistoryItems.Add(new HistoryItem
                (
                    (int)HistoryItemsReader.GetDouble(0), // historyid
                    (int)HistoryItemsReader.GetDouble(1), // profileid
                    HistoryItemsReader.GetString(2), // title
                    HistoryItemsReader.GetString(3) // description
                ));
            }

            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex)
        {
            Console.WriteLine("Error while grabbing history: {0}", ex);
            dbError = DatabaseErrorType.UsernameTakenDBError;
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine("Unexpected error while grabbing history: {0}", ex);
            dbError = DatabaseErrorType.AddProfileDBError;
        }
        return dbError;
    }
}