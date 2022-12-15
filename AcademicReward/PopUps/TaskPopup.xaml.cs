using System.Text;
using AcademicReward.Database;
using AcademicReward.Logic;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.PopUps;

/// <summary>
///     TaskPopUp is the popup when a user clicks on a task on the Home page
///     Primary Author: Xee Lo
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class TaskPopUp : Popup {
    private readonly bool _isAdmin;
    private readonly IDatabase _lookUpTask;
    private readonly ILogic _updateTask;

    /// <summary>
    ///     TaskPopUp constructor
    /// </summary>
    public TaskPopUp() {
        InitializeComponent();
    }

    /// <summary>
    ///     TaskPopUp constructor
    /// </summary>
    /// <param name="selectedTask">ModelClass.Task selectedTask</param>
    public TaskPopUp(Task selectedTask) {
        InitializeComponent();
        SelectedTask = selectedTask;
        title.Text = selectedTask.Title;
        description.Text = selectedTask.Description;
        points.Text = selectedTask.Points.ToString();
        group.Text = MauiProgram.Profile.GetGroupNameUsingGroupId(selectedTask.GroupId);
        SetErrorMessageBox(false, string.Empty);
        _isAdmin = MauiProgram.Profile.IsAdmin;

        _updateTask = new TaskLogic();
        _lookUpTask = new TaskDatabase();
    }

    public Task SelectedTask { get; set; }

    /// <summary>
    ///     Method that closes the popup
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) {
        Close();
    }


    /// <summary>
    ///     Method that submits the task for review
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void SubmitTask(object sender, EventArgs e) {
        LogicErrorType logicError;

        //Lookup the selectedTask from the profiletask table
        _lookUpTask.LookupItem(SelectedTask);
        if (_isAdmin) {
            //if profile is admin 
            //sql call to update the task if the ADMIN has recieved a task a MEMBER has compeleted\
            SelectedTask.IsSubmitted = true;
            SelectedTask.IsApproved = true; //isApproved means ADMIN HAS CHECKED TASK AS COMPLETED 
            logicError = _updateTask.UpdateItem(SelectedTask);
            if (LogicErrorType.NoError == logicError) {
                MauiProgram.Profile.RemoveTaskFromProfile(SelectedTask); //remove it from ADMIN task list
                Close(SelectedTask);
            }
            else {
                logicError = LogicErrorType.UpdateTaskDbError;
            }
        }
        else {
            //This is for when MEMBERS are submitting the task for the first time --- therefore it has not been checked 

            SelectedTask.IsSubmitted = true; //if that task is submitted for review then make this true
            //updates the task so that ADMIN can view the task 
            logicError = _updateTask.UpdateItem(SelectedTask);
            if (LogicErrorType.NoError == logicError)
                //Notification completedTaskNotification = new Notification("A new Task as been added", $"Task: {SelectedTask.Title}", SelectedTask.GroupID);
                //need to send this notification to admin
                Close(SelectedTask);
            else
                logicError = LogicErrorType.UpdateTaskDbError;
        }

        if (logicError != LogicErrorType.NoError) SetErrorMessageBox(true, SetErrorMessageBody(logicError));
    }

    /// <summary>
    ///     Helper method used to either show or hide the error message box
    ///     This is done since a popup cannot have another popup
    /// </summary>
    /// <param name="isVisible">bool isVisible</param>
    /// <param name="errorMessage">string errorMessage</param>
    private void SetErrorMessageBox(bool isVisible, string errorMessage) {
        ErrorFrame.IsVisible = isVisible;
        ErrorStackLayout.IsVisible = isVisible;
        ErrorMessageHeader.IsVisible = isVisible;
        ErrorMessageBody.IsVisible = isVisible;
        ErrorMessageBody.Text = errorMessage;
    }

    /// <summary>
    ///     Helper method used to determine what error message to display.
    /// </summary>
    /// <param name="logicError">LogicErrorType logicError</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
        StringBuilder errorMessageBuilder = new();
        if (logicError == LogicErrorType.UpdateTaskDbError) {
            errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
            errorMessageBuilder.Append(DataConstants.UpdatingTask);
        }

        return errorMessageBuilder.ToString();
    }
}
