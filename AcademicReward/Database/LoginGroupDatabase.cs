using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    public class LoginGroupDatabase : AcademicRewardsDatabase, IDatabase {
        public LoginGroupDatabase() { }

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


        public DatabaseErrorType LookupFullItem(object profile) {
            DatabaseErrorType dbError;
            Profile loggedInProfile = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Gathering all groups for a given profile
                var sql = "SELECT * " +
                          "FROM groups " +
                          "WHERE groupid IN (SELECT a.groupid " +
                                            "FROM groups a, profilegroup b " +
                                            $"WHERE a.groupid = b.groupid AND profileid = {loggedInProfile.ProfileID});";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Creating all our group objects
                //[0] -> groupid | [1] -> groupname | [2] -> groupdescription | [3] -> adminprofileid
                while(reader.Read()) {
                    Group group = new Group((int)reader[0], reader[1] as string, reader[2] as string, (int)reader[3]);
                    MauiProgram.Profile.AddGroupToProfile(group);
                }
                con.Close();
                dbError = DatabaseErrorType.NoError;
            } catch(NpgsqlException ex) {
                //Some database error occurred
                Console.WriteLine("Unexpected error while gathering profile groups: {0}", ex);
                dbError = DatabaseErrorType.LoginGroupCollectionDBError;
            }
            return dbError;
        }
    }
}
