using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Member : Profile {
        private ObservableCollection<ShopItem> purchaseItems;

        public int XP { get; private set; }
        public int Level { get; private set; }

        /// <summary>
        /// Member constructor
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        public Member(string username, string password) : base(username, password) {
            XP = 0;
            Level = 1;
            purchaseItems = new ObservableCollection<ShopItem>();
        }

        /// <summary>
        /// Adds XP to a Member
        /// </summary>
        /// <param name="xp">int xp</param>
        public void AddXPToMember(int xp) {
            //Maybe add some logic here for different levels?
            //This could call the LevelUpMember method to do so.
            if(xp > 0) {
                XP += xp;
            }
        }

        /// <summary>
        /// Add a level to a Member
        /// </summary>
        private void LevelUpMember() { Level++; }

        /// <summary>
        /// Adds a ShopItem to a Member's purchased items list
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void AddShopItemToPurchaseList(ShopItem shopItem) {
            purchaseItems.Add(shopItem);
        }

        /// <summary>
        /// Removes a ShopItem from a Member's purchased items list
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void RemoveShopItemFromPurchaseList(ShopItem shopItem) {
            purchaseItems.Remove(shopItem);
        }
    }
}
