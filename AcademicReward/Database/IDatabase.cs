using AcademicReward.Resources;

namespace AcademicReward.Database { 
    public interface IDatabase {
        public DatabaseErrorType AddItem(object obj);
        public DatabaseErrorType UpdateItem(object obj);
        public DatabaseErrorType DeleteItem(object obj);
        public DatabaseErrorType LookupItem(object obj);
        public DatabaseErrorType LookupFullItem(object obj); 
    }
}
