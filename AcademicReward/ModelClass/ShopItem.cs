using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Model class used to represent a shop item
///     Primary Author: Wil LaLonde
///     Secondary Author: Sean Stille
///     Reviewer: Maximilian Patterson
/// </summary>
public class ShopItem : ObservableObject {
    public const int MinTitleLength = 0;
    public const int MaxTitleLength = 50;
    public const int MinDescriptionLength = 0;
    public const int MaxDescriptionLength = 250;
    public const int MinCostValue = 0;
    public const int MinLevelRequirement = 1;
    public const int DeleteShopItemSuccesValue = -1;
    public const int BuyShopItemSuccessValue = -2;
    private string _description;
    private Group _group;

    private int _id;
    private int _levelRequirement;
    private int _pointCost;
    private string _title;

    /// <summary>
    ///     ShopItem constructor
    /// </summary>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="pointCost">int cost</param>
    /// <param name="levelRequirement">int levelRequirement</param>
    /// <param name="group">Group group</param>
    public ShopItem(string title, string description, int pointCost, int levelRequirement, Group group) {
        Title = title;
        Description = description;
        PointCost = pointCost;
        LevelRequirement = levelRequirement;
        Group = group;
    }

    /// <summary>
    ///     ShopItem constructor (when pulling from the database)
    /// </summary>
    /// <param name="id">int id</param>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="pointCost">int pointCost</param>
    /// <param name="levelRequirement">int levelRequirement</param>
    /// <param name="group">Group group</param>
    public ShopItem(int id, string title, string description, int pointCost, int levelRequirement, Group group) {
        Id = id;
        Title = title;
        Description = description;
        PointCost = pointCost;
        LevelRequirement = levelRequirement;
        Group = group;
    }

    public int Id {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Title {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public string Description {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public int PointCost {
        get => _pointCost;
        set => SetProperty(ref _pointCost, value);
    }

    public int LevelRequirement {
        get => _levelRequirement;
        set => SetProperty(ref _levelRequirement, value);
    }

    public Group Group {
        get => _group;
        set => SetProperty(ref _group, value);
    }
}