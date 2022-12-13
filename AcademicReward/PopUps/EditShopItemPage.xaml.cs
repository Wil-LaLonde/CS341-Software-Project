namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;

/// <summary>
/// EditShopItemPage is the popup to edit a shop item
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class EditShopItemPage : Popup {
	ILogic logic;
	ShopItem changing;
	ShopPage page;

	/// <summary>
	/// EditShopItemPage constructor
	/// </summary>
	/// <param name="ShopLogic">ILogic ShopLogic</param>
	/// <param name="toBeChanged">ShopItem toBeChanged</param>
	/// <param name="shop">ShopPage shop</param>
	public EditShopItemPage(ILogic ShopLogic, ShopItem toBeChanged, ShopPage shop) {
		InitializeComponent();
		logic = ShopLogic;
		changing = toBeChanged;
		page = shop;
		
	}

	/// <summary>
	/// Method called when a user clicks the add button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void AddClicked(object sender, EventArgs e) {
		String itemID = changing.Id + "";
		String itemName = name.Text;
		String itemDesc = description.Text;
		String itemCost = cost.Text;
		string itemLevel = levelRec.Text;
		String itemGroup = "";					//Not sure how to pull this from the array of groups
		String[] itemValues = { name.Text, description.Text, cost.Text, levelRec.Text, itemGroup, itemID };
		logic.UpdateItem(itemValues);
		page.getShopItems();					//Updating list, without this i had to press the add item button to refresh
		Close();
	}

	/// <summary>
	/// Method called when a user clicks the back button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();
}
