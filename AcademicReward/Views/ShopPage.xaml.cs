using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;
using AcademicReward.ModelClass;

namespace AcademicReward.Views;

/// <summary>
/// ShopPage shows all shop items for a profile/group
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ShopPage : ContentPage {
    private ShopLogic ShopLogic;

    /// <summary>
    /// ShopPage constructor
    /// </summary>
	public ShopPage() {
		InitializeComponent();
        ShopLogic = new ShopLogic();
        getShopItems();
	}

    /// <summary>
    /// Method called when a user clicks on the add shop item button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
	private void Button_Clicked(object sender, EventArgs e) {
		this.ShowPopup(new AddShopItemPage(ShopLogic));
        getShopItems();
	}

    /// <summary>
    /// Method called when a user selects a shop item
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedItem(object sender, SelectedItemChangedEventArgs e) {
        ShopItem selectedItem = e.SelectedItem as ShopItem;
        if (selectedItem != null) {
            ShopItem selected = e.SelectedItem as ShopItem;
            this.ShowPopup(new ViewShopItemPage(selected, ShopLogic, this));
            getShopItems();
        }
    }

    /// <summary>
    /// Method called to open the edit shop item page
    /// </summary>
    /// <param name="toBeEditted">ShopItem toBeEditted</param>
    public void openEditPage(ShopItem toBeEditted) {
        this.ShowPopup(new EditShopItemPage(ShopLogic, toBeEditted, this));
        
    }

    /// <summary>
    /// Method used to gather all shop items
    /// </summary>
    public void getShopItems() {
        ShopLogic.LookupItem(null);
        ShopItemList.ItemsSource = ShopLogic.ItemList;
    }
}
