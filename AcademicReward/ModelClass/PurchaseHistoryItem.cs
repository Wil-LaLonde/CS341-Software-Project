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
    /// <param name="profileId">int ProfileId</param>
    /// <param name="shopItemId">int ShopItemId</param>
    public PurchaseHistoryItem(int profileId, int shopItemId) {
        this.ProfileId = profileId;
        this.ShopItemId = shopItemId;
    }

    /// <summary>
    ///     PurchaseHistoryItem constructor w/ title and description
    /// </summary>
    /// <param name="profileId">int ProfileId</param>
    /// <param name="shopItemId">int ShopItemId</param>
    /// <param name="title">string Title</param>
    /// <param name="description">string Description</param>
    public PurchaseHistoryItem(int profileId, int shopItemId, string title, string description) {
        this.ProfileId = profileId;
        this.ShopItemId = shopItemId;
        this.Title = title;
        this.Description = description;
    }

    public int ProfileId { get; set; }
    public int ShopItemId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}