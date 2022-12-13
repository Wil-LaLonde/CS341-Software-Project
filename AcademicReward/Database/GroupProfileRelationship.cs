using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {
    /// <summary>
    /// GroupProfileRelationship is used to get groups for a given profile from the database
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class GroupProfileRelationship : AcademicRewardsDatabase {

        /// <summary>
        /// Method used to gather all profiles for a given group
        /// </summary>
        /// <param name="group">Group group</param>
        /// <returns>ObservableCollection profiles</returns>
        public static ObservableCollection<Profile> getProfilesInGroup(Group group) {
            try {
                int groupId = group.GroupID;
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Select SQL query for getting all profiles
                var sql = "SELECT * " +
                    "FROM profiles " +
                    "WHERE profileid IN (SELECT profileid" +
                    "                    FROM profilegroup" +
                    "                    WHERE groupid = " + $"{groupId}" + ");";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Reading the data
                ObservableCollection<Profile> profiles = new ObservableCollection<Profile>();

                while (reader.Read()) {
                    profiles.Add(new Profile(
                        reader.GetString(1), // Username
                        reader.GetString(7) // Password
                    ));
                }

                //Closing the connection.
                con.Close();

                return profiles;
            }
            catch (Exception e) {
                //Groupname already exists.
                Console.WriteLine("Error while getting all profiles from group: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// Method used to add a profile and group to the database (join table)
        /// </summary>
        /// <param name="profile">Profile profile</param>
        /// <param name="group">Group group</param>
        /// <returns>LogicErrorType logicError</returns>
        public static LogicErrorType addProfileToGroup(Profile profile, Group group) {
            try {

                if (checkAdminExists(group) && profile.IsAdmin) {
                    // Group already has an admin
                    Console.WriteLine("Error while adding profile to group: Admin already exists");
                    return LogicErrorType.GroupAlreadyHasAdmin;
                }
                else {
                    int profileId = profile.ProfileID;
                    int groupId = group.GroupID;
                    using var con = new NpgsqlConnection(InitializeConnectionString());
                    con.Open();
                    //Select SQL query for getting all profiles
                    var sql = "INSERT INTO profilegroup (profileid, groupid) " +
                        "VALUES (" + $"{profileId}" + ", " + $"{groupId}" + ");";
                    //Executing the query.
                    using var cmd = new NpgsqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    //Closing the connection.
                    con.Close();
                    return LogicErrorType.NoError;
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error while adding profile to group: {0}", e);
                return LogicErrorType.GroupCreateError;
            }
        }

        /// <summary>
        /// Method used to find a user by their username in the database
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>Profile profile</returns>
        public static Profile findByUsername(string username) {
            try {
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Select SQL query for getting all profiles
                var sql = "SELECT * " +
                    "FROM profiles " +
                    "WHERE username = '" + $"{username}" + "';";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Reading the data
                Profile profile = null;

                while (reader.Read()) {
                    profile = new Profile(
                        reader.GetInt32(0), // ProfileID
                        reader.GetString(1), // Username
                        reader.GetInt32(2), // XP
                        reader.GetInt32(3), // Points
                        reader.GetInt32(4), // Level
                        reader.GetBoolean(5), // IsAdmin
                        reader.GetString(6), // Salt
                        reader.GetString(7) // Password
                    );
                }

                //Closing the connection.
                con.Close();

                return profile;
            }
            catch (Exception e) {
                //Groupname already exists.
                Console.WriteLine("Error while getting profile by username: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// Method used to check if trying to add an admin to a group
        /// </summary>
        /// <param name="group">Group group</param>
        /// <returns>true/false</returns>
        public static bool checkAdminExists(Group group) {
            try {
                int groupId = group.GroupID;
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Select SQL query for getting all profiles
                var sql = "SELECT * " +
                    "FROM profiles " +
                    "WHERE profileid IN (SELECT profileid" +
                    "                    FROM profilegroup" +
                    "                    WHERE groupid = " + $"{groupId}" + ");";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                //Reading the data
                ObservableCollection<Profile> profiles = new ObservableCollection<Profile>();

                while (reader.Read()) {
                    profiles.Add(new Profile(
                        reader.GetInt32(0), // ProfileID
                        reader.GetString(1), // Username
                        reader.GetInt32(2), // XP
                        reader.GetInt32(3), // Points
                        reader.GetInt32(4), // Level
                        reader.GetBoolean(5), // IsAdmin
                        reader.GetString(6), // Salt
                        reader.GetString(7) // Password
                    ));
                }

                //Closing the connection.
                con.Close();

                foreach (Profile profile in profiles) {
                    if (profile.IsAdmin) {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception e) {
                Console.WriteLine("Error while getting all profiles from group: {0}", e);
                return false;
            }
        }
    }
}
