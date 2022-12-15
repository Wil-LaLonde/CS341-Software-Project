using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
///     NotificationPage shows all notifications for the signed in member
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public partial class NotificationPage : ContentPage {
    private readonly ILogic notificationLogic;

    /// <summary>
    ///     NotificationPage constructor
    /// </summary>
    public NotificationPage() {
        InitializeComponent();
        notificationLogic = new NotificationLogic();
        //Gather all notifications
        PrepareNotificationList();
        RefreshNotificationList();
    }


    /// <summary>
    ///     Helper method used to gather all notifications
    /// </summary>
    private async void PrepareNotificationList() {
        LogicErrorType logicError;
        logicError = notificationLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.LookupAllNotificationsDBError == logicError)
            await DisplayAlert(DataConstants.LookupNotificationDBErrorTitle,
                DataConstants.LookupNotificationDBErrorMessage, DataConstants.OK);
    }

    /// <summary>
    ///     Method called when a user selects a notification
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedNotification(object sender, SelectedItemChangedEventArgs e) {
        Notification selectedNotification = e.SelectedItem as Notification;
        if (selectedNotification != null) {
            NotificationPopUp notificationPopup = new(selectedNotification);
            this.ShowPopup(notificationPopup);
        }
    }

    /// <summary>
    ///     Helper method used to refresh the notification list
    /// </summary>
    private void RefreshNotificationList() {
        NotificationList.ItemsSource = MauiProgram.Profile.NotificationList;
    }
}
