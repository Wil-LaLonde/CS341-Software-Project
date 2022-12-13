namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;

/// <summary>
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ViewShopItemPage : Popup {
	ShopLogic logic;
	ShopItem item;
	ShopPage shop;
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

    public ViewShopItemPage(ShopItem viewedItem, ShopLogic log, ShopPage page)
    {
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

    private void EditClicked(object sender, EventArgs e)
	{
		Close();
        shop.openEditPage(item);     
    }

    private void BackButtonClicked(object sender, EventArgs e) => Close();

	private void DeleteClicked(object sender, EventArgs e)
	{
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