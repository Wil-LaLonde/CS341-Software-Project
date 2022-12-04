using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Xee Lo
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class TaskPopUp : Popup {

    public ModelClass.Task SelectedTask { get; }
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
    private void SubmitTask(object sender, EventArgs e) => Close();

   
}