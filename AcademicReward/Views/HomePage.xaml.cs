using System.Collections.ObjectModel;
using AcademicReward.Database;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.Views;

/// <summary>
///     HomePage is the page the user lands on upon logging in. They can submit tasks as well
///     Primary Author: Xee Lo
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class HomePage : ContentPage {
    private readonly IDatabase _history;
    private readonly bool _isAdmin;
    private readonly IDatabase _lookUpMemberId;
    private readonly IDatabase _lookUpTask;

    private readonly ILogic _taskLogic;
    private ObservableCollection<Task> _tasksToShow;
    private readonly ILogic _updateProfile;

    /// <summary>
    ///     HomePage constructor
    /// </summary>
    public HomePage() {
        InitializeComponent();
        _taskLogic = new TaskLogic();
        _lookUpTask = new TaskDatabase();
        _lookUpMemberId = new TaskDatabase();
        _history = new HistoryDatabase();
        _updateProfile = new ProfileLogic();
        _isAdmin = MauiProgram.Profile.IsAdmin;
        PrepareTaskList();
        RefreshTaskList();
        BindingContext = MauiProgram.Profile;
        Points.SetBinding(Label.TextProperty, nameof(MauiProgram.Profile.Points));
        UsernameDisplay(_isAdmin);
    }

    /// <summary>
    ///     Method is called when you start the home page to display the difference pages for member/admin
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
            Level.Text = MauiProgram.Profile.Level.ToString();
            ProgressBar.Progress = MauiProgram.Profile.GetCurrentXpDouble();
            Exp.Text = MauiProgram.Profile.GetCurrentXpInt() + DataConstants.SpaceSlashSpace +
                Profile.LevelUpRequirementInt;
            TaskLV.HeightRequest = 700;
        }
    }

    /// <summary>
    ///     Helper method used to gather all tasks
    /// </summary>
    private async void PrepareTaskList() {
        _tasksToShow = new ObservableCollection<Task>();
        LogicErrorType
            logicError = _taskLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.LookupAllTasksDbError == logicError) {
            await DisplayAlert(DataConstants.LookupTaskDbErrorTitle, DataConstants.LookupTaskDbErrorMessage,
                DataConstants.Ok);
        }
        else {
            if (MauiProgram.Profile.IsAdmin)
                // ObservableCollection<Task> tasksToShow = new ObservableCollection<Task>();
                foreach (Task task in MauiProgram.Profile.TaskList) {
                    if (task.IsSubmitted && !task.IsApproved) {
                        //Setting to false since we don't want the checkbox to be checked
                        task.IsSubmitted = false;
                        _tasksToShow.Add(task);
                        TaskLV.ItemsSource = _tasksToShow;
                    }
                }
            else
                //  ObservableCollection<Task> tasksToShow = new ObservableCollection<Task>();
                foreach (Task task in MauiProgram.Profile.TaskList) {
                    //look up individual tasks and set the properties if needed 
                    logicError = (LogicErrorType)_lookUpTask.LookupItem(task);
                    if (LogicErrorType.LookupAllNotificationsDbError == logicError) {
                        await DisplayAlert(DataConstants.LookupTaskDbErrorTitle, DataConstants.LookupTaskDbErrorMessage,
                            DataConstants.Ok);
                    }
                    else {
                        if (!task.IsApproved) {
                            _tasksToShow.Add(task);
                            TaskLV.ItemsSource = _tasksToShow;
                        }
                        else {
                            HistoryItem taskHistory = new(MauiProgram.Profile.ProfileId, task.Title,
                                $"{task.Description}\nPoints: {task.Points}\nGroupID: {task.GroupId}");
                            _history.AddItem(taskHistory); //history doesnt have a logic layer so may need to changed

                            MauiProgram.Profile.AddXpToMember(task.Points);     //adds exp to memeber
                            MauiProgram.Profile.AddPointsToMember(task.Points); //adds points to member
                            logicError = _updateProfile.UpdateItem(MauiProgram.Profile);
                            if (logicError == LogicErrorType.NoError)
                                //removes the task once it has been reviewed and approved 
                                _taskLogic.DeleteItem(task);
                            else
                                await DisplayAlert(DataConstants.UpdateProfileXpPointsLevelTitle,
                                    DataConstants.UpdateProfileXpPointsLevelMessage, DataConstants.Ok);
                        }
                    }
                }
        }
    }

    /// <summary>
    ///     Once a task is selected a popup comes up
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void SelectedTask(object sender, SelectedItemChangedEventArgs e) {
        Task selectedTask = (Task)e.SelectedItem;

        TaskPopUp taskPopUp = new(selectedTask);

        Task task = await this.ShowPopupAsync(taskPopUp) as Task;

        if (task != null) {
            if (MauiProgram.Profile.IsAdmin && task.IsApproved) {
                RemoveTask(task);
                int memberId = (int)_lookUpMemberId.FindById(task.TaskId);
                HistoryItem taskHistory = new(MauiProgram.Profile.ProfileId, task.Title,
                    $"MemberID: {memberId}\nGroupID: {task.GroupId}");
                _history.AddItem(taskHistory);
                await DisplayAlert(DataConstants.TaskApprovedTitle, DataConstants.TaskApprovedMessage,
                    DataConstants.Ok);
            }
            else {
                await DisplayAlert(DataConstants.TaskSubmittedTitle, DataConstants.TaskSubmittedMessage,
                    DataConstants.Ok);
                RefreshTaskList();
            }
        }
    }

    /// <summary>
    ///     refreshes the task to show
    /// </summary>
    private void RefreshTaskList() {
        TaskLV.ItemsSource = _tasksToShow;
    }

    /// <summary>
    ///     removes task from list after Admin has approved a task
    /// </summary>
    /// <param name="taskToRemove"></param>
    public void RemoveTask(Task taskToRemove) {
        _tasksToShow.Remove(taskToRemove);
    }
}