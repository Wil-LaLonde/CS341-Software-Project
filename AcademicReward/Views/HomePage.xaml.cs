namespace AcademicReward.Views;
using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using System.Collections.ObjectModel;

public partial class HomePage : ContentPage {
	public HomePage() {
		InitializeComponent();
		TaskLV.ItemsSource = GetTasks();//need to get all tasks... hardcode some tasks
    }

	//hardcoded this until we do get a database
    ObservableCollection<Task> tasks = new ObservableCollection<Task> {
		new Task(false, "Complete 5 tasks", "You need to complete 5 tasks to finish this task", 4, "May 16, 2023"),
		new Task(false, "Level up to 4", "You have leveled up to 4", 5, "June 10, 2023"),
		new Task(false, "Join a group", "Join a group to compete this task", 2, "November 3, 2023"),
		new Task(false, "Buy an item from the shop", "Buy any item from the shop to earn points", 19, "March 9, 2023")};

	//Tasks are true and checked off
	private void TaskCheckBox(object sender, CheckedChangedEventArgs e){

	}

	//this will return the ObservableCollection that will contain all the Task objects to be displayed in the the list view
	private ObservableCollection<Task> GetTasks() {
		return tasks;
	}

	//once a task is selected we will take it to a popup that will have more details about the Task
	private void SelectedTask(object sender, SelectedItemChangedEventArgs e) {
		Task selectedTask = (Task)e.SelectedItem;
         TaskPopUp taskPopUp = new TaskPopUp(selectedTask);
         this.ShowPopup(taskPopUp);
    }
}