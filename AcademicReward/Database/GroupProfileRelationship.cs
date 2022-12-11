using AcademicReward.ModelClass;
using Npgsql;
using System.Collections.ObjectModel;

namespace AcademicReward.Database
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    internal class GroupProfileRelationship : AcademicRewardsDatabase
    {
        public static ObservableCollection<Profile> getProfilesInGroup(Group group)
        {
            try
            {
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

                while (reader.Read())
                {
                    profiles.Add(new Profile(
                        reader.GetString(1), // Username
                        reader.GetString(7) // Password
                    ));
                }

                //Closing the connection.
                con.Close();

                return profiles;
            }
            catch (Exception e)
            {
                //Groupname already exists.
                Console.WriteLine("Error while getting all profiles from group: {0}", e);
                return null;
            }
        }

        public static void addProfileToGroup(Profile profile, Group group)
        {
            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while adding profile to group: {0}", e);
            }
        }
    }
}
