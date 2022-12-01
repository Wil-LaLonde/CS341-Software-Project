using Npgsql;

namespace AcademicReward.Database
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: Wil LaLonde
    /// Reviewer: Wil LaLonde / Maximilian Patterson
    /// </summary>
    public class AcademicRewardsDatabase
    {
        private string connectionString = string.Empty;
        private const string BitHost = "db.bit.io";
        private const string BitApiKey = "v2_3vaWv_9N4SgRBSZweqRCh5mKAQ4BE";
        private const string BitUsername = "pattmax";
        private const string BitDatabaseName = "pattmax/AcademicRewards";

        /// <summary>
        /// InitDatabaseConnection opens up a new
        /// AcademicRewards database connection
        /// 
        /// IMPORTANT: This needs to be opened and
        /// closed in the calling method
        /// </summary>
        public NpgsqlConnection InitDatabaseConnection()
        {
            //Creating the connection string
            connectionString = InitializeConnectionString();
            //Establishing the connection
            using var con = new NpgsqlConnection(connectionString);
            //Retuning the connection
            return con;
        }

        /// <summary>
        /// Constructs the database connection string
        /// </summary>
        /// <returns>string database connection</returns>
        public static string InitializeConnectionString()
        {
            return $"Host={BitHost};Username={BitUsername};Password={BitApiKey};Database={BitDatabaseName}";
        }
    }
}
