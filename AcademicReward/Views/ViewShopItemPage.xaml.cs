using System.Text;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;

namespace AcademicReward;

/// <summary>
///     ViewShopItemPage shows a given shop item
///     Primary Author: Sean Stille
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class ViewShopItemPage : Popup {
    private readonly ShopItem _shopItem;
    private readonly ShopLogic _shopLogic;
    private readonly ShopPage _shopPage;

    /// <summary>
    ///     ViewShopItemPage constructor
    /// </summary>
    /// <param name="viewedItem">ShopItem viewedItem</param>
    /// <param name="log">ShopLogic log</param>
    /// <param name="page">ShopPage page</param>
    public ViewShopItemPage(ShopItem viewedItem, ShopLogic log, ShopPage page) {
        InitializeComponent();
        _shopLogic = log;
        _shopItem = viewedItem;
        ItemTitle.Text = _shopItem.Title;
        Desc.Text = _shopItem.Description;
        LevelRequirement.Text = _shopItem.LevelRequirement.ToString();
        Cost.Text = _shopItem.PointCost.ToString();
        _shopPage = page;
        SetVisibility(MauiProgram.Profile.IsAdmin);
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

    /// <summary>
    ///     Helper method to set items on the page to visible or not
    /// </summary>
    /// <param name="isAdmin">bool isAdmin</param>
    private void SetVisibility(bool isAdmin) {
        if (isAdmin) {
            EditButton.IsVisible = true;
            DeleteButton.IsVisible = true;
            BuyButton.IsVisible = false;
        }
        else {
            EditButton.IsVisible = false;
            DeleteButton.IsVisible = false;
            BuyButton.IsVisible = true;
        }
    }

    /// <summary>
    ///     Method called when the edit button is clicked
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void EditClicked(object sender, EventArgs e) {
        Close();
        _shopPage.OpenEditPage(_shopItem);
    }

    /// <summary>
    ///     Method called when a user clicks on the back button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) {
        Close();
    }

    /// <summary>
    ///     Method called when a user clicks on the delete button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void DeleteClicked(object sender, EventArgs e) {
        LogicErrorType logicError = _shopLogic.DeleteItem(_shopItem);
        if (LogicErrorType.NoError == logicError) _shopItem.Id = ShopItem.DeleteShopItemSuccesValue;
        Close(_shopItem);
    }

    /// <summary>
    ///     Method called when a user clicks on the buy button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void BuyClicked(object sender, EventArgs e) {
        LogicErrorType logicError = _shopLogic.BuyItem(_shopItem);
        if (LogicErrorType.NoError == logicError) {
            _shopItem.Id = ShopItem.BuyShopItemSuccessValue;
            Close(_shopItem);
        }

        //Something went wrong, show error message
        SetErrorMessageBox(true, SetErrorMessageBody(logicError));
    }

    /// <summary>
    ///     Helper method used to either show or hide the error message box
    ///     This is done since a popup cannot have another popup
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
    ///     Helper method used to determine what error message to display.
    /// </summary>
    /// <param name="logicError">LogicErrorType logicError</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
        StringBuilder errorMessageBuilder = new();
        switch (logicError) {
            case LogicErrorType.NeedHigherLevel:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.BuyShopItemLevelRequirementMessage);
                break;
            case LogicErrorType.NotEnoughDoubloons:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.BuyShopItemNotEnoughPointsMessage);
                break;
            case LogicErrorType.BuyItemError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.BuyShopItemDbErrorMessage);
                break;
            default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.BuyShopItemUnknownErrorMessage);
                break;
        }

        return errorMessageBuilder.ToString();
    }
}
