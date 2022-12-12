using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: 
    /// Reviewer: 
    /// </summary>
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

        public LogicErrorType AddItemWithArgs(object[] obj)
        {
            throw new NotImplementedException();
        }
    }
}
