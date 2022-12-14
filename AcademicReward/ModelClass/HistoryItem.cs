using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Model class used to represent a HistoryItem
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Xee Lo
    /// </summary>
    public class HistoryItem : ObservableObject {
        public int HistoryId { get; set; }
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// HistoryItem constructor (when making a new history event)
        /// </summary>
        /// <param name="profileid">int profileid</param>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        public HistoryItem(int profileid, string title, string description) {
            ProfileId = profileid;
            Title = title;
            Description = description;
        }

        /// <summary>
        /// HistoryItem constructor (when pulling from the database)
        /// </summary>
        /// <param name="historyid"></param>
        /// <param name="profileid"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public HistoryItem(int historyid, int profileid, string title, string description) {
            HistoryId = historyid;
            ProfileId = profileid;
            Title = title;
            Description = description;
        }
    }
}
