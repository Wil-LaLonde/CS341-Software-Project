using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using System.Text;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde 
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class AddNotificationPopUp : Popup {
    ILogic notificationLogic;

    /// <summary>
    /// AddNotificationPopUp constructor
    /// </summary>
	public AddNotificationPopUp() {
		InitializeComponent();
        //Set group picker item source
        GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
        notificationLogic = new NotificationLogic();
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

    /// <summary>
    /// Method called when a user clicks on the back button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

    /// <summary>
    /// Method called when a user clicks on the create notification button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CreateNotificationButtonClicked(object sender, EventArgs e) {
        LogicErrorType logicError;
        Group selectedGroup = GroupPicker.SelectedItem as Group; 
        if(selectedGroup != null) {
            //Gathering user input.
            string title = NotificationTitleEntry.Text ?? string.Empty;
            string description = NotificationDescriptionEntry.Text ?? string.Empty;
            //Creating new notification object
            Notification notification = new Notification(title, description, selectedGroup.GroupID);
            logicError = notificationLogic.AddItem(notification);
            if(LogicErrorType.NoError == logicError) {
                MauiProgram.Profile.AddNotificationToProfile(notification);
                Close(notification);
            }
        } else {
            logicError = LogicErrorType.EmptyNotificationGroup;
        }
        //There was some kind of error, show messages
        SetErrorMessageBox(true, SetErrorMessageBody(logicError));
    }

    /// <summary>
	/// Helper method used to either show or hide the error message box
	/// This is done since a popup cannot have another popup
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
    /// Helper method used to determine what error message to display.
    /// </summary>
    /// <param name="logicError">LogicErrorType logicError</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
        StringBuilder errorMessageBuilder = new StringBuilder();
        switch(logicError) {
            case LogicErrorType.EmptyNotificationGroup:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyNotificationGroupMessage);
                break;
            case LogicErrorType.EmptyNotificationTitle:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyNotificationTitleMessage);
                break;
            case LogicErrorType.EmptyNotificationDescription:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyNotificationDescriptionMessage);
                break;
            case LogicErrorType.InvalidNotificationtitleLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidNotificationTitleLengthMessage);
                break;
            case LogicErrorType.InvalidNotificationDescriptionLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidNotificationDescriptionLengthMessage);
                break;
            default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddNotificationUnknownMessage);
                break;
        }
        return errorMessageBuilder.ToString();
    }
}
