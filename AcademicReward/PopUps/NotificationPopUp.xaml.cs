using CommunityToolkit.Maui.Views;
using AcademicReward.ModelClass;

namespace AcademicReward.PopUps;

/// <summary>
/// NotificationPopUp is the popup when a user clicks on a notification
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Maximilian Patterson
/// </summary>
public partial class NotificationPopUp : Popup {

    /// <summary>
    /// NotificationPopUp constructor
    /// </summary>
    /// <param name="notification">Notification notification</param>
	public NotificationPopUp(Notification notification) {
		InitializeComponent();
        SetNotificationLabels(notification);
	}

    /// <summary>
    /// Method called when a user clicks on the back button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

    /// <summary>
    /// Method called when a user clicks on the close button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CloseNotificationButtonClicked(object sender, EventArgs e) => Close();

    /// <summary>
    /// Helper method used to fill in the notification labels
    /// </summary>
    /// <param name="notification">Notification notification</param>
    private void SetNotificationLabels(Notification notification) {
        NotificationTitle.Text = notification.Title;
        NotificationDescription.Text = notification.Description;
        NotificationGroup.Text = MauiProgram.Profile.GetGroupNameUsingGroupID(notification.GroupID);
    }
}
