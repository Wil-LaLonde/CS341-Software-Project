using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicReward.Logic {
    /// <summary>
    /// Primary Author: Xee Lo
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    internal class ProfileLogic : ILogic{
        IDatabase profileDB;
        public ProfileLogic() {
            profileDB = new ProfileDatabase();
        }
        public LogicErrorType AddItem(object obj) {
            return LogicErrorType.NoError;
        }
        public LogicErrorType UpdateItem(object profile) {
            LogicErrorType logicError;
            Profile profileToUpdate = profile as Profile;
            
            //Making database call to update profile XP,Points,and Level
            DatabaseErrorType dbError = profileDB.UpdateItem(profileToUpdate);
            if (DatabaseErrorType.NoError == dbError) {
                logicError = LogicErrorType.NoError;
            }
            else {
                logicError = LogicErrorType.UpdateProfileDBError;
            }
            
            return logicError;
        }
        public LogicErrorType DeleteItem(object obj) {
            return LogicErrorType.NoError;
        }
        public LogicErrorType LookupItem(object obj) {
            return LogicErrorType.NoError;
        }
        public LogicErrorType AddItemWithArgs(object[] obj) {
            return LogicErrorType.NoError;
        }
    }
}
