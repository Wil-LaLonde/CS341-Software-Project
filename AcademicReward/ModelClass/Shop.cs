using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
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
    }
}
