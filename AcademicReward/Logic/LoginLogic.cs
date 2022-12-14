using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System.Security.Cryptography;

namespace AcademicReward.Logic {
    /// <summary>
    /// LoginLogic is the logic behind adding profiles, signing profiles in, and updating profiles (password)
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class LoginLogic : ILogic {
        private const int PasswordIndex = 0;
        private const int SaltIndex = 1;
        private const int SaltByteSize = 36;
        private const int HashIterationCount = 1500;

        private IDatabase loginDB;

        /// <summary>
        /// LoginLogic constructor
        /// </summary>
        public LoginLogic() { loginDB = new LoginDatabase(); }

        /// <summary>
        /// Method for adding a new profile
        /// </summary>
        /// <param name="profile">(Profile)object profile</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType AddItem(object profile) {
            LogicErrorType logicError;
            Profile profileToAdd = profile as Profile;
            //Checking the profile trying to be added
            logicError = AddProfileCheck(profileToAdd);
            if(LogicErrorType.NoError == logicError) {
                //Perform checks here to see if the profile is valid or not
                string storedPassword = GenerateStoredPassword(profileToAdd.Password);
                //Gathing all the password parts
                string[] storedPasswordParts = storedPassword.Split(DataConstants.Colon);
                profileToAdd.Password = storedPasswordParts[PasswordIndex];
                profileToAdd.Salt = storedPasswordParts[SaltIndex];
                //Making our database call to store away our new profile
                DatabaseErrorType dbError = loginDB.AddItem(profileToAdd);
                //Need to make some checks to see if it is valid first
                if (DatabaseErrorType.NoError == dbError) {
                    logicError = LogicErrorType.NoError;
                } else if(DatabaseErrorType.UsernameTakenDBError == dbError) {
                    logicError = LogicErrorType.UsernameTaken;
                } else {
                    logicError = LogicErrorType.AddProfileDBError;
                }
            }
            return logicError;
        }

