using AcademicReward.Database;
using AcademicReward.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ShopLogic()
        {
            ShopData = new ShopItemDatabase();
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

            toBeAdded = new ModelClass.ShopItem(AddItemVals[0], AddItemVals[1], cost, level, null);

            ShopData.AddItem(toBeAdded);

            return LogicErrorType.NoError;
        }

        public LogicErrorType DeleteItem(object obj)
        {
            throw new NotImplementedException();
        }

        public LogicErrorType LookupItem(object obj)
        {
            throw new NotImplementedException();
        }

        public LogicErrorType UpdateItem(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
