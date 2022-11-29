using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class AddTaskPopUp : Popup
{
	public AddTaskPopUp()
	{
		InitializeComponent();
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void CreateTaskButtonClicked(object sender, EventArgs e) => Close();
}
