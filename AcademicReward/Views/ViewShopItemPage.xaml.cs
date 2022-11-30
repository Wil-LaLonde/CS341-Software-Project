namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
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
}