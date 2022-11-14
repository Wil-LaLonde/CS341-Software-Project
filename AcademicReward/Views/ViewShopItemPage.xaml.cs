namespace AcademicReward;
using CommunityToolkit.Maui.Views;

public partial class ViewShopItemPage : Popup {
	public ViewShopItemPage() {
		InitializeComponent();
	}

    private void EditClicked(object sender, EventArgs e) => Close();

    private void BackButtonClicked(object sender, EventArgs e) => Close();

	private void DeleteClicked(object sender, EventArgs e) => Close();
}