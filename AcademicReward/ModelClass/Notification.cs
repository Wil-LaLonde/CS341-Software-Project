using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass {
    public class Notification : ObservableObject {

        public string Title { get; set; }
        public string Description { get; set; }
        public Group Group { get; set; }

        /// <summary>
        /// Notification constructor
        /// </summary>
        /// <param name="title">string title</param>
        /// <param name="description">string description</param>
        /// <param name="group">Group group</param>
        public Notification(string title, string description, Group group) {
            Title = title;
            Description = description;
            Group = group;
        }
    }
}
