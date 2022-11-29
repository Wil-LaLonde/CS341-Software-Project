namespace AcademicReward;
using CommunityToolkit.Maui.Views;

/// <summary>
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ViewShopItemPage : Popup {
	public ViewShopItemPage() {
		InitializeComponent();
	}

    private void EditClicked(object sender, EventArgs e) => Close();

    private void BackButtonClicked(object sender, EventArgs e) => Close();

	private void DeleteClicked(object sender, EventArgs e) => Close();
}