using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class TaskPage : ContentPage {
    private ILogic notificationLogic, taskLogic;

	public TaskPage() {
        InitializeComponent();
        notificationLogic = new NotificationLogic();
        taskLogic = new TaskLogic();
        //Gathering all current notifications
        PrepareNotificationList();
        RefreshNotificationList();
        //Gathering all currents tasks
        PrepareTaskList();
        RefreshTaskList();
    }

    /// <summary>
    /// Helper method used to gather all notifications
    /// </summary>
    private async void PrepareNotificationList() {
        LogicErrorType logicError;
        foreach(Group group in MauiProgram.Profile.GroupList) {
            logicError = notificationLogic.LookupItem(group);
            if(LogicErrorType.LookupAllNotificationsDBError == logicError) {
                await DisplayAlert(DataConstants.LookupNotificationDBErrorTitle, DataConstants.LookupNotificationDBErrorMessage, DataConstants.OK);
                break;
            }
        }
    }

    /// <summary>
    /// Helper method used to gather all tasks
    /// </summary>
    private async void PrepareTaskList() {
        LogicErrorType logicError;
        foreach (Group group in MauiProgram.Profile.GroupList) {
            logicError = taskLogic.LookupItem(group);
            if(LogicErrorType.LookupAllTasksDBError == logicError) {
                await DisplayAlert(DataConstants.LookupTaskDBErrorTitle, DataConstants.LookupTaskDBErrorMessage, DataConstants.OK);
                break;
            }
        }
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
            RefreshNotificationList();
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
            RefreshTaskList();
            await DisplayAlert(DataConstants.CreateTaskSuccessTitle, DataConstants.CreateTaskSuccessMessage, DataConstants.OK);
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

    /// <summary>
    /// Helper method used to refresh the task list
    /// </summary>
    private void RefreshTaskList() {
        ObservableCollection<ModelClass.Task> taskList = new ObservableCollection<ModelClass.Task>();
        foreach (Group group in MauiProgram.Profile.GroupList) {
            foreach (ModelClass.Task task in group.GroupTaskList) {
                taskList.Add(task);
            }
        }
        TaskList.ItemsSource = taskList;
    }
}