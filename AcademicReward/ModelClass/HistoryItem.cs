using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: Xee Lo
    /// </summary>
    public class HistoryItem : ObservableObject
    {
        public int HistoryId { get; set; }
        public int ProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public HistoryItem(int historyid, int profileid, string title, string description)
        {
            this.HistoryId = historyid;
            this.ProfileId = profileid;
            this.Title = title;
            this.Description = description;
        }
    }
}
