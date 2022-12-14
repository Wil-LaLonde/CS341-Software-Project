using System.Text;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.PopUps;

/// <summary>
///     AddTaskPopUp is the popup to add a new task
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Xee Lo
/// </summary>
public partial class AddTaskPopUp : Popup {
    private readonly ILogic _taskLogic;

    /// <summary>
    ///     AddTaskPopUp constructor
    /// </summary>
    public AddTaskPopUp() {
        InitializeComponent();
        //Gather all the groups for the select list
        GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
        _taskLogic = new TaskLogic();
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

    /// <summary>
    ///     Method called when a user clicks the back arrow image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs</param>
    private void BackButtonClicked(object sender, EventArgs e) {
        Close();
    }

    /// <summary>
    ///     Method called when a user clicks the create task button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CreateTaskButtonClicked(object sender, EventArgs e) {
        LogicErrorType logicError;
        Group selectedGroup = GroupPicker.SelectedItem as Group;
        if (selectedGroup != null) {
            //Gathering user input
            string title = TaskTitleEntry.Text ?? string.Empty;
            string description = TaskDescriptionEntry.Text ?? string.Empty;
            string points = TaskPointEntry.Text ?? string.Empty;
            bool isPointsValid = int.TryParse(points, out int intPoints);
            if (isPointsValid) {
                //Create a new Task object
                Task task = new(title, description, intPoints, selectedGroup.GroupId);
                //Check for any logic/database errors
                logicError = _taskLogic.AddItem(task);
                if (LogicErrorType.NoError == logicError) {
                    MauiProgram.Profile.AddTaskToProfile(task);
                    Close(task);
                }
            }
            else {
                logicError = LogicErrorType.InvalidTaskPoints;
            }
        }
        else {
            logicError = LogicErrorType.EmptyTaskGroup;
        }

        //There was some kind of error, show messages
        SetErrorMessageBox(true, SetErrorMessageBody(logicError));
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
        switch (logicError) {
            case LogicErrorType.EmptyTaskGroup:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyTaskGroupMessage);
                break;
            case LogicErrorType.InvalidTaskPoints:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidTaskPointsMessage);
                break;
            case LogicErrorType.EmptyTaskTitle:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyTaskTitleMessage);
                break;
            case LogicErrorType.EmptyTaskDescription:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyTaskDescriptionMessage);
                break;
            case LogicErrorType.NegativeTaskPoints:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.NegativeTaskPointsMessage);
                break;
            case LogicErrorType.InvalidTaskTitleLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidTaskTitleLengthMessage);
                break;
            case LogicErrorType.InvalidTaskDescriptionLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidTaskDescriptionLengthMessage);
                break;
            case LogicErrorType.AddTaskDbError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddTaskDbErrorMessage);
                break;
            default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddTaskUnknownMessage);
                break;
        }

        return errorMessageBuilder.ToString();
    }
}
