using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicReward.Database {
    public class NotificationDatabase : AcademicRewardsDatabase, IDatabase {

        public NotificationDatabase() { }

        public DatabaseErrorType AddItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType UpdateItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType DeleteItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupItem(object notification) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupFullItem(object notification) {
            return DatabaseErrorType.NoError;
        }
    }
}
