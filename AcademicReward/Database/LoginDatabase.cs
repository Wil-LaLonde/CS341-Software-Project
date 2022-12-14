using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;

namespace AcademicReward.Database; 

/// <summary>
///     LoginDatabase is used to sign users in, create new profiles, and update profiles in the database
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class LoginDatabase : AcademicRewardsDatabase, IDatabase {
    /// <summary>
    ///     LoginDatabase constructor
    /// </summary>
    public LoginDatabase() { }

    /// <summary>
    ///     Method used to add a new profile
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType AddItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileToAdd = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Insert SQL query for adding a profile
            string sql = "INSERT INTO Profiles (username, xp, points, level, isadmin, salt, password)" +
                $"VALUES ('{profileToAdd.Username}', {profileToAdd.Xp}, {profileToAdd.Points}, {profileToAdd.Level}, {profileToAdd.IsAdmin}, '{profileToAdd.Salt}', '{profileToAdd.Password}');";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (PostgresException ex) {
            //Username already exists.
            Console.WriteLine("Error while adding profile: {0}", ex);
            dbError = DatabaseErrorType.UsernameTakenDbError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while adding profile: {0}", ex);
            dbError = DatabaseErrorType.AddProfileDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to update a profile's password
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType UpdateItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileToUpdate = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Insert SQL query for adding a profile
            string sql = "UPDATE profiles " +
                $"SET salt = '{profileToUpdate.Salt}', password = '{profileToUpdate.Password}' " +
                $"WHERE profileid = {MauiProgram.Profile.ProfileId};";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            //Closing the connection.
            con.Close();
            MauiProgram.Profile.Salt = profileToUpdate.Salt;
            MauiProgram.Profile.Password = profileToUpdate.Password;
            dbError = DatabaseErrorType.NoError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while updating profile: {0}", ex);
            dbError = DatabaseErrorType.UpdatePasswordDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType DeleteItem(object profile) {
        return DatabaseErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method used to look up a profile
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType LookupItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileToLogin = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Getting the username, salt, and password
            string sql = "SELECT username, salt, password " +
                "FROM Profiles " +
                $"WHERE username = '{profileToLogin.Username}';";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            //Creating temp profile to check password entered
            //[0] -> username | [1] -> salt | [2] password
            MauiProgram.Profile = new Profile(reader[0] as string, reader[1] as string, reader[2] as string);
            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (InvalidOperationException ex) {
            //Username given does not exist
            Console.WriteLine("Error while signing profile in: {0}", ex);
            dbError = DatabaseErrorType.UsernameNotFoundDbError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while signing profile in: {0}", ex);
            dbError = DatabaseErrorType.LoginProfileDbError;
        }

        return dbError;
    }

    /// <summary>
    ///     Method used to look up a full profile
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>DatabaseErrorType dbError</returns>
    public DatabaseErrorType LookupFullItem(object profile) {
        DatabaseErrorType dbError;
        Profile profileToLogin = profile as Profile;
        try {
            //Opening the connection
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();
            //Getting all user information after validating
            string sql = "SELECT * " +
                "FROM Profiles " +
                $"WHERE username = '{profileToLogin.Username}';";
            //Executing the query.
            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            //Creating full profile
            //[0] -> ProfileID | [1] -> username | [2] -> xp | [3] -> points
            //[4] -> level | [5] -> isAdmin | [6] -> salt | [7] -> password
            MauiProgram.Profile = new Profile((int)reader[0], reader[1] as string, (int)reader[2],
                (int)reader[3], (int)reader[4], (bool)reader[5], reader[6] as string, reader[7] as string);
            //Closing the connection.
            con.Close();
            dbError = DatabaseErrorType.NoError;
        }
        catch (InvalidOperationException ex) {
            //Username given does not exist
            Console.WriteLine("Error while signing profile in: {0}", ex);
            dbError = DatabaseErrorType.UsernameNotFoundDbError;
        }
        catch (NpgsqlException ex) {
            //Not sure what happened, log message
            Console.WriteLine("Unexpected error while signing profile in: {0}", ex);
            dbError = DatabaseErrorType.LoginProfileDbError;
        }

        return dbError;
    }

    //Currently not needed
    public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args) {
        return DatabaseErrorType.NotImplemented;
    }

    /// <summary>
    ///     Gathering profile information based off the profileid
    /// </summary>
    /// <param name="id">int id</param>
    /// <returns>object profile</returns>
    public object FindById(int id) {
        try {
            using NpgsqlConnection con = new NpgsqlConnection(InitializeConnectionString());
            con.Open();

            string sql = "SELECT * " +
                "FROM Profiles " +
                $"WHERE profileid= '{id}';";

            using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            // Load into a new profile object
            reader.Read();
            Profile profile = new((int)reader[0], reader[1] as string, (int)reader[2],
                (int)reader[3], (int)reader[4], (bool)reader[5], reader[6] as string, reader[7] as string);

            con.Close();

            return profile;
        }
        catch (Exception e) {
            Console.WriteLine("Error while querying profile by id in: {0}", e);
            return null;
        }
    }
}