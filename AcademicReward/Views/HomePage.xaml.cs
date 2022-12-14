namespace AcademicReward.Views;
using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using AcademicReward.Resources;
using AcademicReward.Database;
using System.Collections.ObjectModel;

/// <summary>
/// HomePage is the page the user lands on upon logging in. They can submit tasks as well
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HomePage : ContentPage {

    private ILogic taskLogic;
    bool isAdmin;
    IDatabase lookUpTask;
    IDatabase history;
    ILogic updateProfile;
    ObservableCollection<Task> tasksToShow;

    /// <summary>
    /// HomePage constructor
    /// </summary>
    public HomePage() {
		InitializeComponent();
        taskLogic = new TaskLogic();
        lookUpTask = new TaskDatabase();
        history = new HistoryDatabase();
        updateProfile = new ProfileLogic();
        isAdmin = MauiProgram.Profile.IsAdmin;
        PrepareTaskList();
        RefreshTaskList();
        UsernameDisplay(isAdmin);
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
    /// Helper method used to gather all tasks
    /// </summary>
    private async void PrepareTaskList() {
        tasksToShow = new ObservableCollection<Task>();
        LogicErrorType
        logicError = taskLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.LookupAllTasksDBError == logicError) {
            await DisplayAlert(DataConstants.LookupTaskDBErrorTitle, DataConstants.LookupTaskDBErrorMessage, DataConstants.OK);
        }
        else {
            if (MauiProgram.Profile.IsAdmin){
               // ObservableCollection<Task> tasksToShow = new ObservableCollection<Task>();
                foreach (var task in MauiProgram.Profile.TaskList){
                   
                     if (task.IsSubmitted && (!task.IsChecked)) {
                     tasksToShow.Add(task);
                     TaskLV.ItemsSource = tasksToShow;
                     }    
                }
            }else{
              //  ObservableCollection<Task> tasksToShow = new ObservableCollection<Task>();
                foreach (var task in MauiProgram.Profile.TaskList)
                {
                    //look up individual tasks and set the properties if needed 
                    logicError = (LogicErrorType)lookUpTask.LookupItem(task);
                    if (LogicErrorType.LookupAllNotificationsDBError == logicError) {
                        await DisplayAlert(DataConstants.LookupTaskDBErrorTitle, DataConstants.LookupTaskDBErrorMessage, DataConstants.OK);
                    }
                    else {
                        if (!task.IsChecked) {
                            tasksToShow.Add(task);
                            TaskLV.ItemsSource = tasksToShow;
                        }
                        else {
                            HistoryItem taskHistory = new HistoryItem(MauiProgram.Profile.ProfileID, task.Title, $"{task.Description}\nPoints: {task.Points}\nGroupID: {task.GroupID}");
                            history.AddItem(taskHistory); //history doesnt have a logic layer so may need to changed

                            MauiProgram.Profile.AddXPToMember(task.Points); //adds exp to memeber
                            MauiProgram.Profile.AddPointsToMember(task.Points); //adds points to member
                            logicError = updateProfile.UpdateItem(MauiProgram.Profile);
                            if (logicError == LogicErrorType.NoError) { 
                                //removes the task once it has been reviewed and approved 
                            taskLogic.DeleteItem(task);
                            }
                            else {
                                await DisplayAlert(DataConstants.UpdateProfileXpPointsLevelTitle, DataConstants.UpdateProfileXpPointsLevelMessage, DataConstants.OK);
                            }
                        }
                    }
                }
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
    /// Method used to refresh the list being shown
    /// </summary>
    private void RefreshTaskList() {
        TaskLV.ItemsSource = tasksToShow;
    }
}