using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using AcademicReward.PopUps;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Maximilian Patterson
/// </summary>
public partial class NotificationPage : ContentPage {
    ILogic notificationLogic;

	public NotificationPage() {
		InitializeComponent();
        notificationLogic = new NotificationLogic();
        //Gather all notifications
        PrepareNotificationList();
        RefreshNotificationList();
    }


    /// <summary>
    /// Helper method used to gather all notifications
    /// </summary>
    private async void PrepareNotificationList() {
        LogicErrorType logicError;
        foreach (Group group in MauiProgram.Profile.GroupList) {
            logicError = notificationLogic.LookupItem(group);
            if (LogicErrorType.LookupAllNotificationsDBError == logicError) {
                await DisplayAlert(DataConstants.LookupNotificationDBErrorTitle, DataConstants.LookupNotificationDBErrorMessage, DataConstants.OK);
                break;
            }
        }
    }

    /// <summary>
    /// Method called when a user selects a notification
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedNotification(object sender, SelectedItemChangedEventArgs e) {
        Notification selectedNotification = e.SelectedItem as Notification;
        if (selectedNotification != null) {
            NotificationPopUp notificationPopup = new NotificationPopUp(selectedNotification);
            this.ShowPopup(notificationPopup);
        }
    }

    /// <summary>
    /// Helper method used to refresh the notification list
    /// </summary>
    private void RefreshNotificationList() {
        ObservableCollection<Notification> notificationList = new ObservableCollection<Notification>();
        foreach (Group group in MauiProgram.Profile.GroupList) {
            foreach (Notification notification in group.GroupNotificationList) {
                notificationList.Add(notification);
            }
        }
        NotificationList.ItemsSource = notificationList;
    }
}