using System.Text;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Views;

/// <summary>
///     CreateGroupPage is the page that is shown for creating a new group
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class CreateGroupPage : ContentPage {
    // Group logic
    private readonly GroupLogic groupLogic;

    /// <summary>
    ///     CreateGroupPage constructor
    /// </summary>
    public CreateGroupPage() {
        // Instantiate groupLogic
        groupLogic = new GroupLogic();
        InitializeComponent();
        SetErrorMessageBox(false, string.Empty);
    }

    /// <summary>
    ///     Method called when a user clicks on the create group button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CreateGroupButtonClicked(object sender, EventArgs e) {
        // Grab the text from the entry boxes
        string groupName = GroupNameEntry.Text;
        string groupDescription = GroupDescriptionEntry.Text;
        //Create group object from information supplied by admin user
        Group groupToAdd = new(groupName, groupDescription, MauiProgram.Profile);
        LogicErrorType logicError = groupLogic.AddItem(groupToAdd);
        if (LogicErrorType.NoError == logicError)
            // Navigate back to the group page
            Navigation.PopAsync();
        else
            //Something went wrong, display message.
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
            case LogicErrorType.EmptyGroupName:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyGroupNameMessage);
                break;
            case LogicErrorType.EmptyGroupDescription:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyGroupDescriptionMessage);
                break;
            case LogicErrorType.InvalidGroupNameLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidGroupNameLengthMessage);
                break;
            case LogicErrorType.InvalidGroupDescriptionLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidGroupDescriptionMessage);
                break;
            case LogicErrorType.GroupCreateError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.CreateGroupDBErrorMessage);
                break;
            default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.CreateGroupUnknownErrorMessage);
                break;
        }

        return errorMessageBuilder.ToString();
    }
}
