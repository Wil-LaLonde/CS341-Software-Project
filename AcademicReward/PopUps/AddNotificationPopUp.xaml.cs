using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

public partial class AddNotificationPopUp : Popup
{
	public AddNotificationPopUp()
	{
		InitializeComponent();
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void CreateNotificationButtonClicked(object sender, EventArgs e) => Close();
}