        /// <summary>
        /// Method used to update a profile's password
        /// </summary>
        /// <param name="profile">object profile</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType UpdateItem(object profile) {
            LogicErrorType logicError;
            Profile profileToUpdate = profile as Profile;
            logicError = UpdatePasswordCheck(profileToUpdate);
            if(LogicErrorType.NoError == logicError) {
                //Gathering new salt and hash
                string storedPassword = GenerateStoredPassword(profileToUpdate.NewPassword);
                //Gathing all the password parts
                string[] storedPasswordParts = storedPassword.Split(DataConstants.Colon);
                profileToUpdate.Password = storedPasswordParts[PasswordIndex];
                profileToUpdate.Salt = storedPasswordParts[SaltIndex];
                //Making database call to update salt and password (hash)
                DatabaseErrorType dbError = loginDB.UpdateItem(profileToUpdate);
                if(DatabaseErrorType.NoError == dbError) {
                    logicError = LogicErrorType.NoError;
                } else {
                    logicError = LogicErrorType.UpdatePasswordDBError;
                }
            }
            return logicError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object profile) {
            return LogicErrorType.NotImplemented;
        }

        /// <summary>
        /// Method for signing a user in
        /// </summary>
        /// <param name="signInProfile">(Profile)object signInProfile</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType LookupItem(object signInProfile) {
            LogicErrorType logicError;
            Profile profile = signInProfile as Profile;
            //First check user input
            logicError = SignInProfileCheck(profile);
            if(LogicErrorType.NoError == logicError) {
                //Looking up profile info from database.
                DatabaseErrorType dbError = loginDB.LookupItem(signInProfile);
                if(DatabaseErrorType.NoError == dbError) {
                    //Checking if passwords match
                    if(IsValidLogin(MauiProgram.Profile.Salt, MauiProgram.Profile.Password, profile.Password)) {
                        //Need to make one more database call to get all user information
                        dbError = loginDB.LookupFullItem(profile);
                        if(DatabaseErrorType.NoError != dbError) {
                            logicError = LogicErrorType.SignProfileInDBError;
                        }
                    } else {
                        logicError = LogicErrorType.PasswordIncorrect;
                    }
                } else if(DatabaseErrorType.UsernameNotFoundDBError == dbError) {
                    logicError = LogicErrorType.UsernameNotFound;
                } else {
                    logicError = LogicErrorType.SignProfileInDBError;
                }
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check user input while signing an account in
        /// </summary>
        /// <param name="profile">Profile profile</param>
        /// <returns>LogicErrorType logicError</returns>
        private LogicErrorType SignInProfileCheck(Profile profile) {
            LogicErrorType logicError;
            if (string.IsNullOrEmpty(profile.Username)) {
                logicError = LogicErrorType.EmptyUsername;
            } else if (string.IsNullOrEmpty(profile.Password)) {
                logicError = LogicErrorType.EmptyPassword;
            } else if (CheckUsernameLength(profile.Username)) {
                logicError = LogicErrorType.InvalidUsernameLength;
            } else if (CheckPasswordLength(profile.Password)) {
                logicError = LogicErrorType.InvalidPasswordLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check user input while adding an account
        /// </summary>
        /// <param name="profile">Profile profile</param>
        /// <returns>LogicErrorType logicError</returns>
        private LogicErrorType AddProfileCheck(Profile profile) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(profile.Username)) {
                logicError = LogicErrorType.EmptyUsername;
            } else if(string.IsNullOrEmpty(profile.Password)) {
                logicError = LogicErrorType.EmptyPassword;
            } else if(string.IsNullOrEmpty(profile.ReEnterPassword)) {
                logicError = LogicErrorType.EmptyReEnterPassword;
            } else if(!profile.Password.Equals(profile.ReEnterPassword)) {
                logicError = LogicErrorType.PasswordMismatch;
            } else if(CheckUsernameLength(profile.Username)) {
                logicError = LogicErrorType.InvalidUsernameLength;
            } else if(CheckPasswordLength(profile.Password)) {
                logicError = LogicErrorType.InvalidPasswordLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to help update a user's password
        /// </summary>
        /// <param name="profile">Profile profile</param>
        /// <returns>LogicErrorType logicError</returns>
        private LogicErrorType UpdatePasswordCheck(Profile profile) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(profile.Password)) {
                logicError = LogicErrorType.EmptyOldPassword;
            } else if(string.IsNullOrEmpty(profile.NewPassword)) {
                logicError = LogicErrorType.EmptyNewPassword;
            } else if(string.IsNullOrEmpty(profile.ReEnterPassword)) {
                logicError = LogicErrorType.EmptyReEnterNewPassword;
            } else if(!profile.NewPassword.Equals(profile.ReEnterPassword)) {
                logicError = LogicErrorType.PasswordMismatch;
            }else if(CheckPasswordLength(profile.Password)) {
                logicError = LogicErrorType.InvalidOldPasswordLength;
            } else if(CheckPasswordLength(profile.NewPassword)) {
                logicError = LogicErrorType.InvalidNewPasswordLength;
            } else if(CheckPasswordLength(profile.ReEnterPassword)) {
                logicError = LogicErrorType.InvalidReEnterNewPasswordLength;
            } else if(!IsValidLogin(MauiProgram.Profile.Salt, MauiProgram.Profile.Password, profile.Password)) {
                //checking if the old password does not match the current
                logicError = LogicErrorType.PasswordIncorrect;
            } else if(IsValidLogin(MauiProgram.Profile.Salt, MauiProgram.Profile.Password, profile.NewPassword)) {
                //checking if the new password matches the current
                logicError = LogicErrorType.CurrentPasswordError;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method to check the given username length
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>bool true/false</returns>
        private bool CheckUsernameLength(string username) {
            int usernameLength = username.Length;
            return usernameLength < Profile.MinUsernameLength || usernameLength > Profile.MaxUsernameLength;
        }

        /// <summary>
        /// Helper method to check the given password length
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns>bool true/false</returns>
        private bool CheckPasswordLength(string password) {
            int passwordLength = password.Length;
            return passwordLength < Profile.MinPasswordLength || passwordLength > Profile.MaxPasswordLength;
        }

        /// <summary>
        /// Helper method that creates a new salt and prepares
        /// the string values that will be stored away
        /// 
        /// The return string will be in the format of: hash:salt
        /// Index 0 -> hash
        /// Index 1 -> salt
        /// 
        /// Main Resource:
        /// https://visualstudiomagazine.com/articles/2016/12/01/hashing-passwords.aspx
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns>string hash:salt</returns>
        public string GenerateStoredPassword(string password) {
            //Creating main salt storage bytes
            RNGCryptoServiceProvider saltStorage = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            saltStorage.GetBytes(salt);
            //Creating the hash
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, salt, HashIterationCount);
            byte[] hash = hasher.GetBytes(SaltByteSize);
            return Convert.ToBase64String(hash) + DataConstants.Colon + Convert.ToBase64String(salt); ;
        }

        /// <summary>
        /// Helper method used to determine if a given password
        /// matches what is stored in our database.
        /// 
        /// Main Resource:
        /// https://visualstudiomagazine.com/articles/2016/12/01/hashing-passwords.aspx
        /// </summary>
        /// <param name="storedSalt">string storedSalt</param>
        /// <param name="storedPassword">string storedPassword</param>
        /// <param name="testPassword">string testPassword</param>
        /// <returns>bool true/false</returns>
        public bool IsValidLogin(string storedSalt, string storedPassword, string testPassword) {
            //Gathering string bytes
            byte[] storedSaltBytes = Convert.FromBase64String(storedSalt);
            byte[] storedPasswordBytes = Convert.FromBase64String(storedPassword);
            //Getting hash of current password with stored salt
            Rfc2898DeriveBytes hashTool = new Rfc2898DeriveBytes(testPassword, storedSaltBytes, HashIterationCount);
            byte[] testHash = hashTool.GetBytes(SaltByteSize);
            //Checking if there is a mismatch
            uint differences = (uint)storedPasswordBytes.Length ^ (uint)testHash.Length;
            for (int position = 0; position < Math.Min(storedPasswordBytes.Length, testHash.Length); position++)
                differences |= (uint)(storedPasswordBytes[position] ^ testHash[position]);
            return differences == 0;
        }

        public LogicErrorType AddItemWithArgs(object[] obj)
        {
            throw new NotImplementedException();
        }
    }
}
