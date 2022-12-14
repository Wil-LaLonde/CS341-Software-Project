using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Model class used to represent a task
    /// Primary Author: Xee Lo
    /// Secondary Author: None
    /// Reviewer: Wil LaLonde
    /// </summary>
    public class Task : ObservableObject {
        public const int MinTitleLength = 0;
        public const int MaxTitleLength = 50;
        public const int MinDescriptionLength = 0;
        public const int MaxDescriptionLength = 250;
        public const int MinPointValue = 0;

        public int TaskID { get; private set; }
        public bool IsChecked { get; set; }
        public string Title { get; set;}
        public string Description { get; set; }
        public int Points { get; set; }
        public int GroupID { get; private set; }
        public bool IsSubmitted { get; set; }

        /// <summary>
        /// Task constructor
        /// </summary>
        /// <param name="isChecked">bool isChecked</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="points">int points</param>
        public Task(bool isChecked, string title, string description, int points) {
            IsChecked = isChecked;
            Title = title;
            Description = description;
            Points = points;
        }  
        
        /// <summary>
        /// Task constructor (used when creating a new task)
        /// </summary>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="points">int points</param>
        /// <param name="groupID">int groupID</param>
        public Task(string title, string description, int points, int groupID) {
            IsChecked = false;
            IsSubmitted = false;
            Title = title;
            Description = description;
            Points = points;
            GroupID = groupID;
        }

        /// <summary>
        /// Task constructor (used when gathering a task)
        /// </summary>
        /// <param name="taskID">int taskID</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="points">int points</param>
        /// <param name="groupID">int groupID</param>
        /// <param name="isChecked">bool isChecked</param>
        public Task(int taskID, string title, string description, int points, int groupID, bool isChecked) {
            IsChecked = isChecked;
            TaskID = taskID;
            Title = title;
            Description = description;
            Points = points;
            GroupID = groupID;
        }

        /// <summary>
        /// Task constructor (used for checking if the task should be displayed for memeber or admin)
        /// </summary>
        /// <param name="taskID">int taskID</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="points">int points</param>
        /// <param name="groupID">int groupID</param>
        /// <param name="isChecked">bool isChecked</param>
        /// <param name="isSubmitted">bool isSubmitted</param>
        public Task(int taskID, string title, string description, int points, int groupID, bool isChecked, bool isSubmitted) {
            IsChecked = isChecked;
            IsSubmitted = isSubmitted;
            TaskID = taskID;
            Title = title;
            Description = description;
            Points = points;
            GroupID = groupID;
        }
    }
}
