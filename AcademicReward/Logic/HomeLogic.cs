using AcademicReward.Resources;
using AcademicReward.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicReward.ModelClass;

namespace AcademicReward.Logic {

    /// <summary>
    /// Primary Author: Xee Lo
    /// Secondary Author: Wil LaLonde
    /// Reviewer: 
    /// </summary>
    class HomeLogic : ILogic {

        private IDatabase homeDatabase;
        public HomeLogic() { 
            homeDatabase = new HomeDatabase();
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
        /// Method used to look up tasks for a group
        /// </summary>
        /// <param name="group">object group</param>
        /// <returns>LogicErrorType</returns>
        public LogicErrorType LookupItem(object group) {
            LogicErrorType logicError;
            DatabaseErrorType dbError = homeDatabase.LookupFullItem(group);
            if (DatabaseErrorType.NoError == dbError) {
                logicError = LogicErrorType.NoError;
            }
            else {
                logicError = LogicErrorType.LookupAllTasksDBError;
            }
            return logicError;
        }
    }
}
