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
        public LogicErrorType AddItem(object group) {
            LogicErrorType logicError;
            Group groupToAdd = group as Group;
            //Checking user input
            logicError = AddGroupCheck(groupToAdd);
            if(LogicErrorType.NoError == logicError) {
                // Add to database
                DatabaseErrorType dbError = GroupDB.AddItem(groupToAdd);
                if(DatabaseErrorType.NoError == dbError) {
                    IDatabase historyDB = new HistoryDatabase();
                    historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryCreateGroupTitle, string.Format(DataConstants.HistoryCreateGroupDescription, groupToAdd.GroupName)));
                } else {
                    logicError = LogicErrorType.GroupCreateError;
                }
            }
            return logicError;
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

        /// <summary>
        /// Helper method used to check a group before adding it to the database
        /// </summary>
        /// <param name="group">Group group</param>
        /// <returns>LogicErrorType logicError</returns>
        private LogicErrorType AddGroupCheck(Group group) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(group.GroupName)) {
                logicError = LogicErrorType.EmptyGroupName;
            } else if(string.IsNullOrEmpty(group.GroupDescription)) {
                logicError = LogicErrorType.EmptyGroupDescription;
            } else if(CheckGroupNameLength(group.GroupName)) {
                logicError = LogicErrorType.InvalidGroupNameLength;
            } else if(CheckGroupDescriptionLength(group.GroupDescription)) {
                logicError = LogicErrorType.InvalidGroupDescriptionLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check the group name length
        /// </summary>
        /// <param name="groupName">string groupName</param>
        /// <returns>true/false</returns>
        private bool CheckGroupNameLength(string groupName) {
            int groupNameLength = groupName.Length;
            return groupNameLength < Group.MinGroupNameLength || groupNameLength > Group.MaxGroupNameLength;
        }

        /// <summary>
        /// Helper method used to check the group description length
        /// </summary>
        /// <param name="groupDescription">string groupDescription</param>
        /// <returns>true/false</returns>
        private bool CheckGroupDescriptionLength(string groupDescription) {
            int groupDescriptionLength = groupDescription.Length;
            return groupDescriptionLength < Group.MinGroupDescriptionLength || groupDescriptionLength > Group.MaxGroupDescriptionLength;
        }
    }
}
