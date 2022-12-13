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

        /// <summary>
        /// Edits a given shop item by replacing the variables in the shop item with new, editted variables.
        /// </summary>
        /// <param name="ShopItem">ShopItem shopItem</param>
        /// <param name="newValues">The new shopitem which will take ShopItems place</param>
        public void editShopItemFromShop(ShopItem ShopItem, ShopItem newValues) {
            newValues.Id = ShopItem.Id;
            RemoveShopItemFromShop(ShopItem);
            AddShopItemToShop(newValues);
        }
    }
}
