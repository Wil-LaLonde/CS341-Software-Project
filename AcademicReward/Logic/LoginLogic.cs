using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System.Security.Cryptography;

namespace AcademicReward.Logic {
    public class LoginLogic : ILogic {
        private const int HashIterationIndex = 0;
        private const int HashIndex = 1;
        private const int SaltIndex = 2;
        private const int SaltByteSize = 36;
        private const int HashIterationCount = 1500;

        private IDatabase loginDB;

        /// <summary>
        /// LoginLogic constructor
        /// </summary>
        public LoginLogic() {
            loginDB = new LoginDatabase();
        }

        /// <summary>
        /// Method for adding a new profile
        /// </summary>
        /// <param name="profile">(Profile)object profile</param>
        /// <returns></returns>
        public LogicErrorType AddItem(object profile) {
            Profile profileToAdd = profile as Profile;
            //Perform checks here to see if the profile is valid or not
            string storedPassword = GenerateStoredPassword(profileToAdd.Password);
            //Gathing all the password parts
            string[] storedPasswordParts = storedPassword.Split(DataConstants.Colon);
            profileToAdd.Password = storedPasswordParts[HashIndex];
            profileToAdd.Salt = storedPasswordParts[SaltIndex];
            //Making our database call to store away our new profile
            DatabaseErrorType dbError = loginDB.AddItem(profileToAdd);
            //Need to make some checks to see if it is valid first
            return dbError == DatabaseErrorType.NoError ? LogicErrorType.NoError : LogicErrorType.AddProfileDBError;
        }

        public LogicErrorType UpdateItem(object profile) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object profile) {
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method for signing a user in
        /// </summary>
        /// <param name="signInProfile"></param>
        /// <returns></returns>
        public LogicErrorType LookupItem(object signInProfile) {
            //Convert object to Profile type
            Profile profile = signInProfile as Profile;
            LogicErrorType signInProfileType = SignInProfileCheck(profile);
            //Make some database call here...
            if(LogicErrorType.NoError == signInProfileType) {
                DatabaseErrorType dbError = loginDB.LookupItem(signInProfile);
                if (dbError == DatabaseErrorType.NoError) {
                    //Checking if passwords match
                    if(IsValidLogin(MauiProgram.Profile.Salt, MauiProgram.Profile.Password, profile.Password)) {
                        //Need to make one more database call to get all user information
                        dbError = loginDB.LookupFullItem(profile);
                        if(DatabaseErrorType.LoginProfileDBError == dbError) {
                            signInProfileType = LogicErrorType.SignProfileInDBError;
                        }
                    } else {
                        signInProfileType = LogicErrorType.PasswordIncorrect;
                    }
                } else {
                    //Something else here to see if the user name was wrong
                }
            }
            return signInProfileType;
        }

        private LogicErrorType SignInProfileCheck(Profile profile) {
            LogicErrorType signInType;
            if(string.IsNullOrEmpty(profile.Username)) {
                signInType = LogicErrorType.EmptyUsername;
            } else if(string.IsNullOrEmpty(profile.Password)) {
                signInType = LogicErrorType.EmptyPassword;
            } else {
                signInType = LogicErrorType.NoError;
            }
            return signInType;
        }

        private LogicErrorType AddProfileCheck() {
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Helper method that creates a new salt and prepares
        /// the string values that will be stored away
        /// 
        /// The return string will be in the format of
        /// 1. Iteration count
        /// 2. Salt
        /// 3. Hash
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns>string storedPassword</returns>
        private string GenerateStoredPassword(string password) {
            //Creating main salt storage bytes
            RNGCryptoServiceProvider saltStorage = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            saltStorage.GetBytes(salt);
            //Creating the our hash
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, salt, HashIterationCount);
            byte[] hash = hasher.GetBytes(SaltByteSize);
            //Creating the encrypted string
            string storedPassword = HashIterationCount + DataConstants.Colon +
                                    Convert.ToBase64String(hash) + DataConstants.Colon +
                                    Convert.ToBase64String(salt);
            return storedPassword;
        }

        private bool IsValidLogin(string storedSalt, string storedPassword, string testPassword) {
            //Gathering string bytes
            byte[] originalSalt = Convert.FromBase64String(storedSalt);
            byte[] originalHash = Convert.FromBase64String(storedPassword);
            //Getting hash of current password with stored salt
            Rfc2898DeriveBytes hashTool = new Rfc2898DeriveBytes(testPassword, originalSalt, HashIterationCount);
            byte[] testHash = hashTool.GetBytes(SaltByteSize);
            string test = Convert.ToBase64String(testHash);
            //Checking if there is a mismatch
            uint differences = (uint)originalHash.Length ^ (uint)testHash.Length;
            for (int position = 0; position < Math.Min(originalHash.Length, testHash.Length); position++)
                differences |= (uint)(originalHash[position] ^ testHash[position]);
            return differences == 0;
        }
    }
}
