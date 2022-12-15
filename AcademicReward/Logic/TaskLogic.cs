using AcademicReward.Database;
using AcademicReward.Resources;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.Logic; 

/// <summary>
///     TaskLogic is the logic behind anything task related
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Xee Lo
/// </summary>
public class TaskLogic : ILogic {
    private readonly IDatabase _taskDb;

    /// <summary>
    ///     TaskLogic constructor
    /// </summary>
    public TaskLogic() {
        _taskDb = new TaskDatabase();
    }

    /// <summary>
    ///     Method used to add a task (logic)
    /// </summary>
    /// <param name="task">object task</param>
    /// <returns>LogicErrorType</returns>
    public LogicErrorType AddItem(object task) {
        LogicErrorType logicError;
        Task taskToAdd = task as Task;
        //Checking user input
        logicError = AddTaskCheck(taskToAdd);
        if (LogicErrorType.NoError == logicError) {
            DatabaseErrorType dbError = _taskDb.AddItem(taskToAdd);
            if (DatabaseErrorType.NoError == dbError)
                logicError = LogicErrorType.NoError;
            else
                logicError = LogicErrorType.AddTaskDbError;
        }

        return logicError;
    }

    /// <summary>
    ///     Method use to update task
    /// </summary>
    /// <param name="task"></param>
    /// <returns>LogicErrorType</returns>
    public LogicErrorType UpdateItem(object task) {
        LogicErrorType logicError;
        Task taskToUpdate = task as Task;
        DatabaseErrorType dbError = _taskDb.UpdateItem(taskToUpdate);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.UpdateTaskDbError;
        return logicError;
    }

    /// <summary>
    ///     Method used to delete a task
    /// </summary>
    /// <param name="task">object task</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType DeleteItem(object task) {
        LogicErrorType logicError;
        Task taskToDelete = task as Task;
        DatabaseErrorType dbError = _taskDb.DeleteItem(taskToDelete);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.DeleteTaskDbError;
        return logicError;
    }

    /// <summary>
    ///     Method used to gather all tasks for a given profile
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>LogicErrorType</returns>
    public LogicErrorType LookupItem(object profile) {
        LogicErrorType logicError;
        DatabaseErrorType dbError = _taskDb.LookupFullItem(profile);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.LookupAllTasksDbError;
        return logicError;
    }

    //Currently not needed
    public LogicErrorType AddItemWithArgs(object[] obj) {
        return LogicErrorType.NotImplemented;
    }

    /// <summary>
    ///     Helper method used to check a given task
    /// </summary>
    /// <param name="task">ModelClass.Task task</param>
    /// <returns>LogicErrorType</returns>
    private LogicErrorType AddTaskCheck(Task task) {
        LogicErrorType logicError;
        if (string.IsNullOrEmpty(task.Title))
            logicError = LogicErrorType.EmptyTaskTitle;
        else if (string.IsNullOrEmpty(task.Description))
            logicError = LogicErrorType.EmptyTaskDescription;
        else if (CheckPointValue(task.Points))
            logicError = LogicErrorType.NegativeTaskPoints;
        else if (CheckTitleLength(task.Title))
            logicError = LogicErrorType.InvalidTaskTitleLength;
        else if (CheckDescriptionLength(task.Description))
            logicError = LogicErrorType.InvalidTaskDescriptionLength;
        else
            logicError = LogicErrorType.NoError;
        return logicError;
    }

    /// <summary>
    ///     Helper method used to check a task's points
    /// </summary>
    /// <param name="points">int points</param>
    /// <returns>true/false</returns>
    private bool CheckPointValue(int points) {
        return points < Task.MinPointValue;
    }

    /// <summary>
    ///     Helper method used to check a task's title length
    /// </summary>
    /// <param name="title">string title</param>
    /// <returns>true/false</returns>
    private bool CheckTitleLength(string title) {
        int titleLength = title.Length;
        return titleLength < Task.MinTitleLength || titleLength > Task.MaxTitleLength;
    }

    /// <summary>
    ///     Helper method used to check a task's description length
    /// </summary>
    /// <param name="description">string description</param>
    /// <returns>true/false</returns>
    private bool CheckDescriptionLength(string description) {
        int descriptionLength = description.Length;
        return descriptionLength < Task.MinDescriptionLength || descriptionLength > Task.MaxDescriptionLength;
    }
}