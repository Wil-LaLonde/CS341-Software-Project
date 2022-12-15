using CommunityToolkit.Maui.Views;
using Task = AcademicReward.ModelClass.Task;

namespace AcademicReward.PopUps;

/// <summary>
///     TaskPageTaskPopUp is the popup when a user clicks on a task on the Task Page
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Xee Lo
/// </summary>
public partial class TaskPageTaskPopUp : Popup {
    /// <summary>
    ///     TaskPageTaskPopUp constructor
    /// </summary>
    /// <param name="task">ModelClass.Task task</param>
    public TaskPageTaskPopUp(Task task) {
        InitializeComponent();
        SetTaskLabels(task);
    }

    /// <summary>
    ///     Method called when a user clicks on the back button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) {
        Close();
    }

    /// <summary>
    ///     Method called when a user clicks on the close button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CloseTaskButtonClicked(object sender, EventArgs e) {
        Close();
    }

    /// <summary>
    ///     Helper method used to set all the task detail labels
    /// </summary>
    /// <param name="task">ModelClass.Task task</param>
    private void SetTaskLabels(Task task) {
        TaskTitle.Text = task.Title;
        TaskDescription.Text = task.Description;
        TaskPoints.Text = task.Points.ToString();
        TaskGroup.Text = MauiProgram.Profile.GetGroupNameUsingGroupId(task.GroupId);
    }
}
