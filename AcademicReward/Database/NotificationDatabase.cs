using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class NotificationDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// NotificationDatbase constructor
        /// </summary>
        public NotificationDatabase() { }

        /// <summary>
        /// Method used to add a notification (database)
        /// </summary>
        /// <param name="notification">object notification</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType AddItem(object notification) {
            DatabaseErrorType dbError;
            Notification notificationToAdd = notification as Notification;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to add notification to table
                var sql = "INSERT INTO notifications (notificationtitle, notificationdescription, groupid)" +
                          $"VALUES ('{notificationToAdd.Title}', '{notificationToAdd.Description}', {notificationToAdd.GroupID});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while adding notification: {0}", ex);
                dbError = DatabaseErrorType.AddNotificationDBError;
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType UpdateItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupFullItem(object notification) {
            return DatabaseErrorType.NoError;
        }
    }
}
