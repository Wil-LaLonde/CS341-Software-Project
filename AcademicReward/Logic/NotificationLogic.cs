using AcademicReward.Database;
using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicReward.Logic {
    public class NotificationLogic : ILogic {
        IDatabase notificationDB;

        /// <summary>
        /// NotificationLogic constructor
        /// </summary>
        public NotificationLogic() { notificationDB = new NotificationDatabase(); }

        public LogicErrorType AddItem(object notification) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType UpdateItem(object notification) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object notification) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType LookupItem(object notification) {
            return LogicErrorType.NoError;
        }
    }
}
