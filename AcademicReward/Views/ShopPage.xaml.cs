using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
///     ShopPage shows all shop items for a profile/group
///     Primary Author: Sean Stille
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class ShopPage : ContentPage {
    private readonly ShopLogic _shopLogic;

    /// <summary>
    ///     ShopPage constructor
    /// </summary>
    public ShopPage() {
        InitializeComponent();
        _shopLogic = new ShopLogic();
        BindingContext = MauiProgram.Profile;
        Points.SetBinding(Label.TextProperty, nameof(MauiProgram.Profile.Points));
        SetVisibility(MauiProgram.Profile.IsAdmin);
        //Gathering the shop items for the given profile
        PrepareShopItemList();
        RefreshShopItemList();
    }

    /// <summary>
    ///     Helper method to set items on the page to visible or not
    /// </summary>
    /// <param name="isAdmin">bool isAdmin</param>
    private void SetVisibility(bool isAdmin) {
        if (isAdmin) {
            AddButton.IsVisible = true;
            CreateText.IsVisible = true;
            PointStack.IsVisible = false;
            PointsLabel.IsVisible = false;
            Points.IsVisible = false;
        }
        else {
            AddButton.IsVisible = false;
            CreateText.IsVisible = false;
        }
    }

    /// <summary>
    ///     Helper method to gather all shop items
    /// </summary>
    private async void PrepareShopItemList() {
        LogicErrorType logicError;
        logicError = _shopLogic.LookupItem(MauiProgram.Profile);
        if (LogicErrorType.NoError != logicError)
            await DisplayAlert(DataConstants.LookupShopItemDbErrorTitle, DataConstants.LookupShopItemDbErrorMessage,
                DataConstants.Ok);
    }

    /// <summary>
    ///     Helper method to refresh the shop item list
    /// </summary>
    private void RefreshShopItemList() {
        ShopItemList.ItemsSource = MauiProgram.Profile.ProfileShop.ShopItemList;
    }

    /// <summary>
    ///     Method called when a user clicks on the add shop item button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void Button_Clicked(object sender, EventArgs e) {
        ShopItem shopItem = await this.ShowPopupAsync(new AddShopItemPage(_shopLogic)) as ShopItem;
        if (shopItem != null)
            await DisplayAlert(DataConstants.AddShopItemSuccessTitle, DataConstants.AddShopItemSuccessMessage,
                DataConstants.Ok);
    }

    /// <summary>
    ///     Method called when a user selects a shop item
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private async void SelectedItem(object sender, SelectedItemChangedEventArgs e) {
        ShopItem selectedItem = e.SelectedItem as ShopItem;
        if (selectedItem != null) {
            ShopItem shopItem =
                await this.ShowPopupAsync(new ViewShopItemPage(selectedItem, _shopLogic, this)) as ShopItem;
            if (shopItem != null && shopItem.Id == ShopItem.DeleteShopItemSuccesValue)
                await DisplayAlert(DataConstants.DeleteShopItemSuccessTitle, DataConstants.DeleteShopItemSuccessMessage,
                    DataConstants.Ok);
            else if (shopItem != null && shopItem.Id == ShopItem.BuyShopItemSuccessValue)
                await DisplayAlert(DataConstants.BuyShopItemSuccessTitle, DataConstants.BuyShopItemSuccessMessage,
                    DataConstants.Ok);
        }
    }

    /// <summary>
    ///     Method called to open the edit shop item page
    /// </summary>
    /// <param name="toBeEditted">ShopItem toBeEditted</param>
    public async void OpenEditPage(ShopItem toBeEditted) {
        ShopItem shopItem = await this.ShowPopupAsync(new EditShopItemPage(_shopLogic, toBeEditted, this)) as ShopItem;
        if (shopItem != null)
            await DisplayAlert(DataConstants.UpdateShopItemSuccessTitle, DataConstants.UpdateShopItemSuccessMessage,
                DataConstants.Ok);
    }
}
