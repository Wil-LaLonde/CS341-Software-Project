using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

public partial class ShopPage : ContentPage {

    private ILogic ShopLogic;
	public ShopPage() {
		InitializeComponent();
        ShopLogic = new ShopLogic();
	}

	private void Button_Clicked(object sender, EventArgs e) {
		this.ShowPopup(new AddShopItemPage(ShopLogic));
	}

    private void Ticket_Clicked(object sender, EventArgs e) {
       this.ShowPopup(new ViewShopItemPage());
    }

    private void Shirt_Clicked(object sender, EventArgs e) {
        
    }
}