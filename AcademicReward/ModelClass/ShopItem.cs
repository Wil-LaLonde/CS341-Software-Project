using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class ShopItem : ObservableObject {

        public string Title { get; set; }
        public string Description { get; set; }
        public int PointCost { get; set; }
        public int LevelRequirement { get; set; }
        public Group Group { get; set; }

        /// <summary>
        /// ShopItem constructor
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
    }
}
