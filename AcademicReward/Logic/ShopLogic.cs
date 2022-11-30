using AcademicReward.Database;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
namespace AcademicReward.Logic
{
    /**
 *  Primary Author: Sean Stille
 *  Reviewer: TBD
 *  Desc: The logic implementation for items within the shop, adding them, deleting them, etc.
 */
    public class ShopLogic : ILogic
    {
        IDatabase ShopData;
        public ObservableCollection<ShopItem> ItemList;
        public ShopLogic()
        {
            ShopData = new ShopItemDatabase(this);
            ItemList = new ObservableCollection<ShopItem>();
        }
        public LogicErrorType AddItem(object obj)
        {
            ModelClass.ShopItem toBeAdded;
            String[] AddItemVals = (String[])obj;
            int cost = -1;
            int level = -1;
            if(! int.TryParse(AddItemVals[2], out cost)) //Testing cost, if it doesn't work, throw exception
            {
                //return LogicErrorType.InvalidCost;
            }
            if( !int.TryParse(AddItemVals[3], out level)) // Testing level
            {
                //return LogicErrorType.InvalidLevel;
            }

            toBeAdded = new ShopItem(AddItemVals[0], AddItemVals[1], cost, level, null);
            
            
            ItemList.Add(toBeAdded);
            if (ShopData.AddItem(toBeAdded) == DatabaseErrorType.NoError)
            {
                return LogicErrorType.NoError;
            }
            else
            {
                return LogicErrorType.LookupAllTasksDBError;
            }
        }

        public LogicErrorType DeleteItem(object obj)
        {
            ShopItem item = obj as ShopItem;
            ShopItem toBeRemoved = null;
            foreach ( ShopItem i in ItemList)
            {
                if (i.Id == item.Id)
                {
                    toBeRemoved = i;
                }
            }
            ItemList.Remove(toBeRemoved);
            ShopData.DeleteItem(toBeRemoved);
            return LogicErrorType.NoError;
        }

        public LogicErrorType LookupItem(object obj)
        {
            ShopData.LookupFullItem(null);
            return LogicErrorType.NoError;
        }

        public LogicErrorType UpdateItem(object obj)
        {
            ModelClass.ShopItem toBeAdded;
            String[] AddItemVals = (String[])obj;
            int cost = -1;
            int level = -1;
            if (!int.TryParse(AddItemVals[2], out cost)) //Testing cost, if it doesn't work, throw exception
            {
                //return LogicErrorType.InvalidCost;
            }
            if (!int.TryParse(AddItemVals[3], out level)) // Testing level
            {
                //return LogicErrorType.InvalidLevel;
            }

            toBeAdded = new ShopItem(int.Parse(AddItemVals[5]), AddItemVals[0], AddItemVals[1], cost, level, null);

            
            if (ShopData.UpdateItem(toBeAdded) == DatabaseErrorType.NoError)
            {
                return LogicErrorType.NoError;
            }
            else
            {
                return LogicErrorType.LookupAllTasksDBError;
            }
            throw new NotImplementedException();
        }
    }
}
