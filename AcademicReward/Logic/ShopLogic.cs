using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System.Collections.ObjectModel;

namespace AcademicReward.Logic {
    /// <summary>
    /// The logic implementation for items within the shop, adding them, deleting them, etc.
    /// Primary Author: Sean Stille
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class ShopLogic : ILogic {
        IDatabase historyDB;
        ShopItemDatabase ShopData;
        public ObservableCollection<ShopItem> ItemList;

        /// <summary>
        /// ShopLogic constructor
        /// </summary>
        public ShopLogic() {
            ShopData = new ShopItemDatabase(this);
            ItemList = new ObservableCollection<ShopItem>();
            historyDB = new HistoryDatabase();
        }


        public LogicErrorType BuyItem(ShopItem item)
        {
            Profile currentMember = MauiProgram.Profile;
            if (currentMember.Level < item.LevelRequirement)
            {
                return LogicErrorType.NeedHigherLevel;
            }
            if (currentMember.Points < item.PointCost)
            {
                return LogicErrorType.NotEnoughDoubloons;
            }
            if ( ShopData.BuyItem(item) != DatabaseErrorType.NoError)
            {
                return LogicErrorType.UnsuccessfulDBAdd;
            }

            currentMember.Points -= item.PointCost;
            //Need to make a database call here to change the point values
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method used to add a shop item (logic)
        /// </summary>
        /// <param name="shopItem">object shopItem</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType AddItem(object shopItem) {
            LogicErrorType logicError;
            ShopItem shopItemToAdd = shopItem as ShopItem;
            //Checking user input
            logicError = AddShopItemCheck(shopItemToAdd);
            if(LogicErrorType.NoError == logicError) {
                DatabaseErrorType dbError = ShopData.AddItem(shopItemToAdd);
                if(DatabaseErrorType.NoError == dbError) {
                    ItemList.Add(shopItemToAdd);
                    //Adding new history item
                    historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryAddShopItemTitle, 
                        string.Format(DataConstants.HistoryAddShopItemDescription, shopItemToAdd.Title, shopItemToAdd.Group.GroupName)));
                } else {
                    logicError = LogicErrorType.AddShopItemDBError;
                }
            }
            return logicError;
        }

        /// <summary>
        /// Method used to delete a shop item (logic)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType DeleteItem(object obj) {
            ShopItem item = obj as ShopItem;
            ShopItem toBeRemoved = null;
            foreach ( ShopItem i in ItemList) {
                if (i.Id == item.Id) {
                    toBeRemoved = i;
                }
            }
            ItemList.Remove(toBeRemoved);
            ShopData.DeleteItem(toBeRemoved);
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method used to lookup all shop items (logic)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType LookupItem(object obj) {
            ShopData.LookupFullItem(null);
            return LogicErrorType.NoError;
        }

        /// <summary>
        /// Method used to update a shop item (logic)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType UpdateItem(object obj) {
            ModelClass.ShopItem toBeAdded;
            String[] AddItemVals = (String[])obj;
            int cost = -1;
            int level = -1;
            //Testing cost, if it doesn't work, throw exception
            if (!int.TryParse(AddItemVals[2], out cost))  {
                return LogicErrorType.InvalidCost;
            }
            // Testing level
            if (!int.TryParse(AddItemVals[3], out level))  {
                return LogicErrorType.InvalidLevel;
            }

            toBeAdded = new ShopItem(int.Parse(AddItemVals[5]), AddItemVals[0], AddItemVals[1], cost, level, null);
            
            if (ShopData.UpdateItem(toBeAdded) == DatabaseErrorType.NoError) {
                return LogicErrorType.NoError;
            } else {
                return LogicErrorType.UnsuccessfulDBAdd;
            }
        }

        //Currently not needed
        public LogicErrorType AddItemWithArgs(object[] obj) {
            return LogicErrorType.NotImplemented;
        }

        /// <summary>
        /// Helper method used to check a shop item
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        private LogicErrorType AddShopItemCheck(ShopItem shopItem) {
            LogicErrorType logicError;
            if (string.IsNullOrEmpty(shopItem.Title)) {
                logicError = LogicErrorType.EmptyShopItemTitle;
            } else if(string.IsNullOrEmpty(shopItem.Description)) {
                logicError = LogicErrorType.EmptyTaskDescription;
            } else if(CheckPointValue(shopItem.PointCost)) {
                logicError = LogicErrorType.NegativeShopItemCost;
            } else if(CheckLevelRequirement(shopItem.LevelRequirement)) {
                logicError = LogicErrorType.NegativeShopItemLevelRequirement;
            } else if(CheckItemTitleLength(shopItem.Title)) {
                logicError = LogicErrorType.InvalidShopItemLength;
            } else if(CheckItemDescriptionLength(shopItem.Description)) {
                logicError = LogicErrorType.InvalidShopItemDescriptionLength;
            } else {
                logicError = LogicErrorType.NoError;
            }
            return logicError;
        }

        /// <summary>
        /// Helper method used to check a point value
        /// </summary>
        /// <returns>true/false</returns>
        private bool CheckPointValue(int points) {
            return points < ShopItem.MinCostValue;
        }  

        /// <summary>
        /// Helper method used to check a level requirement
        /// </summary>
        /// <param name="levelRequirement">int levelRequirement</param>
        /// <returns>true/false</returns>
        private bool CheckLevelRequirement(int levelRequirement) {
            return levelRequirement < ShopItem.MinLevelRequirement;
        }

        /// <summary>
        /// Helper method used to check a title's length
        /// </summary>
        /// <param name="itemTitle">string itemTitle</param>
        /// <returns>true/false</returns>
        private bool CheckItemTitleLength(string itemTitle) {
            int itemTitleLength = itemTitle.Length;
            return itemTitleLength < ShopItem.MinTitleLength || itemTitleLength > ShopItem.MaxTitleLength;
        }

        /// <summary>
        /// Helper method used to check a description's length
        /// </summary>
        /// <param name="itemDescription">string itemDescription</param>
        /// <returns>true/false</returns>
        private bool CheckItemDescriptionLength(string itemDescription) {
            int itemDescriptionLength = itemDescription.Length;
            return itemDescriptionLength < ShopItem.MinDescriptionLength || itemDescriptionLength > ShopItem.MaxDescriptionLength;
        }
    }
}
