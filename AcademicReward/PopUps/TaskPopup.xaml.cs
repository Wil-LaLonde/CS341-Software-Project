using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;
using AcademicReward.Resources;
using System.Text;
using System.Threading.Tasks;
using AcademicReward.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class TaskPopUp : Popup {

    public ModelClass.Task SelectedTask { get; set; }
    bool isAdmin;
    IDatabase lookUpTask;
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
        SetErrorMessageBox(false, string.Empty);
        isAdmin = MauiProgram.Profile.IsAdmin;
       
        updateTask = new TaskLogic();
        lookUpTask = new TaskDatabase();
        
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

        //Lookup the selectedTask from the profiletask table
        lookUpTask.LookupItem(SelectedTask); 
        if (isAdmin){//if profile is admin 
            //sql call to update the task if the ADMIN has recieved a task a MEMBER has compeleted\
            SelectedTask.IsChecked = true;  //isChecked means ADMIN HAS CHECKED TASK AS COMPLETED 
            logicError = updateTask.UpdateItem(SelectedTask);
            if (LogicErrorType.NoError == logicError) {
                
                MauiProgram.Profile.RemoveTaskFromProfile(SelectedTask); //remove it from ADMIN task list
               // HomePage removeTask = new HomePage();
               // removeTask.RemoveTask(SelectedTask);
                Close(SelectedTask);
            }
            else {
                logicError = LogicErrorType.UpdateTaskDBError; 
            }
        }
        else {
             //This is for when MEMBERS are submitting the task for the first time --- therefore it has not been checked 

                SelectedTask.IsSubmitted = true;  //if that task is submitted for review then make this true
                //updates the task so that ADMIN can view the task 
                logicError = updateTask.UpdateItem(SelectedTask);
                if (LogicErrorType.NoError == logicError) {
                                                      //Notification completedTaskNotification = new Notification("A new Task as been added", $"Task: {SelectedTask.Title}", SelectedTask.GroupID);
                                                      //need to send this notification to admin
                    Close(SelectedTask);
                }
                else {
                    logicError = LogicErrorType.UpdateTaskDBError;
                }
            
        }
        if(logicError != LogicErrorType.NoError) {
            SetErrorMessageBox(true, SetErrorMessageBody(logicError));
        }
    }

    /// <summary>
    /// Helper method used to either show or hide the error message box
    /// This is done since a popup cannot have another popup
    /// </summary>
    /// <param name="isVisible">bool isVisible</param>
    /// <param name="errorMessage">string errorMessage</param>
    private void SetErrorMessageBox(bool isVisible, string errorMessage) {
        ErrorFrame.IsVisible = isVisible;
        ErrorStackLayout.IsVisible = isVisible;
        ErrorMessageHeader.IsVisible = isVisible;
        ErrorMessageBody.IsVisible = isVisible;
        ErrorMessageBody.Text = errorMessage;
    }

    /// <summary>
    /// Helper method used to determine what error message to display.
    /// </summary>
    /// <param name="logicError">LogicErrorType logicError</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
        StringBuilder errorMessageBuilder = new StringBuilder();
        if (logicError == LogicErrorType.UpdateTaskDBError) {
       
             errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
             errorMessageBuilder.Append(DataConstants.UpdatingTask);   
        }
        return errorMessageBuilder.ToString();
    }

}