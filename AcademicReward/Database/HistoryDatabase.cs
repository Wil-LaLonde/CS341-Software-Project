using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {
    /// <summary>
    /// HistoryDatabase controls anything in the history table in the database
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Xee Lo
    /// </summary>
    public class HistoryDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// Method used to add a history item (database)
        /// </summary>
        /// <param name="historyItem">object historyItem</param>
        /// <returns>DatabaseErrrorType dbError</returns>
        public DatabaseErrorType AddItem(object historyItem) {
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "INSERT INTO history (profileId, title, description) VALUES (" + $"'{historyItemToUpdate.ProfileId}', '{historyItemToUpdate.Title}', '{historyItemToUpdate.Description}');";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex) {
                Console.WriteLine("Error while adding history: {0}", ex);
                dbError = DatabaseErrorType.AddHistoryDBError;
            }
            catch (NpgsqlException ex) {
                Console.WriteLine("Error while adding history: {0}", ex);
                dbError = DatabaseErrorType.AddHistoryDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to delete a history item (database)
        /// </summary>
        /// <param name="historyItem">object historyItem</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType DeleteItem(object historyItem) { 
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "DELETE FROM history WHERE historyid = " + $"'{historyItemToUpdate.HistoryId}';";
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex) {
                Console.WriteLine("Error while deleting history: {0}", ex);
                dbError = DatabaseErrorType.DeleteHistoryDBError;
            }
            catch (NpgsqlException ex) {
                Console.WriteLine("Error while deleting history: {0}", ex);
                dbError = DatabaseErrorType.DeleteHistoryDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to update a history item (database)
        /// </summary>
        /// <param name="historyItem">object historyItem</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType UpdateItem(object historyItem) {
            HistoryItem historyItemToUpdate = historyItem as HistoryItem;
            DatabaseErrorType dbError;
            try {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "UPDATE history SET title = " + $"'{historyItemToUpdate.Title}', description = '{historyItemToUpdate.Description}' WHERE historyid = '{historyItemToUpdate.HistoryId}';";
                using var cmd = new NpgsqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (PostgresException ex) {
                Console.WriteLine("Error while editing history: {0}", ex);
                dbError = DatabaseErrorType.UpdateHistoryDBError;
            }
            catch (NpgsqlException ex) {
                Console.WriteLine("Error while editing history: {0}", ex);
                dbError = DatabaseErrorType.UpdateHistoryDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to load all history items from the database
        /// </summary>
        /// <param name="historyItems">ObservableCollection historyItems</param>
        /// <param name="args">string[] args</param>
        /// <returns></returns>
        public DatabaseErrorType LoadItems(ObservableCollection<object> historyItems, string[] args) {
            DatabaseErrorType dbError;
            int ProfileId = int.Parse(args[0]);
            try {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                var sql = "SELECT * FROM history WHERE profileid = " + $"'{ProfileId}';";
                using var cmd = new NpgsqlCommand(sql, con);
                var HistoryItemsReader = cmd.ExecuteReader();

                // Add all the items from HistoryItemsReader to HistoryItems
                while (HistoryItemsReader.Read()) {
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
            catch (PostgresException ex) {
                Console.WriteLine("Error while grabbing history: {0}", ex);
                dbError = DatabaseErrorType.LoadHistoryDBError;
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
}
