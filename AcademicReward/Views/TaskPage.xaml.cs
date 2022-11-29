using AcademicReward.PopUps;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class TaskPage : ContentPage {

	public TaskPage() {
        InitializeComponent();
        //Most likely some database calls here to populate the lists
        //NotificationList.ItemsSource = testNotificationList;
        //TaskList.ItemsSource = testTaskList;
    }

	private void AddNotificationButtonClicked(object sender, EventArgs e) {
        AddNotificationPopUp addNotificationPopUp = new AddNotificationPopUp();
        this.ShowPopup(addNotificationPopUp);
    }

	private void AddTaskButtonClicked(object sender, EventArgs e) {
		AddTaskPopUp addTaskPopUp = new AddTaskPopUp();
		this.ShowPopup(addTaskPopUp);
	}
}