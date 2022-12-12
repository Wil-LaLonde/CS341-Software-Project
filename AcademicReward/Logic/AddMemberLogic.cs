﻿using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: 
    /// Reviewer: 
    /// </summary>
    public class AddMemberLogic : ILogic
    {
        private IDatabase GroupDB;
        private IDatabase historyDB;

        /// <summary>
        /// LoginGroupLogic constructor
        /// </summary>
        public AddMemberLogic()
        {
            GroupDB = new GroupDatabase();
            historyDB = new HistoryDatabase();
        }

        //Currently not needed
        public LogicErrorType AddItemWithArgs(object[] obj)
        {
            // Convert object to array
            var arguments = obj as object[];

            // Find profile by username
            var loginDB = new LoginDatabase();
            var profile = GroupProfileRelationship.findByUsername(arguments[0] as string);

            // Set group from arguments
            var group = arguments[1] as Group;

            // Add relationship to database
            var error = GroupProfileRelationship.addProfileToGroup(profile, group);
            if (error != LogicErrorType.NoError)
            {
                return error;
            }


            // Create history entry for a new member in group
            try
            {
                historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryAddMemberToGroupGroupTitle, string.Format(DataConstants.HistoryAddMemberToGroupGroupDescription, profile.Username, group.GroupName)));
            } catch
            {
                return LogicErrorType.HistoryAddError;
            }
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType UpdateItem(object obj)
        {
            // Update database
            var dbError = GroupDB.UpdateItem(obj);
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object obj)
        {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType LookupItem(object profile)
        {
            return LogicErrorType.NoError;
        }

        public LogicErrorType AddItem(object obj)
        {
            throw new NotImplementedException();
        }
    }
}