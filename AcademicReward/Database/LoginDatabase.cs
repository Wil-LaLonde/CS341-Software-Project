using AcademicReward.Resources;
using AcademicReward.ModelClass;
using Npgsql;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class LoginDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// LoginDatabase constructor
        /// </summary>
        public LoginDatabase() { }

        public DatabaseErrorType AddItem(object profile) {
            DatabaseErrorType dbError;
            Profile profileToAdd = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Insert SQL query for adding a profile
                var sql = "INSERT INTO Profiles (username, xp, points, level, isadmin, salt, password)" +
                          $"VALUES ('{profileToAdd.Username}', {profileToAdd.XP}, {profileToAdd.Points}, {profileToAdd.Level}, {profileToAdd.IsAdmin}, '{profileToAdd.Salt}', '{profileToAdd.Password}');";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            } catch(PostgresException ex) {
                //Username already exists.
                Console.WriteLine("Error while adding profile: {0}", ex);
                dbError = DatabaseErrorType.UsernameTakenDBError;
            } catch(NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while adding profile: {0}", ex);
                dbError = DatabaseErrorType.AddProfileDBError;
            }
            return dbError;
        }

        //Currently not needed
        public DatabaseErrorType UpdateItem(object profile) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object profile) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupItem(object profile) {
            DatabaseErrorType dbError;
            Profile profileToLogin = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Getting the username, salt, and password
                var sql = "SELECT username, salt, password " +
                          "FROM Profiles " +
                          $"WHERE username = '{profileToLogin.Username}';";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
                using NpgsqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                //Creating temp profile to check password entered
                //[0] -> username | [1] -> salt | [2] password
                MauiProgram.Profile = new Profile(reader[0] as string, reader[1] as string, reader[2] as string);
                //Closing the connection.
                con.Close();
                dbError = DatabaseErrorType.NoError;
            } catch(InvalidOperationException ex) {
                //Username given does not exist
                Console.WriteLine("Error while signing profile in: {0}", ex);
                dbError = DatabaseErrorType.UsernameNotFoundDBError;
            } catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while signing profile in: {0}", ex);
                dbError = DatabaseErrorType.LoginProfileDBError;
            }
            return dbError;
        }

        public DatabaseErrorType LookupFullItem(object profile) {
            DatabaseErrorType dbError;
            Profile profileToLogin = profile as Profile;
            try {
                //Opening the connection
                using var con = new NpgsqlConnection(InitializeConnectionString());
                con.Open();
                //Getting all user information after validating
                var sql = "SELECT * " +
                          "FROM Profiles " +
                          $"WHERE username = '{profileToLogin.Username}';";
                //Executing the query.
                using var cmd = new NpgsqlCommand(sql, con);
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
                dbError = DatabaseErrorType.UsernameNotFoundDBError;
            }
            catch (NpgsqlException ex) {
                //Not sure what happened, log message
                Console.WriteLine("Unexpected error while signing profile in: {0}", ex);
                dbError = DatabaseErrorType.LoginProfileDBError;
            }
            return dbError;
        }
    }
}
