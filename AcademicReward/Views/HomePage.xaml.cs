namespace AcademicReward.Views;
using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using System.Collections.ObjectModel;
using AcademicReward.ModelClass;
using Xamarin.Google.Crypto.Tink.Signature;
using System.Diagnostics;
using AcademicReward.Logic;
using AcademicReward.Resources;


/// <summary>
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HomePage : ContentPage {

    private ILogic homeLogic;
    bool isAdmin;
    public HomePage() {
		InitializeComponent();
        homeLogic = new HomeLogic();
		//TaskLV.ItemsSource = GetTasks();//need to get all tasks... hardcode some tasks
        isAdmin = MauiProgram.Profile.IsAdmin;
		UsernameDisplay(isAdmin);
        PrepareTaskList();
        RefreshTaskList();
    }

    /// <summary>
    /// Method is called when you start the home page to display the difference pages for member/admin
    /// </summary>
    /// <param name="isAdmin">object sender</param>
    public void UsernameDisplay(bool isAdmin) {
        if (isAdmin) {
            points.IsVisible = false;
            level.IsVisible = false;
            progressBar.IsVisible = false;
            exp.IsVisible = false;
		}
		else {
			TaskLV.HeightRequest = 700;
        }
    }

	
    ObservableCollection<Task> tasks = new ObservableCollection<Task> {
		//new Task(false, "Complete 5 tasks", "You need to complete 5 tasks to finish this task", 4, "May 16, 2023"),
		//new Task(false, "Level up to 4", "You have leveled up to 4", 5, "June 10, 2023"),
		//new Task(false, "Join a group", "Join a group to compete this task", 2, "November 3, 2023"),
		//new Task(false, "Buy an item from the shop", "Buy any item from the shop to earn points", 19, "March 9, 2023")
	};

    /// <summary>
    /// Checks to see if the checkbox has been checked off and then make the bool isChecked true
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void TaskCheckBox(object sender, CheckedChangedEventArgs e){
        

	}

    /// <summary>
    /// gets all the tasks to display on the listview
    /// </summary>
    private void PrepareTaskList() {
        LogicErrorType logicError;
        foreach (Group group in MauiProgram.Profile.GroupList) {
            logicError = homeLogic.LookupItem(group);
            if (LogicErrorType.LookupAllTasksDBError == logicError) {
                DisplayAlert(DataConstants.LookupTaskDBErrorTitle, DataConstants.LookupTaskDBErrorMessage, DataConstants.OK);
                break;
            }
        }
	}

    /// <summary>
    /// Once a task is selected a popup comes up 
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void SelectedTask(object sender, SelectedItemChangedEventArgs e) {
		Task selectedTask = (Task)e.SelectedItem;
         TaskPopUp taskPopUp = new TaskPopUp(selectedTask);
         this.ShowPopup(taskPopUp);
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
        TaskLV.ItemsSource = taskList;
    }
}