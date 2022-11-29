using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    public class TaskLogic : ILogic {
        private IDatabase taskDB;

        /// <summary>
        /// TaskLogic constructor
        /// </summary>
        public TaskLogic() { 
            taskDB = new TaskDatabase();
        }

        /// <summary>
        /// Method used to add a task (logic)
        /// </summary>
        /// <param name="task">object task</param>
        /// <returns>LogicErrorType</returns>
        public LogicErrorType AddItem(object task) {
            LogicErrorType logicError;
            ModelClass.Task taskToAdd = task as ModelClass.Task;
            //Checking user input
            logicError = AddTaskCheck(taskToAdd);
            if(LogicErrorType.NoError == logicError) {
                DatabaseErrorType dbError = taskDB.AddItem(taskToAdd);
                if(DatabaseErrorType.NoError == dbError) {
                    logicError = LogicErrorType.NoError;
                } else {
                    logicError = LogicErrorType.AddTaskDBError;
                }
            }
            return logicError;
        }

        //Currently not needed
        public LogicErrorType UpdateItem(object task) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType DeleteItem(object task) {
            return LogicErrorType.NoError;
        }

        //Currently not needed
        public LogicErrorType LookupItem(object task) {
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Helper method used to check a given task
        /// </summary>
        /// <param name="task">ModelClass.Task task</param>
        /// <returns>LogicErrorType</returns>
        private LogicErrorType AddTaskCheck(ModelClass.Task task) {
            LogicErrorType logicError;
            if(string.IsNullOrEmpty(task.Title)) {
                logicError = LogicErrorType.EmptyTaskTitle;
            } else if(string.IsNullOrEmpty(task.Description)) {
                logicError = LogicErrorType.EmptyTaskDescription;
            } else if(CheckPointValue(task.Points)) {
                logicError = LogicErrorType.NegativeTaskPoints;
            } else if(CheckTitleLength(task.Title)) {
                logicError = LogicErrorType.InvalidTaskTitleLength;
            } else if(CheckDescriptionLength(task.Description)) {
                logicError = LogicErrorType.InvalidTaskDescriptionLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check a task's points
        /// </summary>
        /// <param name="points">int points</param>
        /// <returns>true/false</returns>
        private bool CheckPointValue(int points) {
            return points < ModelClass.Task.MinPointValue;
        }

        /// <summary>
        /// Helper method used to check a task's title length
        /// </summary>
        /// <param name="title">string title</param>
        /// <returns>true/false</returns>
        private bool CheckTitleLength(string title) {
            int titleLength = title.Length;
            return titleLength < ModelClass.Task.MinTitleLength || titleLength > ModelClass.Task.MaxTitleLength;
        }

        /// <summary>
        /// Helper method used to check a task's description length
        /// </summary>
        /// <param name="description">string description</param>
        /// <returns>true/false</returns>
        private bool CheckDescriptionLength(string description) {
            int descriptionLength = description.Length;
            return descriptionLength < ModelClass.Task.MinDescriptionLength || descriptionLength > ModelClass.Task.MaxDescriptionLength;
        }
    }
}
