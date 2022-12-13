using AcademicReward.Logic;
using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
/// TaskPage shows all tasks and notifications for an admin
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class TaskPage : ContentPage {
    private ILogic notificationLogic;
    private IDatabase historyDB;

    /// <summary>
    /// TaskPage constructor
    /// </summary>
	public TaskPage() {
        InitializeComponent();
        notificationLogic = new NotificationLogic();
        historyDB = new HistoryDatabase();
        //Gathering all current notifications
        PrepareNotificationList();
        //Gathering all currents tasks
        PrepareTaskList();
    }

    /// <summary>
    /// Helper method used to gather all notifications
    /// </summary>
    private async void PrepareNotificationList() {
        LogicErrorType logicError;
        logicError = notificationLogic.LookupItem(MauiProgram.Profile);
        if(LogicErrorType.LookupAllNotificationsDBError == logicError) {
            await DisplayAlert(DataConstants.LookupNotificationDBErrorTitle, DataConstants.LookupNotificationDBErrorMessage, DataConstants.OK);      
        } else {
            NotificationList.ItemsSource = MauiProgram.Profile.NotificationList;
        }
    }

    /// <summary>
    /// Helper method used to gather all tasks.
    /// No need to make a database call as tasks have 
    /// been gathered on the home page.
    /// </summary>
    private void PrepareTaskList() {
        TaskList.ItemsSource = MauiProgram.Profile.TaskList;
    }

    /// <summary>
    /// Method call when a user clicks on the add notification button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
	private async void AddNotificationButtonClicked(object sender, EventArgs e) {
        AddNotificationPopUp addNotificationPopUp = new AddNotificationPopUp();
        Notification notification = await this.ShowPopupAsync(addNotificationPopUp) as Notification;
        if(notification != null) {
            //Adding a history item for creating a notification
            historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryCreateNotificationTitle, 
                string.Format(DataConstants.HistoryCreateNotificationDescription, notification.Title, MauiProgram.Profile.GetGroupNameUsingGroupID(notification.GroupID))));
            await DisplayAlert(DataConstants.CreateNotificationSuccessTitle, DataConstants.CreateNotificationSuccessMessage, DataConstants.OK);
        }
    }

    /// <summary>
    /// Method called when a user selects a notification
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedNotification(object sender, SelectedItemChangedEventArgs e) {
        Notification selectedNotification = e.SelectedItem as Notification;
        if(selectedNotification != null) {
            NotificationPopUp notificationPopup = new NotificationPopUp(selectedNotification);
            this.ShowPopup(notificationPopup);
        }
    }

    /// <summary>
    /// Method called when a user selects a task
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedTask(object sender, SelectedItemChangedEventArgs e) {
        ModelClass.Task selectedTask = e.SelectedItem as ModelClass.Task;
        if(selectedTask != null) {
            TaskPageTaskPopUp taskPopup = new TaskPageTaskPopUp(selectedTask);
            this.ShowPopup(taskPopup);
        }
    }

    /// <summary>
    /// Method called when a user clicks on the add task button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
	private async void AddTaskButtonClicked(object sender, EventArgs e) {
		AddTaskPopUp addTaskPopUp = new AddTaskPopUp();
		ModelClass.Task task = await this.ShowPopupAsync(addTaskPopUp) as ModelClass.Task;
        if(task != null) {
            //Adding a history item for creating a task
            historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryCreateTaskTitle, 
                string.Format(DataConstants.HistoryCreateTaskDescription, task.Title, MauiProgram.Profile.GetGroupNameUsingGroupID(task.GroupID))));
            await DisplayAlert(DataConstants.CreateTaskSuccessTitle, DataConstants.CreateTaskSuccessMessage, DataConstants.OK);
        }
	}
}
