using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic
{
    /// <summary>
    /// Primary Author: Wil LaLonde
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
    }
}