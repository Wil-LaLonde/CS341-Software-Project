using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Model class used to represent a Shop
    /// Primary Author: Wil LaLonde
    /// Secondary Author: Sean Stille
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class Shop : ObservableObject {
        private ObservableCollection<ShopItem> shopItemList;

        public ObservableCollection<ShopItem> ShopItemList { get { return shopItemList; } }

        /// <summary>
        /// Shop constructor
        /// </summary>
        public Shop() {
            shopItemList = new ObservableCollection<ShopItem>();
        }

        /// <summary>
        /// Adding a ShopItem to a Shop
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void AddShopItemToShop(ShopItem shopItem) {
            shopItemList.Add(shopItem);
        }

        /// <summary>
        /// Removing a ShopItem from a Shop
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void RemoveShopItemFromShop(ShopItem shopItem) {
            shopItemList.Remove(shopItem);
        }
    }
}
