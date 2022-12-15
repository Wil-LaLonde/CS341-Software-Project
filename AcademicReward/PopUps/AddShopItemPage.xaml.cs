namespace AcademicReward;

using AcademicReward.Logic;
using CommunityToolkit.Maui.Views;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System.Text;

/// <summary>
/// AddShopItemPage is the popup when adding a new shop item
/// Primary Author: Sean Stille
/// Secondary Author: Wil LaLonde
/// Reviewer: Wil LaLonde
/// </summary>
public partial class AddShopItemPage : Popup {
	ILogic shopLogic;

	/// <summary>
	/// AddShopItemPage constructor
	/// </summary>
	/// <param name="ShopLogic">ILogic ShopLogic</param>
	public AddShopItemPage(ILogic ShopLogic) {
		InitializeComponent();
        shopLogic = ShopLogic;
		GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

	/// <summary>
	/// Method called when a user clicks the add button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void AddClicked(object sender, EventArgs e) {
		LogicErrorType logicError;
		Group selectedGroup = GroupPicker.SelectedItem as Group;
		if (selectedGroup != null) {
			string itemName = name.Text ?? string.Empty;
			string itemDescription = description.Text ?? string.Empty;
			bool isValidItemCost = int.TryParse(cost.Text, out int itemCost);
			if(isValidItemCost) {
				bool isValidLevelRequirement = int.TryParse(levelRec.Text, out int levelRequirement);
				if(isValidLevelRequirement) {
					//Create ShopItem object
					ShopItem shopItemToAdd = new ShopItem(itemName, itemDescription, itemCost, levelRequirement, selectedGroup);
					logicError = shopLogic.AddItem(shopItemToAdd);
					//Shop item was added successfully
					if(LogicErrorType.NoError == logicError) {
						Close(shopItemToAdd);
					}
                } else {
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
		switch(logicError) {
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
			case LogicErrorType.AddShopItemDBError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddShopItemDBErrorMessage);
                break;
			default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddShopItemUnknownErrorMessage);
                break;
        }
		return errorMessageBuilder.ToString();
	}
}
