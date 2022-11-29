namespace AcademicReward;
using CommunityToolkit.Maui.Views;

/// <summary>
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class AddShopItemPage : Popup {
	public AddShopItemPage() {
		InitializeComponent();
	}

    private void AddClicked(object sender, EventArgs e) => Close();

    private void BackButtonClicked(object sender, EventArgs e) => Close();
}