﻿using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    public class TaskDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// TaskDatabase constructor
        /// </summary>
        public TaskDatabase() { }

        /// <summary>
        /// Method used to add a new task (database)
        /// </summary>
        /// <param name="task">object task</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType AddItem(object task) {
            DatabaseErrorType dbError;
            ModelClass.Task taskToAdd = task as ModelClass.Task;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to add task to table
                var sql = "INSERT INTO tasks (tasktitle, taskdescription, points, groupid, ischecked)" +
                          $"VALUES ('{taskToAdd.Title}', '{taskToAdd.Description}', {taskToAdd.Points}, {taskToAdd.GroupID}, {taskToAdd.IsChecked});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            } catch(NpgsqlException ex) {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while adding task: {0}", ex);
                dbError = DatabaseErrorType.AddTaskDBError;
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType UpdateItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupFullItem(object task) {
            return DatabaseErrorType.NoError;
        }
    }
}
