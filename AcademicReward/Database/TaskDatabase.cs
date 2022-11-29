using AcademicReward.Resources;

namespace AcademicReward.Database {
    public class TaskDatabase : AcademicRewardsDatabase, IDatabase {

        public TaskDatabase() { }

        public DatabaseErrorType AddItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType UpdateItem(object task) {
            return DatabaseErrorType.NoError;
        }

        //Currently not needed
        public DatabaseErrorType DeleteItem(object task) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupItem(object task) {
            return DatabaseErrorType.NoError;
        }

        public DatabaseErrorType LookupFullItem(object task) {
            return DatabaseErrorType.NoError;
        }
    }
}
