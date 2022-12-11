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
    public class GroupLogic : ILogic
    {
        private IDatabase GroupDB;

        /// <summary>
        /// LoginGroupLogic constructor
        /// </summary>
        public GroupLogic()
        {
            GroupDB = new GroupDatabase();
        }

        //Currently not needed
        public LogicErrorType AddItem(object obj)
        {
            var group = obj as ModelClass.Group;

            // Add to database
            var dbError = GroupDB.AddItem(obj);

            // Create history entry for a new group
            var historyDB = new HistoryDatabase();
            historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryCreateGroupTitle, string.Format(DataConstants.HistoryCreateGroupDescription, group.GroupName)));

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

        /// <summary>
        /// Method that calls the database to gather 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public LogicErrorType LookupItem(object profile)
        {
            return LogicErrorType.NoError;
        }

        public LogicErrorType AddItemWithArgs(object[] obj)
        {
            throw new NotImplementedException();
        }
    }
}