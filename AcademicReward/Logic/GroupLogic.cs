using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// GroupLogic is the logic behind anything group related
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class GroupLogic : ILogic {
        private IDatabase GroupDB;

        /// <summary>
        /// GroupLogic constructor
        /// </summary>
        public GroupLogic() {
            GroupDB = new GroupDatabase();
        }

        /// <summary>
        /// Method that adds a new Group to the database 
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType AddItem(object obj) {
            var group = obj as Group;

            // Add to database
            var dbError = GroupDB.AddItem(obj);
            if (dbError != DatabaseErrorType.NoError) {
                Console.WriteLine("Error adding group to database");
                return LogicErrorType.GroupCreateError;
            }

            // Create history entry for a new group
            var historyDB = new HistoryDatabase();
            var error = historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryCreateGroupTitle, string.Format(DataConstants.HistoryCreateGroupDescription, group.GroupName)));
            if (error != DatabaseErrorType.NoError) {
                Console.WriteLine("Error adding history item");
                return LogicErrorType.HistoryAddError;
            }

            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method used to update a group (logic)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType UpdateItem(object obj) {
            // Update database
            var dbError = GroupDB.UpdateItem(obj);
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object obj) {
            return LogicErrorType.NotImplemented;
        }

        //Currently not needed
        public LogicErrorType LookupItem(object profile) {
            return LogicErrorType.NotImplemented;
        }

        //Currently not needed
        public LogicErrorType AddItemWithArgs(object[] obj) {
            return LogicErrorType.NotImplemented;
        }
    }
}
