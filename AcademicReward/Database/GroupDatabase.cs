using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    public class GroupDatabase : AcademicRewardsDatabase, IDatabase
    {

        /// <summary>
        /// GroupDatabase constructor
        /// </summary>
        public GroupDatabase() { }

        /// <summary>
        /// Method used to add a new group (database)
        /// </summary>
        /// <param name="group">object group</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType AddItem(object group)
        {
            DatabaseErrorType dbError;
            var groupToAdd = group as ModelClass.Group;
            try
            {
                // Open connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();

                // SQL to add group to table and return created row
                var sql = "INSERT INTO groups (groupname, groupdescription, adminprofileid)" +
                    " VALUES ('" + $"{groupToAdd.GroupName}" + "', '" + $"{groupToAdd.GroupDescription}" + "', '" + $"{groupToAdd.GroupAdmin.ProfileID}" + "') RETURNING groupid;";

                // Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();

                // Closing the connection.
                dbError = DatabaseErrorType.NoError;

                // Getting the group id from the created row
                var id = 0;
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                con.Close();
                dbError = DatabaseErrorType.NoError;

                groupToAdd.GroupID = id;
                GroupProfileRelationship.addProfileToGroup(MauiProgram.Profile, groupToAdd);

                groupToAdd.AdminProfileID = MauiProgram.Profile.ProfileID;
                MauiProgram.Profile.AddGroupToProfile(groupToAdd); // Update list for current profile
            }
            catch (NpgsqlException ex)
            {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while adding task: {0}", ex);
                dbError = DatabaseErrorType.AddGroupDBError;
            }
            return dbError;
        }

        /// <summary>
        /// Method used to update a group (database)
        /// </summary>
        /// <param name="group">object group</param>
        /// <returns>DatabaseErrorType</returns>
        public DatabaseErrorType UpdateItem(object group)
        {
            DatabaseErrorType dbError;
            var groupToAdd = group as ModelClass.Group;

            try
            {
                // Open connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();

                //SQL to add task to table, also adding task to profiletask table
                var sql = "UPDATE groups SET groupname = '" + $"{groupToAdd.GroupName}" + "', groupdescription = '" + $"{groupToAdd.GroupDescription}" + "', adminprofileid = '" + $"{groupToAdd.AdminProfileID}" + "' WHERE groupid = '" + $"{groupToAdd.GroupID}" + "';";

                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            }
            catch (NpgsqlException ex)
            {
                //Something went wrong adding the task
                Console.WriteLine("Unexpected error while updating group: {0}", ex);
                dbError = DatabaseErrorType.UpdateGroupDBError;
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object task)
        {
            throw new NotImplementedException();
        }

        //Currently not needed
        public DatabaseErrorType LookupItem(object task)
        {
            throw new NotImplementedException();
        }

        //Currently not needed
        public DatabaseErrorType LookupFullItem(object profile)
        {
            throw new NotImplementedException();
        }
        
        //Currently not needed
        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args)
        {
            throw new NotImplementedException();
        }

        //Currently not needed
        public object FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}

