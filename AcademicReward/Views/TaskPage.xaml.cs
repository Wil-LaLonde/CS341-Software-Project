using AcademicReward.Database;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.Views;

/// <summary>
///     TaskPage shows all tasks and notifications for an admin
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Xee Lo
/// </summary>
public partial class TaskPage : ContentPage {
    private readonly IDatabase _historyDb;
    private readonly ILogic _notificationLogic;

    /// <summary>
    ///     TaskPage constructor
    /// </summary>
    public TaskPage() {
        InitializeComponent();
        _notificationLogic = new NotificationLogic();
        _historyDb = new HistoryDatabase();
        //Gathering all current notifications
        PrepareNotificationList();
        //Gathering all currents tasks
        PrepareTaskList();
    }

    /// <summary>
    ///     Helper method used to gather all notifications
    /// </summary>
    private async void PrepareNotificationList() {
        LogicErrorType logicError;
        logicError = _notificationLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.LookupAllNotificationsDbError == logicError)
            await DisplayAlert(DataConstants.LookupNotificationDbErrorTitle,
                DataConstants.LookupNotificationDbErrorMessage, DataConstants.Ok);
        else
            NotificationList.ItemsSource = MauiProgram.Profile.NotificationList;
    }

    /// <summary>
    ///     Helper method used to gather all tasks.
    ///     No need to make a database call as tasks have
    ///     been gathered on the home page.
    /// </summary>
    private void PrepareTaskList() {
        TaskList.ItemsSource = MauiProgram.Profile.TaskList;
    }

    /// <summary>
    ///     Method call when a user clicks on the add notification button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void AddNotificationButtonClicked(object sender, EventArgs e) {
        AddNotificationPopUp addNotificationPopUp = new();
        Notification notification = await this.ShowPopupAsync(addNotificationPopUp) as Notification;
        if (notification != null) {
            //Adding a history item for creating a notification
            _historyDb.AddItem(new HistoryItem(MauiProgram.Profile.ProfileId,
                DataConstants.HistoryCreateNotificationTitle,
                string.Format(DataConstants.HistoryCreateNotificationDescription, notification.Title,
                    MauiProgram.Profile.GetGroupNameUsingGroupId(notification.GroupId))));
            await DisplayAlert(DataConstants.CreateNotificationSuccessTitle,
                DataConstants.CreateNotificationSuccessMessage, DataConstants.Ok);
        }
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
    ///     Method called when a user selects a task
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedTask(object sender, SelectedItemChangedEventArgs e) {
        Task selectedTask = e.SelectedItem as Task;
        if (selectedTask != null) {
            TaskPageTaskPopUp taskPopup = new(selectedTask);
            this.ShowPopup(taskPopup);
        }
    }

    /// <summary>
    ///     Method called when a user clicks on the add task button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void AddTaskButtonClicked(object sender, EventArgs e) {
        AddTaskPopUp addTaskPopUp = new();
        Task task = await this.ShowPopupAsync(addTaskPopUp) as Task;
        if (task != null) {
            //Adding a history item for creating a task
            _historyDb.AddItem(new HistoryItem(MauiProgram.Profile.ProfileId, DataConstants.HistoryCreateTaskTitle,
                string.Format(DataConstants.HistoryCreateTaskDescription, task.Title,
                    MauiProgram.Profile.GetGroupNameUsingGroupId(task.GroupId))));
            await DisplayAlert(DataConstants.CreateTaskSuccessTitle, DataConstants.CreateTaskSuccessMessage,
                DataConstants.Ok);
        }
    }
}
