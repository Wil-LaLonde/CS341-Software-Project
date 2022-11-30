using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass
{
    /// <summary>
    /// Primary Author: Maximilian Patterson
    /// Secondary Author: None
    /// Reviewer: 
    /// </summary>
    public class HistoryItem : ObservableObject
    {
        public int historyid { get; set; }
        public int profileid { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public HistoryItem(int historyid, int profileid, string title, string description)
        {
            this.historyid = historyid;
            this.profileid = profileid;
            this.title = title;
            this.description = description;
        }
    }
}
