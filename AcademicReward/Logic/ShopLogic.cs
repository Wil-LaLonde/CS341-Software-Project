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
        IDatabase ShopData;
        public ObservableCollection<ShopItem> ItemList;

        /// <summary>
        /// ShopLogic constructor
        /// </summary>
        public ShopLogic() {
            ShopData = new ShopItemDatabase(this);
            ItemList = new ObservableCollection<ShopItem>();
        }

        /// <summary>
        /// Method used to add a shop item (logic)
        /// </summary>
        /// <param name="obj">object obj</param>
        /// <returns>LogicErrorType logicError</returns>
        public LogicErrorType AddItem(object obj) {
            ModelClass.ShopItem toBeAdded;
            String[] AddItemVals = (String[])obj;
            int cost = -1;
            int level = -1;
            //Testing cost, if it doesn't work, throw exception
            if (! int.TryParse(AddItemVals[2], out cost)) {
                return LogicErrorType.InvalidCost;
            }
            // Testing level
            if ( !int.TryParse(AddItemVals[3], out level))  {
                return LogicErrorType.InvalidLevel;
            }
            
            toBeAdded = new ShopItem(AddItemVals[0], AddItemVals[1], cost, level, null);
            
            ItemList.Add(toBeAdded);
            if (ShopData.AddItem(toBeAdded) == DatabaseErrorType.NoError) {
                return LogicErrorType.NoError;
            } else {
                return LogicErrorType.UnsuccessfulDBAdd;
            }
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
    }
}
