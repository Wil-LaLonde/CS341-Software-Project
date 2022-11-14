using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

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
        date.Text = selectedTask.Date;
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void SubmitTask(object sender, EventArgs e) => Close();

   
}