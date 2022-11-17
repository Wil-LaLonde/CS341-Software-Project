using AcademicReward.Resources;

namespace AcademicReward.Database {
    public class LoginDatabase : AcademicRewardsDatabase, IDatabase {

        /// <summary>
        /// LoginDatabase constructor
        /// </summary>
        public LoginDatabase() { }

        public DatabaseErrorType AddItem(object profile) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType UpdateItem(object profile) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType DeleteItem(object profile) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupItem(object profile) {
            return DatabaseErrorType.NoError;
        }
    }
}
