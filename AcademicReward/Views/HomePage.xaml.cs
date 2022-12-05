namespace AcademicReward.Views;
using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using AcademicReward.Resources;

/// <summary>
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HomePage : ContentPage {

    private ILogic taskLogic;
    bool isAdmin;
    public HomePage() {
		InitializeComponent();
        taskLogic = new TaskLogic();
        isAdmin = MauiProgram.Profile.IsAdmin;
		UsernameDisplay(isAdmin);
        PrepareTaskList();
    }

    /// <summary>
    /// Method is called when you start the home page to display the difference pages for member/admin
    /// </summary>
    /// <param name="isAdmin">bool isAdmin</param>
    private void UsernameDisplay(bool isAdmin) {
        //Bind user name to signed in profile
        Username.Text = MauiProgram.Profile.Username;
        if (isAdmin) {
            PointsLabel.IsVisible = false;
            Points.IsVisible = false;
            LevelLabel.IsVisible = false;
            Level.IsVisible = false;
            ProgressBar.IsVisible = false;
            ExpLabel.IsVisible = false;
            Exp.IsVisible = false;
		}
		else {
            //Need to map over all member values
            Points.Text = MauiProgram.Profile.Points.ToString();
            Level.Text = MauiProgram.Profile.Level.ToString();
            ProgressBar.Progress = MauiProgram.Profile.GetCurrentXPDouble();
            Exp.Text = MauiProgram.Profile.GetCurrentXPInt() + DataConstants.SpaceSlashSpace + Profile.LevelUpRequirementInt;
			TaskLV.HeightRequest = 700;
        }
    }

    /// <summary>
    /// Checks to see if the checkbox has been checked off and then make the bool isChecked true
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void TaskCheckBox(object sender, CheckedChangedEventArgs e){
        

	}

    /// <summary>
    /// Helper method used to gather all tasks
    /// </summary>
    private async void PrepareTaskList() {
        LogicErrorType logicError;
        logicError = taskLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.LookupAllTasksDBError == logicError) {
            await DisplayAlert(DataConstants.LookupTaskDBErrorTitle, DataConstants.LookupTaskDBErrorMessage, DataConstants.OK);
        }
        else {
            TaskLV.ItemsSource = MauiProgram.Profile.TaskList;
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
}