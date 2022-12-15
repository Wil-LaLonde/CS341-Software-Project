using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Model class used to represent a Shop
///     Primary Author: Wil LaLonde
///     Secondary Author: Sean Stille
///     Reviewer: Maximilian Patterson
/// </summary>
public class Shop : ObservableObject {
    /// <summary>
    ///     Shop constructor
    /// </summary>
    public Shop() {
        ShopItemList = new ObservableCollection<ShopItem>();
    }

    public ObservableCollection<ShopItem> ShopItemList { get; }

    /// <summary>
    ///     Adding a ShopItem to a Shop
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    public void AddShopItemToShop(ShopItem shopItem) {
        ShopItemList.Add(shopItem);
    }

    /// <summary>
    ///     Removing a ShopItem from a Shop
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    public void RemoveShopItemFromShop(ShopItem shopItem) {
        ShopItemList.Remove(shopItem);
    }
}