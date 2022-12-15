using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     GroupDatabase controls anything related to groups in the database
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public class GroupDatabase : AcademicRewardsDatabase, IDatabase {
    /// <summary>
    ///     GroupDatabase constructor
    /// </summary>
    public GroupDatabase() { }

    /// <summary>
    ///     Method used to add a new group (database)
    /// </summary>
    /// <param name="group">object group</param>
    /// <returns>DatabaseErrorType</returns>
    public DatabaseErrorType AddItem(object group) {
        DatabaseErrorType dbError;
        Group groupToAdd = group as Group;

        try {
            // Open connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();

            // SQL to add group to table and return created row
            string sql = "INSERT INTO groups (groupname, groupdescription, adminprofileid)" +
                " VALUES ('" + $"{groupToAdd.GroupName}" + "', '" + $"{groupToAdd.GroupDescription}" + "', '" +
                $"{groupToAdd.GroupAdmin.ProfileID}" + "') RETURNING groupid;";

            // Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Closing the connection.
            dbError = DatabaseErrorType.NoError;

            // Getting the group id from the created row
            int id = 0;
            while (reader.Read()) id = reader.GetInt32(0);

            con.Close();
            dbError = DatabaseErrorType.NoError;

            groupToAdd.GroupID = id;
            GroupProfileRelationship.addProfileToGroup(MauiProgram.Profile, groupToAdd);

            groupToAdd.AdminProfileID = MauiProgram.Profile.ProfileID;
            MauiProgram.Profile.AddGroupToProfile(groupToAdd); // Update list for current profile
        }
        catch (NpgsqlException ex) {
            //Something went wrong adding the group
            Console.WriteLine("Unexpected error while adding group: {0}", ex);
            dbError = DatabaseErrorType.AddGroupDBError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to update a group (database)
    /// </summary>
    /// <param name="group">object group</param>
    /// <returns>DatabaseErrorType</returns>
    public DatabaseErrorType UpdateItem(object group) {
        DatabaseErrorType dbError;
        Group groupToAdd = group as Group;

        try {
            // Open connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();

            //SQL to add task to table, also adding task to profiletask table
            string sql = "UPDATE groups SET groupname = '" + $"{groupToAdd.GroupName}" + "', groupdescription = '" +
                $"{groupToAdd.GroupDescription}" + "', adminprofileid = '" + $"{groupToAdd.AdminProfileID}" +
                "' WHERE groupid = '" + $"{groupToAdd.GroupID}" + "';";

            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Something went wrong updating the group
            Console.WriteLine("Unexpected error while updating group: {0}", ex);
            dbError = DatabaseErrorType.UpdateGroupDBError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType DeleteItem(object group) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType LookupItem(object group) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType LookupFullItem(object group) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
        return DatabaseErrorType.NotImplemented;
    }

    //Currently not needed
    public object FindById(int id) {
        return DatabaseErrorType.NotImplemented;
    }
}