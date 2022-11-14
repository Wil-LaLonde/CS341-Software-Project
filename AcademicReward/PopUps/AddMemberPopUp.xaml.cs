using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

public partial class AddMemberPopUp : Popup {
	public AddMemberPopUp() {
		InitializeComponent();
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();

	private void AddMemberButtonClicked(object sender, EventArgs e) => Close();
}