using AcademicReward.Resources;
using AcademicReward.ModelClass;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde, Xee Lo
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
                //SQL to add task to table, also adding task to profiletask table
                var sql = "INSERT INTO tasks (tasktitle, taskdescription, points, groupid, ischecked)" +
                          $"VALUES ('{taskToAdd.Title}', '{taskToAdd.Description}', {taskToAdd.Points}, {taskToAdd.GroupID}, {taskToAdd.IsChecked}, {taskToAdd.IsSubmitted}); " +
                          "INSERT INTO profiletask " +
                          "SELECT profileid, MAX(taskid), ischecked, ischecked " +
                          "FROM profilegroup, tasks " +
                          $"WHERE profilegroup.groupid = {taskToAdd.GroupID} " +
                          "GROUP BY profileid, ischecked;";
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
            DatabaseErrorType dbError;
            ModelClass.Task taskToUpdate = task as ModelClass.Task;

            if (taskToUpdate.IsChecked) {
                //update the bool
                try {
                    //Opening the connection
                    using var con = new NpgsqlConnection(InitializeConnectionString());
                    con.Open();
                    //SQL to update the task that is associated to a profile in profiletask table ADMIN VIEW
                    var sql = "UPDATE profiletask" +
                              $"SET isapproved = '{taskToUpdate.IsChecked}'" +
                              $"WHERE profiletask.taskid = {taskToUpdate.TaskID} +" +
                              $"AND profiletask.profileid = {MauiProgram.Profile.ProfileID};";// +
                    /*"UPDATE tasks" +
                    $"SET ischecked = '{taskToUpdate.IsChecked}'" +
                    $"AND task.profileid = {MauiProgram.Profile.ProfileID};";*/

                    //Executing the query.
                    using var cmd = new NpgsqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    //Closing the connection.
                    con.Close();
                    dbError = DatabaseErrorType.NoError;
                }
                catch (NpgsqlException ex) {
                    //Something went wrong updating the task
                    Console.WriteLine("Unexpected error while adding task: {0}", ex);
                    dbError = DatabaseErrorType.UpdateTaskDbError;
                }
            }
            else {
                try {
                    //Opening the connection
                    using var con = new NpgsqlConnection(InitializeConnectionString());
                    con.Open();
                    //SQL to update the task that is associated to a profile in profiletask table MEMBER VIEW
                    var sql = "UPDATE profiletask" +
                              $"SET issubmitted = '{taskToUpdate.IsSubmitted}'" +
                              $"WHERE taskid = {taskToUpdate.TaskID} +" +
                              $"AND profileid = {MauiProgram.Profile.ProfileID};";
                    //Executing the query.
                    using var cmd = new NpgsqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    //Closing the connection.
                    con.Close();
                    dbError = DatabaseErrorType.NoError;
                }
                catch (NpgsqlException ex) {
                    //Something went wrong updating the task
                    Console.WriteLine("Unexpected error while adding task: {0}", ex);
                    dbError = DatabaseErrorType.UpdateTaskDbError;
                }
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object task) {
            DatabaseErrorType dbError;
            ModelClass.Task taskToFind = task as ModelClass.Task;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup tasks for a group
                var sql = "SELECT issubmitted, isapproved" +
                          "FROM profiletask " +
                          $"WHERE taskid = {taskToFind.TaskID} AND profileid = {MauiProgram.Profile.ProfileID};";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Creating tasks objects
                //[0] -> issubmitted | [1] -> isapproved
                while (reader.Read()) {
                    //set the task here
                    taskToFind.IsSubmitted = (bool)reader[0];
                    taskToFind.IsChecked = (bool)reader[1];
                }
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex) {
                //Something went wrong looking up the task
                Console.WriteLine("Unexpected error while looking up task: {0}", ex);
                dbError = DatabaseErrorType.LookupTaskDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to look up all tasks for a profile
        /// </summary>
        /// <param name="profile">object profile</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType LookupFullItem(object profile) {
            DatabaseErrorType dbError;
            ModelClass.Profile profileTasks = profile as ModelClass.Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //SQL to lookup tasks for a group
                var sql = "SELECT * " +
                          "FROM tasks " +
                          $"WHERE taskid IN (SELECT taskid FROM profiletask WHERE profileid = {profileTasks.ProfileID});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Creating tasks objects
                //[0] -> taskid | [1] -> tasktitle | [2] -> taskdescription | [3] -> points | [4] -> groupid | [5] -> ischecked | [6] -> issubmitted
                while (reader.Read()) {
                    ModelClass.Task task = new ModelClass.Task((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3], (int)reader[4], (bool)reader[5], (bool)reader[6]);
                    profileTasks.AddTaskToProfile(task);
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

        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args)
        {
            throw new NotImplementedException();
        }

        public object FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
