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
    Profile profile;
    private ILogic notificationLogic, taskLogic;

	public TaskPage() {
        InitializeComponent();
        notificationLogic = new NotificationLogic();
        taskLogic = new TaskLogic();
        NotificationList.ItemsSource = GetNotificationList();
        TaskList.ItemsSource = GetTaskList();
        profile = MauiProgram.Profile;
        //Most likely some database calls here to populate the lists
        //NotificationList.ItemsSource = testNotificationList;
        //TaskList.ItemsSource = testTaskList;
    }

    private ObservableCollection<Notification> GetNotificationList() {
        LogicErrorType logicError;
        ObservableCollection<Notification> notificationList = new ObservableCollection<Notification>();
        //Iterate over all groups to gather all notifications
        foreach(Group group in MauiProgram.Profile.GroupList) {
            logicError = notificationLogic.LookupItem(group);
            if(LogicErrorType.NoError == logicError) {

            }
        }
        return notificationList;
    }

    private ObservableCollection<ModelClass.Task> GetTaskList() {
        ObservableCollection<ModelClass.Task> taskList = new ObservableCollection<ModelClass.Task>();
        //Iterate over all groups to gather all tasks
        foreach(Group group in MauiProgram.Profile.GroupList) {

        }
        return taskList;
    }

	private async void AddNotificationButtonClicked(object sender, EventArgs e) {
        AddNotificationPopUp addNotificationPopUp = new AddNotificationPopUp();
        Notification notification = await this.ShowPopupAsync(addNotificationPopUp) as Notification;
        if(notification != null) {
            await DisplayAlert(DataConstants.CreateNotificationSuccessTitle, DataConstants.CreateNotificationSuccessMessage, DataConstants.OK);
        }
    }

	private async void AddTaskButtonClicked(object sender, EventArgs e) {
		AddTaskPopUp addTaskPopUp = new AddTaskPopUp();
		ModelClass.Task task = await this.ShowPopupAsync(addTaskPopUp) as ModelClass.Task;
        if(task != null) {
            await DisplayAlert(DataConstants.CreateTaskSuccessTitle, DataConstants.CreateTaskSuccessMessage, DataConstants.OK);
        }
	}
}