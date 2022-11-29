using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Primary Author: Xee Lo
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class Task : ObservableObject {
        public const int MinTitleLength = 0;
        public const int MaxTitleLength = 50;
        public const int MinDescriptionLength = 0;
        public const int MaxDescriptionLength = 250;

        public bool IsChecked { get; set; }
        public string Title { get; set;}
           //Title should have limited chars
        public string Description { get; set; }
        public int Points { get; set; }
        public string Date { get; set; }

        /// <summary>
        /// Task constructor
        /// </summary>
        /// <param name="isChecked">bool isChecked</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="points">int points</param>
        /// <param name="date">string date</param>
        public Task(bool isChecked, string title, string description, int points, string date) {
            IsChecked = isChecked;
            Title = title;
            Description = description;
            Points = points;
            Date = date;
        }   
    }
}
