using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic; 

/// <summary>
///     The logic implementation for items within the shop, adding them, deleting them, etc.
///     Primary Author: Sean Stille
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public class ShopLogic : ILogic {
    private readonly IDatabase historyDB;
    private readonly ShopItemDatabase shopDB;

    /// <summary>
    ///     ShopLogic constructor
    /// </summary>
    public ShopLogic() {
        shopDB = new ShopItemDatabase();
        historyDB = new HistoryDatabase();
    }

    /// <summary>
    ///     Method used to add a shop item (logic)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType AddItem(object shopItem) {
        LogicErrorType logicError;
        ShopItem shopItemToAdd = shopItem as ShopItem;
        //Checking user input
        logicError = CheckShopItem(shopItemToAdd);
        if (LogicErrorType.NoError == logicError) {
            DatabaseErrorType dbError = shopDB.AddItem(shopItemToAdd);
            if (DatabaseErrorType.NoError == dbError)
                //Adding new history item
                historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryAddShopItemTitle,
                    string.Format(DataConstants.HistoryAddShopItemDescription, shopItemToAdd.Title,
                        shopItemToAdd.Group.GroupName)));
            else
                logicError = LogicErrorType.AddShopItemDBError;
        }

        return logicError;
    }

    /// <summary>
    ///     Method used to delete a shop item (logic)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType DeleteItem(object shopItem) {
        LogicErrorType logicError;
        DatabaseErrorType dbError = shopDB.DeleteItem(shopItem);
        if (DatabaseErrorType.NoError == dbError) {
            logicError = LogicErrorType.NoError;
            //Add new history item
            ShopItem shopItemToDelete = shopItem as ShopItem;
            historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryDeleteShopItemTitle,
                string.Format(DataConstants.HistoryDeleteShopItemDescription, shopItemToDelete.Title,
                    shopItemToDelete.Group.GroupName)));
        }
        else {
            logicError = LogicErrorType.DeleteShopItemDBError;
        }

        return logicError;
    }

    /// <summary>
    ///     Method used to lookup all shop items (logic)
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType LookupItem(object profile) {
        LogicErrorType logicError;
        DatabaseErrorType dbError = shopDB.LookupFullItem(profile);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.LookupAllShopItemsDBError;
        return logicError;
    }

    /// <summary>
    ///     Method used to update a shop item (logic)
    /// </summary>
    /// <param name="shopItem">object shopItem</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType UpdateItem(object shopItem) {
        LogicErrorType logicError;
        ShopItem shopItemToUpdate = shopItem as ShopItem;
        //Checking user input
        logicError = CheckShopItem(shopItemToUpdate);
        if (LogicErrorType.NoError == logicError) {
            DatabaseErrorType dbError = shopDB.UpdateItem(shopItemToUpdate);
            if (DatabaseErrorType.NoError == dbError)
                //Adding new history item
                historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID,
                    DataConstants.HistoryUpdateShopItemTitle,
                    string.Format(DataConstants.HistoryUpdateShopItemDescription, shopItemToUpdate.Title,
                        shopItemToUpdate.Group.GroupName)));
            else
                logicError = LogicErrorType.UpdateShopItemDBError;
        }

        return logicError;
    }

    //Currently not needed
    public LogicErrorType AddItemWithArgs(object[] obj) {
        return LogicErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method used to buy a shop item (logic)
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType BuyItem(ShopItem shopItem) {
        LogicErrorType logicError = CheckBuyShopItem(MauiProgram.Profile, shopItem);
        if (LogicErrorType.NoError == logicError) {
            DatabaseErrorType dbError = shopDB.BuyItem(shopItem);
            if (DatabaseErrorType.NoError == dbError) {
                logicError = LogicErrorType.NoError;
                //Add new history item
                historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryBuyShopItemTitle,
                    string.Format(DataConstants.HistoryBuyShopItemDescription, shopItem.Title,
                        shopItem.Group.GroupName)));
            }
            else {
                logicError = LogicErrorType.BuyItemError;
            }
        }

        return logicError;
    }

    /// <summary>
    ///     Helper method used to check a shop item
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    private LogicErrorType CheckShopItem(ShopItem shopItem) {
        LogicErrorType logicError;
        if (string.IsNullOrEmpty(shopItem.Title))
            logicError = LogicErrorType.EmptyShopItemTitle;
        else if (string.IsNullOrEmpty(shopItem.Description))
            logicError = LogicErrorType.EmptyShopItemDescription;
        else if (CheckPointValue(shopItem.PointCost))
            logicError = LogicErrorType.NegativeShopItemCost;
        else if (CheckLevelRequirement(shopItem.LevelRequirement))
            logicError = LogicErrorType.NegativeShopItemLevelRequirement;
        else if (CheckItemTitleLength(shopItem.Title))
            logicError = LogicErrorType.InvalidShopItemLength;
        else if (CheckItemDescriptionLength(shopItem.Description))
            logicError = LogicErrorType.InvalidShopItemDescriptionLength;
        else
            logicError = LogicErrorType.NoError;
        return logicError;
    }

    /// <summary>
    ///     Helper method for checking when buying a shop item
    /// </summary>
    /// <param name="profile">Profile profile</param>
    /// <param name="shopItem">ShopItem shopItem</param>
    /// <returns>LogicErrorType logicError</returns>
    private LogicErrorType CheckBuyShopItem(Profile profile, ShopItem shopItem) {
        LogicErrorType logicError;
        if (profile.Level < shopItem.LevelRequirement)
            logicError = LogicErrorType.NeedHigherLevel;
        else if (profile.Points < shopItem.PointCost)
            logicError = LogicErrorType.NotEnoughDoubloons;
        else
            logicError = LogicErrorType.NoError;
        return logicError;
    }

    /// <summary>
    ///     Helper method used to check a point value
    /// </summary>
    /// <returns>true/false</returns>
    private bool CheckPointValue(int points) {
        return points < ShopItem.MinCostValue;
    }

    /// <summary>
    ///     Helper method used to check a level requirement
    /// </summary>
    /// <param name="levelRequirement">int levelRequirement</param>
    /// <returns>true/false</returns>
    private bool CheckLevelRequirement(int levelRequirement) {
        return levelRequirement < ShopItem.MinLevelRequirement;
    }

    /// <summary>
    ///     Helper method used to check a title's length
    /// </summary>
    /// <param name="itemTitle">string itemTitle</param>
    /// <returns>true/false</returns>
    private bool CheckItemTitleLength(string itemTitle) {
        int itemTitleLength = itemTitle.Length;
        return itemTitleLength < ShopItem.MinTitleLength || itemTitleLength > ShopItem.MaxTitleLength;
    }

    /// <summary>
    ///     Helper method used to check a description's length
    /// </summary>
    /// <param name="itemDescription">string itemDescription</param>
    /// <returns>true/false</returns>
    private bool CheckItemDescriptionLength(string itemDescription) {
        int itemDescriptionLength = itemDescription.Length;
        return itemDescriptionLength < ShopItem.MinDescriptionLength ||
            itemDescriptionLength > ShopItem.MaxDescriptionLength;
    }
}