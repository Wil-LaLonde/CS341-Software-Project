using Npgsql;

namespace AcademicReward.Database {
    public class AcademicRewardsDatabase : IDatabase {
        private string connectionString = string.Empty;

        /// <summary>
        /// AcademicRewardsDatabase constructor
        /// </summary>
        public AcademicRewardsDatabase() {
            connectionString = InitializeConnectionString();
        }

        /// <summary>
        /// Constructs the database connection string
        /// </summary>
        /// <returns>string database connection</returns>
        private string InitializeConnectionString() {
            string bitHost = "db.bit.io";
            string bitApiKey = "v2_3vaWv_9N4SgRBSZweqRCh5mKAQ4BE"; // from the "Password" field of the "Connect" menu
            string bitUsername = "pattmax";
            string bitDatabaseName = "pattmax/AcademicRewards";
            return $"Host={bitHost};Username={bitUsername};Password={bitApiKey};Database={bitDatabaseName}";
        }
    }
}
