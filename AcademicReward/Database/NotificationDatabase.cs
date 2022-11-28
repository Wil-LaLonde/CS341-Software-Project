using AcademicReward.Resources;

namespace AcademicReward.Database {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
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
