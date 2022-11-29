using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde 
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class AddNotificationPopUp : Popup
{
	public AddNotificationPopUp()
	{
		InitializeComponent();
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void CreateNotificationButtonClicked(object sender, EventArgs e) => Close();
}
