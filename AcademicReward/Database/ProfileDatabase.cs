using AcademicReward.Resources;
using System.Collections.ObjectModel;
using Npgsql;
using AcademicReward.ModelClass;

namespace AcademicReward.Database {
    /// <summary>
    /// ProfileDatabase updates a profile's xp, points, and level
    /// Primary Author: Xee Lo
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class ProfileDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// ProfileDatabase constructor
        /// </summary>
        public ProfileDatabase() { }

        //Currently not needed
        public DatabaseErrorType AddItem(object obj) {
            return DatabaseErrorType.NotImplemented;
        }

        /// <summary>
        /// Updating profile XP, Points, and Level in the database
        /// </summary>
        /// <param name="profile">object profile</param>
        /// <returns>DatabaseErrorType dbError</returns>
        public DatabaseErrorType UpdateItem(object profile) {
            DatabaseErrorType dbError;
            Profile profileToUpdate = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for updating a profile (XP, POINTS, AND LEVEL)
                var sql = "UPDATE profiles " +
                          $"SET xp = {profileToUpdate.XP}, points = {profileToUpdate.Points}, level = {profileToUpdate.Level} " +
                          $"WHERE profileid = {MauiProgram.Profile.ProfileID};";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while updating profile: {0}", ex);
                dbError = DatabaseErrorType.UpdateProfileDBError;
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object obj) {
            return DatabaseErrorType.NotImplemented;
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
        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
            return DatabaseErrorType.NotImplemented;
        }

        //Currently not needed
        public Object FindById(int id) {
            return DatabaseErrorType.NotImplemented;
        }
    }
}
