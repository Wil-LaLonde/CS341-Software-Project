﻿using Npgsql;

namespace AcademicReward.Database {
    public class AcademicRewardsDatabase {
        private string connectionString = string.Empty;
        private const string BitHost = "db.bit.io";
        private const string BitApiKey = "v2_3vaWv_9N4SgRBSZweqRCh5mKAQ4BE";
        private const string BitUsername = "pattmax";
        private const string BitDatabaseName = "pattmax/AcademicRewards";

        /// <summary>
        /// InitDatabaseConnection opens up a new
        /// AcademicRewards database connection
        /// 
        /// IMPORTANT: This needs to be closed after
        /// the database action has been completed
        /// </summary>
        public NpgsqlConnection InitDatabaseConnection() {
            //Creating the connection string
            connectionString = InitializeConnectionString();
            //Establishing the connection
            using var con = new NpgsqlConnection(connectionString);
            con.Open();
            //Returning the connection, this needs to be closed in the calling method!!!
            return con;
        }

        /// <summary>
        /// Constructs the database connection string
        /// </summary>
        /// <returns>string database connection</returns>
        private static string InitializeConnectionString() {
            return $"Host={BitHost};Username={BitUsername};Password={BitApiKey};Database={BitDatabaseName}";
        }
    }
}
