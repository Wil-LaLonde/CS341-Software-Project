namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
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
}
