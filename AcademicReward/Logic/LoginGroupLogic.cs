using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    public class LoginGroupLogic : ILogic {
        private IDatabase loginGroupDB;

        /// <summary>
        /// LoginGroupLogic constructor
        /// </summary>
        public LoginGroupLogic() {
            loginGroupDB = new LoginGroupDatabase();
        }

        //Currently not needed
        public LogicErrorType AddItem(object obj) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType UpdateItem(object obj) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object obj) {
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method that calls the database to gather 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public LogicErrorType LookupItem(object profile) {
            LogicErrorType logicError;
            DatabaseErrorType dbError = loginGroupDB.LookupFullItem(profile);
            if(DatabaseErrorType.NoError == dbError) {
                logicError = LogicErrorType.NoError;
            } else {
                logicError = LogicErrorType.LoginGroupCollectionDBError;
            }
            return logicError;
        }
    }
}
