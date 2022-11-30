namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
using CommunityToolkit.Maui.Views;
public partial class AddShopItemPage : Popup {
	ILogic logic;
	public AddShopItemPage(ILogic ShopLogic) {
		InitializeComponent();
		logic = ShopLogic;
	}

	private void AddClicked(object sender, EventArgs e) {
		String itemName = name.Text;
		String itemDesc = description.Text;
		String itemCost = cost.Text;
		string itemLevel = levelRec.Text;
		String itemGroup = ""; //Not sure how to pull this from the array of groups
		String[] itemValues = { name.Text, description.Text, cost.Text, levelRec.Text, itemGroup };
		logic.AddItem(itemValues);
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();
}