using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

public partial class AddTaskPopUp : Popup
{
	public AddTaskPopUp()
	{
		InitializeComponent();
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void CreateTaskButtonClicked(object sender, EventArgs e) => Close();
}
