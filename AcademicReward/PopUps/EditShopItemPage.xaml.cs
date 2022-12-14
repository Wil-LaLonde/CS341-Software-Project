namespace AcademicReward;

using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;
using System.Text;
using AcademicReward.Resources;

/// <summary>
/// EditShopItemPage is the popup to edit a shop item
/// Primary Author: Sean Stille
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class EditShopItemPage : Popup {
	ILogic shopLogic;
	ShopItem shopItem;
	ShopPage shopPage;

	/// <summary>
	/// EditShopItemPage constructor
	/// </summary>
	/// <param name="ShopLogic">ILogic ShopLogic</param>
	/// <param name="toBeChanged">ShopItem toBeChanged</param>
	/// <param name="shop">ShopPage shop</param>
	public EditShopItemPage(ILogic ShopLogic, ShopItem toBeChanged, ShopPage shop) {
		InitializeComponent();
		shopLogic = ShopLogic;
        shopItem = toBeChanged;
        shopPage = shop;
        GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
        //Setting values to make updating easier for user
        name.Text = shopItem.Title;
        description.Text = shopItem.Description;
        cost.Text = shopItem.PointCost.ToString();
        levelRec.Text = shopItem.LevelRequirement.ToString();
        GroupPicker.SelectedItem = shopItem.Group;
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

	/// <summary>
	/// Method called when a user clicks the update button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void UpdateClicked(object sender, EventArgs e) {
        LogicErrorType logicError;
        Group selectedGroup = GroupPicker.SelectedItem as Group;
        if (selectedGroup != null) {
            string itemName = name.Text ?? string.Empty;
            string itemDescription = description.Text ?? string.Empty;
            bool isValidItemCost = int.TryParse(cost.Text, out int itemCost);
            if (isValidItemCost) {
                bool isValidLevelRequirement = int.TryParse(levelRec.Text, out int levelRequirement);
                if (isValidLevelRequirement) {
                    //Create shop item that will replace the old one
                    ShopItem shopItemToUpdate = new ShopItem(shopItem.Id, itemName, itemDescription, itemCost, levelRequirement, selectedGroup);
                    logicError = shopLogic.UpdateItem(shopItemToUpdate);
                    //Shop item was updated successfully
                    if (LogicErrorType.NoError == logicError) {
                        //Update shop item values with new ones
                        shopItem.Title = shopItemToUpdate.Title;
                        shopItem.Description = shopItemToUpdate.Description;
                        shopItem.PointCost = shopItemToUpdate.PointCost;
                        shopItem.LevelRequirement = shopItemToUpdate.LevelRequirement;
                        shopItem.Group = shopItemToUpdate.Group;
                        Close(shopItemToUpdate);
                    }
                }
                else {
                    logicError = LogicErrorType.InvalidLevel;
                }
            } else {
                logicError = LogicErrorType.InvalidCost;
            }
        } else {
            logicError = LogicErrorType.EmptyShopItemGroup;
        }
        // There was some kind of error, show messages
        SetErrorMessageBox(true, SetErrorMessageBody(logicError));
	}

	/// <summary>
	/// Method called when a user clicks the back button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

    /// <summary>
    /// Helper method used to either show or hide the error message box
    /// This is done since a popup cannot have another popup
    /// </summary>
    /// <param name="isVisible">bool isVisible</param>
    /// <param name="errorMessage">string errorMessage</param>
    private void SetErrorMessageBox(bool isVisible, string errorMessage) {
        ErrorFrame.IsVisible = isVisible;
        ErrorStackLayout.IsVisible = isVisible;
        ErrorMessageHeader.IsVisible = isVisible;
        ErrorMessageBody.IsVisible = isVisible;
        ErrorMessageBody.Text = errorMessage;
    }

    /// <summary>
    /// Helper method used to determine what error message to display.
    /// </summary>
    /// <param name="logicError">LogicErrorType logicError</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
        StringBuilder errorMessageBuilder = new StringBuilder();
        switch (logicError)
        {
            case LogicErrorType.EmptyShopItemGroup:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyShopItemGroupMessage);
                break;
            case LogicErrorType.EmptyShopItemTitle:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyShopItemTitleMessage);
                break;
            case LogicErrorType.EmptyShopItemDescription:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyShopItemDescriptionMessage);
                break;
            case LogicErrorType.InvalidCost:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidItemCostMessage);
                break;
            case LogicErrorType.InvalidLevel:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidLevelMessage);
                break;
            case LogicErrorType.NegativeShopItemCost:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.NegativeShopItemCostMessage);
                break;
            case LogicErrorType.NegativeShopItemLevelRequirement:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.NegativeShopItemLevelRequirementMessage);
                break;
            case LogicErrorType.InvalidShopItemLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidShopItemTitleLengthMessage);
                break;
            case LogicErrorType.InvalidShopItemDescriptionLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidShopItemDescriptionLengthMessage);
                break;
            case LogicErrorType.UpdateShopItemDBError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.UpdateShopItemDBErrorMessage);
                break;
            default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.UpdateShopItemUnknownErrorMessage);
                break;
        }
        return errorMessageBuilder.ToString();
    }
}
