using AcademicReward.Resources;
using AcademicReward.ModelClass;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {
    class HomeDatabase : AcademicRewardsDatabase, IDatabase{

        /// <summary>
        /// Primary Author: Xee Lo
        /// Secondary Author: Wil LaLonde 
        /// Reviewer: 
        /// </summary>
        public HomeDatabase() { }

        //Currently not needed
        public DatabaseErrorType AddItem(object obj) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType UpdateItem(object obj) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object obj) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object obj) {
            return DatabaseErrorType.NoError;
        }

        /// <summary>
        /// Method used to look up all tasks for a group
        /// </summary>
        /// <param name="group">object group</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType LookupFullItem(object group) {
            DatabaseErrorType dbError;
            Group groupTasks = group as Group;
            //Profile profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup tasks for a group
                var sql = "SELECT * " +
                          "FROM tasks " +
                          $"WHERE groupid = {groupTasks.GroupID};";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Creating tasks objects
                //[0] -> taskid | [1] -> tasktitle | [2] -> taskdescription | [3] -> points | [4] -> groupid | [5] -> ischecked
                while (reader.Read()) {
                    ModelClass.Task task = new ModelClass.Task((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3], (int)reader[4], (bool)reader[5]);
                    groupTasks.AddTaskToGroup(task);
                }
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Something went wrong looking up the task
                Console.WriteLine("Unexpected error while looking up task: {0}", ex);
                dbError = DatabaseErrorType.LookupAllTasksDBError;
            }
            return dbError;
        }

        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
            return DatabaseErrorType.NoError;
        }

        public object FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
