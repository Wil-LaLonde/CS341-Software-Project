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
public partial class EditShopItemPage : Popup {
	ILogic logic;
	ShopItem changing;
	ShopPage page;
	public EditShopItemPage(ILogic ShopLogic, ShopItem toBeChanged, ShopPage shop) {
		InitializeComponent();
		logic = ShopLogic;
		changing = toBeChanged;
		page = shop;
		
	}

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

    private void BackButtonClicked(object sender, EventArgs e) => Close();
}