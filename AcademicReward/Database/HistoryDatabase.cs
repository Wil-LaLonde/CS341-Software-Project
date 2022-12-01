using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer:
    /// </summary>
    public class HistoryDatabase : AcademicRewardsDatabase, IDatabase
    {
        public DatabaseErrorType AddItem(object historyItem)
        {
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try
            {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "INSERT INTO history (profileId, title, description) VALUES (" + $"'{historyItemToUpdate.ProfileId}', '{historyItemToUpdate.Title}', '{historyItemToUpdate.Description}');";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine("Error while adding history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error while adding history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            return dbError;
        }

        public DatabaseErrorType DeleteItem(object historyItem)
        { 
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try
            {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "DELETE FROM history WHERE historyid = " + $"'{historyItemToUpdate.HistoryId}';";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine("Error while deleting history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error while deleting history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            return dbError;
        }

        public DatabaseErrorType UpdateItem(object historyItem)
        {
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try
            {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "UPDATE history SET title = " + $"'{historyItemToUpdate.Title}', description = '{historyItemToUpdate.Description}' WHERE historyid = '{historyItemToUpdate.HistoryId}';";
                using var cmd = new NpgsqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine("Error while editing history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error while editing history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            return dbError;
        }

        public DatabaseErrorType LoadItems(ObservableCollection<object> historyItems, string[] args)
        {
            DatabaseErrorType dbError;
            int ProfileId = int.Parse(args[0]);
            try
            {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "SELECT * FROM history WHERE profileid = " + $"'{ProfileId}';";
                using var cmd = new NpgsqlCommand(sql, con);
                var HistoryItemsReader = cmd.ExecuteReader();

                // Add all the items from HistoryItemsReader to HistoryItems
                while (HistoryItemsReader.Read())
                {
                    historyItems.Add(new HistoryItem
                    (
                        (int)HistoryItemsReader.GetDouble(0), // historyid
                        (int)HistoryItemsReader.GetDouble(1), // ProfileId
                        HistoryItemsReader.GetString(2), // title
                        HistoryItemsReader.GetString(3) // Description
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
                Console.WriteLine("Error while grabbing history: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            }
            return dbError;
        }

        public DatabaseErrorType LookupItem(object obj)
        {
            throw new NotImplementedException();
        }

        public DatabaseErrorType LookupFullItem(object obj)
        {
            throw new NotImplementedException();
        }

        public object FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
