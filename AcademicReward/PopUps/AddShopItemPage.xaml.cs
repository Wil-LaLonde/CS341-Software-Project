namespace AcademicReward;

using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;

/// <summary>
/// AddShopItemPage is the popup when adding a new shop item
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class AddShopItemPage : Popup {
	ILogic logic;

	/// <summary>
	/// AddShopItemPage constructor
	/// </summary>
	/// <param name="ShopLogic">ILogic ShopLogic</param>
	public AddShopItemPage(ILogic ShopLogic) {
		InitializeComponent();
		logic = ShopLogic;
		GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
	}

	/// <summary>
	/// Method called when a user clicks the add button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void AddClicked(object sender, EventArgs e) {
		String itemName = name.Text;
		String itemDesc = description.Text;
		String itemCost = cost.Text;
		string itemLevel = levelRec.Text;
		String itemGroup = ""; //Not sure how to pull this from the array of groups
		String[] itemValues = { name.Text, description.Text, cost.Text, levelRec.Text, itemGroup, GroupPicker.SelectedIndex + ""};
		logic.AddItem(itemValues);
		
		Close();
	}

	/// <summary>
	/// Method called when a user clicks the back button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();
}
