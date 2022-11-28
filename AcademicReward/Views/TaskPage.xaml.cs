using AcademicReward.PopUps;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class TaskPage : ContentPage {
	public class TestNotification {
		public TestNotification(string notification) {
			TestNotificationValue = notification;
		}

		public string TestNotificationValue { get; set; }
	}

	public class TestTask {
		public TestTask(string task) {
			TestTaskValue = task;
		}

		public string TestTaskValue { get; set; }
	}

	public TaskPage() {
		//Temp notification list for testing
		List<TestNotification> testNotificationList = new List<TestNotification>();
		testNotificationList.Add(new TestNotification("Testing Notification 1"));
        testNotificationList.Add(new TestNotification("Testing Notification 2"));
        testNotificationList.Add(new TestNotification("Testing Notification 3"));
        testNotificationList.Add(new TestNotification("Testing Notification 4"));
        testNotificationList.Add(new TestNotification("Testing Notification 5"));
		//Temp task list for testing
		List<TestTask> testTaskList = new List<TestTask>();
		testTaskList.Add(new TestTask("Testing Task 1"));
        testTaskList.Add(new TestTask("Testing Task 2"));
        testTaskList.Add(new TestTask("Testing Task 3"));
        testTaskList.Add(new TestTask("Testing Task 4"));
        testTaskList.Add(new TestTask("Testing Task 5"));
        InitializeComponent();
        NotificationList.ItemsSource = testNotificationList;
        TaskList.ItemsSource = testTaskList;
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