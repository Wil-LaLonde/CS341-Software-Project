using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

public partial class LoginPopUp : Popup {
	public LoginPopUp() {
		InitializeComponent();
	}

	//Some data validation will need to be done here
	private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void SignUpButtonClicked(object sender, EventArgs e) => Close();
}