namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;

/// <summary>
/// ViewShopItemPage shows a given shop item
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ViewShopItemPage : Popup {
	ShopLogic logic;
	ShopItem item;
	ShopPage shop;
	
	/// <summary>
	/// ViewShopItemPage constructor
	/// </summary>
	/// <param name="viewedItem">ShopItem viewedItem</param>
	/// <param name="log">ShopLogic log</param>
	public ViewShopItemPage(ShopItem viewedItem, ShopLogic log) {
		InitializeComponent();
		logic = log;
		item = viewedItem;
		ItemTitle.Text = item.Title;
		Desc.Text = item.Description;
		Cost.Text = item.PointCost + "";
		shop = null;
		SetVisibility(MauiProgram.Profile.IsAdmin);
	}

	/// <summary>
	/// ViewShopItemPage constructor
	/// </summary>
	/// <param name="viewedItem">ShopItem viewedItem</param>
	/// <param name="log">ShopLogic log</param>
	/// <param name="page">ShopPage page</param>
    public ViewShopItemPage(ShopItem viewedItem, ShopLogic log, ShopPage page) {
        InitializeComponent();
        logic = log;
        item = viewedItem;
        ItemTitle.Text = item.Title;
        Desc.Text = item.Description;
        Cost.Text = item.PointCost + "";
		shop = page;
        SetVisibility(MauiProgram.Profile.IsAdmin);
    }

	private void SetVisibility(bool isAdmin)
	{
		if (isAdmin)
		{
			EditButton.IsVisible = true;
			DeleteButton.IsVisible = true;
			BuyButton.IsVisible = false;
		}
		else
		{
            EditButton.IsVisible = false;
            DeleteButton.IsVisible = false;
            BuyButton.IsVisible = true;
        }
	}
  
	/// <summary>
	/// Method called when the edit button is clicked
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void EditClicked(object sender, EventArgs e) {
		Close();
        shop.openEditPage(item);     
    }

	/// <summary>
	/// Method called when a user clicks on the back button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

	/// <summary>
	/// Method called when a user clicks on the delete button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void DeleteClicked(object sender, EventArgs e) {
		logic.DeleteItem(item);
	}


	private void BuyClicked(object sender, EventArgs e)
	{
		Profile profile = MauiProgram.Profile;
		String title = "Default";
		String body = "Default";
		String close = "Close";
		LogicErrorType result = logic.BuyItem(item);
		switch (result)
		{
			case LogicErrorType.NeedHigherLevel:
				title = "You need a higher level!";
				body = $"You are level {profile.Level}, you need to be level {item.LevelRequirement} to purchase this item";
				break;
			case LogicErrorType.NotEnoughDoubloons:
				title = "You need more points!";
				body = $"This item costs {item.PointCost} points, you only have {profile.Points} points";
				break;
			default:
				break;
		}
		if (result != LogicErrorType.NoError)
		{
            shop.displayError(title, body, close);
        }
		Close();
	}
}
