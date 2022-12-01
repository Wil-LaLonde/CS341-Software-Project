using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;
using AcademicReward.ModelClass;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ShopPage : ContentPage {

    private ShopLogic ShopLogic;
	public ShopPage() {
		InitializeComponent();
        ShopLogic = new ShopLogic();
        getShopItems();
	}

	private void Button_Clicked(object sender, EventArgs e) {
		this.ShowPopup(new AddShopItemPage(ShopLogic));
        getShopItems();
	}

    private void SelectedItem(object sender, SelectedItemChangedEventArgs e)
    {
        ShopItem selectedItem = e.SelectedItem as ShopItem;
        if (selectedItem != null)
        {
            //ShopItem notificationPopup = new Shopitem();
            //this.ShowPopup(notificationPopup);
            ShopItem selected = e.SelectedItem as ShopItem;
            this.ShowPopup(new ViewShopItemPage(selected, ShopLogic, this));
            getShopItems();
        }
    }

    public void openEditPage(ShopItem toBeEditted)
    {
        this.ShowPopup(new EditShopItemPage(ShopLogic, toBeEditted, this));
        
    }

    public void getShopItems()
    {
        ShopLogic.LookupItem(null);
       // ShopLogic.ItemList.Add(new ShopItem("TestTitle", "TestDesc",0,0,null));
        ShopItemList.ItemsSource = ShopLogic.ItemList;
    }
}