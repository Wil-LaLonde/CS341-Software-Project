namespace AcademicReward;
using CommunityToolkit.Maui.Views;
public partial class AddShopItemPage : Popup {
	public AddShopItemPage() {
		InitializeComponent();
	}

    private void AddClicked(object sender, EventArgs e) => Close();

    private void BackButtonClicked(object sender, EventArgs e) => Close();
}