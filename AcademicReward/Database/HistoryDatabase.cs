using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     HistoryDatabase controls anything in the history table in the database
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Xee Lo
/// </summary>
public class HistoryDatabase : AcademicRewardsDatabase, IDatabase {
    /// <summary>
    ///     Method used to add a history item (database)
    /// </summary>
    /// <param name="historyItem">object historyItem</param>
    /// <returns>DatabaseErrrorType dbError</returns>
    public DatabaseErrorType AddItem(object historyItem) {
        HistoryItem historyItemToUpdate = historyItem as HistoryItem;
        DatabaseErrorType dbError;
        try {
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            string sql = "INSERT INTO history (profileId, title, description) VALUES (" +
                $"'{historyItemToUpdate.ProfileId}', '{historyItemToUpdate.Title}', '{historyItemToUpdate.Description}');";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            Console.WriteLine("Error while adding history: {0}", ex);
            dbError = DatabaseErrorType.AddHistoryDbError;
        }
        catch (NpgsqlException ex) {
            Console.WriteLine("Error while adding history: {0}", ex);
            dbError = DatabaseErrorType.AddHistoryDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to delete a history item (database)
    /// </summary>
    /// <param name="historyItem">object historyItem</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType DeleteItem(object historyItem) {
        HistoryItem historyItemToUpdate = historyItem as HistoryItem;
        DatabaseErrorType dbError;
        try {
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            string sql = "DELETE FROM history WHERE historyid = " + $"'{historyItemToUpdate.HistoryId}';";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            Console.WriteLine("Error while deleting history: {0}", ex);
            dbError = DatabaseErrorType.DeleteHistoryDbError;
        }
        catch (NpgsqlException ex) {
            Console.WriteLine("Error while deleting history: {0}", ex);
            dbError = DatabaseErrorType.DeleteHistoryDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to update a history item (database)
    /// </summary>
    /// <param name="historyItem">object historyItem</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType UpdateItem(object historyItem) {
        HistoryItem historyItemToUpdate = historyItem as HistoryItem;
        DatabaseErrorType dbError;
        try {
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            string sql = "UPDATE history SET title = " +
                $"'{historyItemToUpdate.Title}', description = '{historyItemToUpdate.Description}' WHERE historyid = '{historyItemToUpdate.HistoryId}';";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            Console.WriteLine("Error while editing history: {0}", ex);
            dbError = DatabaseErrorType.UpdateHistoryDbError;
        }
        catch (NpgsqlException ex) {
            Console.WriteLine("Error while editing history: {0}", ex);
            dbError = DatabaseErrorType.UpdateHistoryDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to load all history items from the database
    /// </summary>
    /// <param name="historyItems">ObservableCollection historyItems</param>
    /// <param name="args">string[] args</param>
    /// <returns></returns>
    public DatabaseErrorType LoadItems(ObservableCollection<object> historyItems, string[] args) {
        DatabaseErrorType dbError;
        int profileId = int.Parse(args[0]);
        try {
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            string sql = "SELECT * FROM history WHERE profileid = " + $"'{profileId}';";
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            NpgsqlDataReader historyItemsReader = cmd.ExecuteReader();

            // Add all the items from HistoryItemsReader to HistoryItems
            while (historyItemsReader.Read()) {
                historyItems.Add(new HistoryItem
                (
                    (int)historyItemsReader.GetDouble(0), // historyid
                    (int)historyItemsReader.GetDouble(1), // ProfileId
                    historyItemsReader.GetString(2),      // title
                    historyItemsReader.GetString(3)       // Description
                ));
            }

            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            Console.WriteLine("Error while grabbing history: {0}", ex);
            dbError = DatabaseErrorType.LoadHistoryDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType LookupItem(object obj) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType LookupFullItem(object obj) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public object FindById(int id) {
        return DatabaseErrorType.NotImplemented;
    }
}