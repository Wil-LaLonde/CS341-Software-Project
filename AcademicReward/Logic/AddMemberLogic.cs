using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// AddMemberLogic is the logic behind adding a member to a group
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class AddMemberLogic : ILogic {
        private IDatabase GroupDB, historyDB;

        /// <summary>
        /// LoginGroupLogic constructor
        /// </summary>
        public AddMemberLogic() {
            GroupDB = new GroupDatabase();
            historyDB = new HistoryDatabase();
        }

        /// <summary>
        /// Method used to add a profile to a group
        /// </summary>
        /// <param name="obj">object[] obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType AddItemWithArgs(object[] obj) {
            LogicErrorType logicError;
            // Convert object to array
            var arguments = obj as object[];
            logicError = AddMemberCheck(arguments[0] as string);
            if(LogicErrorType.NoError == logicError) {
                // Find profile by username
                var profile = GroupProfileRelationship.findByUsername(arguments[0] as string);

                // Set group from arguments
                var group = arguments[1] as Group;
                logicError = GroupProfileRelationship.addProfileToGroup(profile, group);
                if(LogicErrorType.NoError == logicError) {
                    //Add history item
                    historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryAddMemberToGroupGroupTitle, string.Format(DataConstants.HistoryAddMemberToGroupGroupDescription, profile.Username, group.GroupName)));
                } 
            }
            return logicError;
        }

        /// <summary>
        /// Method used to update a group
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicType</returns>
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
        public LogicErrorType AddItem(object obj) {
            return LogicErrorType.NotImplemented;
        }

        /// <summary>
        /// Helper method used to check the given member username
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>LogicErrorType logicError</returns>
        private LogicErrorType AddMemberCheck(string username) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(username)) {
                logicError = LogicErrorType.EmptyUsername;
            } else if(CheckUsernameLength(username)) {
                logicError = LogicErrorType.InvalidUsernameLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check the username length
        /// </summary>
        /// <param name="username">string username</param>
        /// <returns>true/false</returns>
        private bool CheckUsernameLength(string username) {
            int usernameLength = username.Length;
            return usernameLength < Profile.MinUsernameLength || usernameLength > Profile.MaxUsernameLength;
        }
    }
}
