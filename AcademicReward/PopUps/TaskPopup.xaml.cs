using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;
using AcademicReward.Resources;
using static Android.Provider.ContactsContract;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class TaskPopUp : Popup {

    public ModelClass.Task SelectedTask { get; }
    bool isAdmin;
    IDatabase history;
    ILogic updateTask;
    public TaskPopUp() {
		InitializeComponent();
        

    }

	public TaskPopUp(ModelClass.Task selectedTask) {
        InitializeComponent();
        SelectedTask = selectedTask;
        title.Text = selectedTask.Title;
        description.Text = selectedTask.Description;
        points.Text = selectedTask.Points.ToString();
        group.Text = selectedTask.GroupID.ToString();
        isAdmin = MauiProgram.Profile.IsAdmin;
        history = new HistoryDatabase();
        updateTask = new TaskLogic();
    }

    /// <summary>
    /// Method that closes the popup
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

    /// <summary>
    /// Method that submits the task for review
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void SubmitTask(object sender, EventArgs e) {
        LogicErrorType logicError;
        if (isAdmin)
        {
            //call the task database to change the profileTask table
            SelectedTask.IsChecked = true;

            //sql call to update table
            logicError = updateTask.UpdateItem(SelectedTask);

            MauiProgram.Profile.RemoveTaskFromProfile(SelectedTask);

        }
        else
        {
            if (SelectedTask.IsChecked)//if the task is fully checked off 
            {

                //send notification to memeber that they have been awarded 
              //  Notification completedTaskNotification = new Notification("Congratulations, you have completed a task", $"Task: {SelectedTask.Title}", SelectedTask.GroupID);
               // MauiProgram.Profile.AddNotificationToProfile(completedTaskNotification);
                //send notification to memeber that they have been awarded 

                //add it to task to history
                HistoryItem taskHistory = new HistoryItem(MauiProgram.Profile.ProfileID, SelectedTask.Title, $"{SelectedTask.Description}\nPoints: {SelectedTask.Points}\nGroupID: {SelectedTask.GroupID}");
                history.AddItem(taskHistory);
                //add it to task to history

                //add points and exp 
                MauiProgram.Profile.AddXPToMember(SelectedTask.Points);
                MauiProgram.Profile.AddPointsToMember(SelectedTask.Points);
                //add points and exp 

                //remove task from the list 
                MauiProgram.Profile.RemoveTaskFromProfile(SelectedTask);

                //removes task from list 
                MauiProgram.Profile.RemoveTaskFromProfile(SelectedTask);
                //removes task from list
            }
            else //if the task is not fully checked then
            {
                //add the task to the admin tasklist 
                logicError = updateTask.UpdateItem(SelectedTask);
                SelectedTask.IsSubmitted = true;//if that task is submitted for review then make this true

                //Notification completedTaskNotification = new Notification("A new Task as been added", $"Task: {SelectedTask.Title}", SelectedTask.GroupID);
                //need to send this notification to admin


            }
        }

        Close();
    }
    

   
}