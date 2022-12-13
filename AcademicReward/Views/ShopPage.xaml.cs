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

    bool isAdmin;

    private ShopLogic ShopLogic;
	public ShopPage() {
		InitializeComponent();
        isAdmin = MauiProgram.Profile.IsAdmin;
        ShopLogic = new ShopLogic();
        SetVisibility();
        getShopItems();
        
    }
    private void SetVisibility()
    {
        if ( isAdmin)
        {
            AddButton.IsVisible = true;
            CreateText.IsVisible = true;
        }
        else
        {
            AddButton.IsVisible = false;
            CreateText.IsVisible = false;
        }
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
        ShopItemList.ItemsSource = ShopLogic.ItemList;
    }

    public void displayError(String title, String body, String cancel)
    {
        DisplayAlert(title, body, cancel);
    }
}
