using AcademicReward.Database;
using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicReward.Logic {
    internal class ProfileLogic : ILogic{
        IDatabase profileDB;
        public ProfileLogic() {
            profileDB = new ProfileDatabase();
        }
        public LogicErrorType AddItem(object obj) {
            return LogicErrorType.NoError;
        }
        public LogicErrorType UpdateItem(object obj) {
            LogicErrorType logicError;
            Profile profileToUpdate = profile as Profile;
            
                
                //Making database call to update salt and password (hash)
                DatabaseErrorType dbError = profileDB.UpdateItem(profileToUpdate);
                if (DatabaseErrorType.NoError == dbError) {
                    logicError = LogicErrorType.NoError;
                }
                else {
                    logicError = LogicErrorType.UpdatePasswordDBError;
                }
            
            return logicError;
        }
        public LogicErrorType DeleteItem(object obj) {
            return LogicErrorType.NoError;
        }
        public LogicErrorType LookupItem(object obj) {
            return LogicErrorType.NoError;
        }
    }
}
