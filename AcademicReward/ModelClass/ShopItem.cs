using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Model class used to represent a shop item
    /// Primary Author: Wil LaLonde
    /// Secondary Author: Sean Stille
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class ShopItem : ObservableObject {

        public static int IdCounter = 0;
        public int Id { get; set; }
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
            Id = ++IdCounter;
        }

        /// <summary>
        /// ShopItem constructor (when pulling from the database)
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
            if (id > IdCounter) {
                IdCounter = id++;               //This prevents overlapping IDs
            }
        }
    }
}
