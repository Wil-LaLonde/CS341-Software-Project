using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer:
/// </summary>
public class PurchaseHistoryItem : ObservableObject {
    /// <summary>
    ///     PurchaseHistoryItem constructor
    /// </summary>
    /// <param name="ProfileId">int ProfileId</param>
    /// <param name="ShopItemId">int ShopItemId</param>
    public PurchaseHistoryItem(int ProfileId, int ShopItemId) {
        this.ProfileId = ProfileId;
        this.ShopItemId = ShopItemId;
    }

    /// <summary>
    ///     PurchaseHistoryItem constructor w/ title and description
    /// </summary>
    /// <param name="ProfileId">int ProfileId</param>
    /// <param name="ShopItemId">int ShopItemId</param>
    /// <param name="Title">string Title</param>
    /// <param name="Description">string Description</param>
    public PurchaseHistoryItem(int ProfileId, int ShopItemId, string Title, string Description) {
        this.ProfileId = ProfileId;
        this.ShopItemId = ShopItemId;
        this.Title = Title;
        this.Description = Description;
    }

    public int ProfileId { get; set; }
    public int ShopItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}