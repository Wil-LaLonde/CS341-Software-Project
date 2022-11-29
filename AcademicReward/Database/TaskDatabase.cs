using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database {
    public class TaskDatabase : AcademicRewardsDatabase, IDatabase {

        public TaskDatabase() { }

        public DatabaseErrorType AddItem(object task) {
            DatabaseErrorType dbError;
            ModelClass.Task taskToAdd = task as ModelClass.Task;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to add task to table
                var sql = "INSERT INTO tasks (tasktitle, taskdescription, points, groupid)" +
                          $"VALUES ('{taskToAdd.Title}', '{taskToAdd.Description}', {taskToAdd.Points}, {taskToAdd.GroupID});";
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

        public DatabaseErrorType LookupItem(object task) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupFullItem(object task) {
            return DatabaseErrorType.NoError;
        }
    }
}
