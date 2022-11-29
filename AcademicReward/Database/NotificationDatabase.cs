using AcademicReward.Resources;

namespace AcademicReward.Database {
    public class NotificationDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// NotificationDatbase constructor
        /// </summary>
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
