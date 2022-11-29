using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic {
    public class TaskLogic : ILogic {
        private IDatabase taskDB;

        /// <summary>
        /// TaskLogic constructor
        /// </summary>
        public TaskLogic() { 
            taskDB = new TaskDatabase();
        }

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

        public LogicErrorType UpdateItem(object task) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object task) {
            return LogicErrorType.NoError;
        }

        public LogicErrorType LookupItem(object task) {
            return LogicErrorType.NoError;
        }

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

        private bool CheckPointValue(int points) {
            return points < ModelClass.Task.MinPointValue;
        }

        private bool CheckTitleLength(string title) {
            int titleLength = title.Length;
            return titleLength < ModelClass.Task.MinTitleLength || titleLength > ModelClass.Task.MaxTitleLength;
        }

        private bool CheckDescriptionLength(string description) {
            int descriptionLength = description.Length;
            return descriptionLength < ModelClass.Task.MinDescriptionLength || descriptionLength > ModelClass.Task.MaxDescriptionLength;
        }
    }
}
