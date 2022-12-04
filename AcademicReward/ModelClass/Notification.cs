using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class Notification : ObservableObject {
        public const int MinTitleLength = 0;
        public const int MaxTitleLength = 50;
        public const int MinDescriptionLength = 0;
        public const int MaxDescriptionLength = 250;

        public int NotificationID { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int GroupID { get; set; }

        /// <summary>
        /// Notification constructor (when making a new Notification)
        /// </summary>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="groupID">int groupID</param>
        public Notification(string title, string description, int groupID) {
            Title = title;
            Description = description;
            GroupID = groupID;
        }

        /// <summary>
        /// Notification constructor (when getting exisiting Notifications)
        /// </summary>
        /// <param name="notificationID">int notificationID</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="groupID">int groupID</param>
        public Notification(int notificationID, string title, string description, int groupID) {
            NotificationID = notificationID;
            Title = title;
            Description = description;
            GroupID = groupID;
        }
    }
}
