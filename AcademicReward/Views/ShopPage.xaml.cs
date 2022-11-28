using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ShopPage : ContentPage {
	public ShopPage() {
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e) {
		this.ShowPopup(new AddShopItemPage());
	}

    private void Ticket_Clicked(object sender, EventArgs e) {
       this.ShowPopup(new ViewShopItemPage());
    }

    private void Shirt_Clicked(object sender, EventArgs e) {
        
    }
}