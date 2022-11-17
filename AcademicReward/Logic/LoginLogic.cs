using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;

namespace AcademicReward.Logic {
    public class LoginLogic : ILogic {
        private IDatabase loginDB;

        /// <summary>
        /// LoginLogic constructor
        /// </summary>
        public LoginLogic() {
            loginDB = new LoginDatabase();
        }

        public LogicErrorType AddItem(object profile) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType UpdateItem(object profile) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object profile) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType LookupItem(object signInProfile) {
            //Convert object to Profile type
            Profile profile = signInProfile as Profile;
            LogicErrorType signInProfileType = SignInProfileCheck(profile);
            //Make some database call here...
            if(signInProfileType == LogicErrorType.NoError) {

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
    }
}
