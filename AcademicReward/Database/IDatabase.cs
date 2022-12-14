using AcademicReward.Resources;
using System.Collections.ObjectModel;

namespace AcademicReward.Database {
    /// <summary>
    /// IDatabase interface
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None 
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public interface IDatabase {
        public DatabaseErrorType AddItem(object obj);
        public DatabaseErrorType UpdateItem(object obj);
        public DatabaseErrorType DeleteItem(object obj);
        public DatabaseErrorType LookupItem(object obj);
        public DatabaseErrorType LookupFullItem(object obj);
        public DatabaseErrorType LoadItems(ObservableCollection<object> obj, string[] args);
        public Object FindById(int id);
    }
}
