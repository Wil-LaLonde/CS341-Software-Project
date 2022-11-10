using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Group : ObservableObject {
        private ObservableCollection<Profile> groupProfileList;

        public string GroupName { get; set; }
        public ObservableCollection<Profile> GroupAccountList { get { return groupProfileList; } }

        /// <summary>
        /// Group constructor
        /// </summary>
        /// <param name="groupName">string groupName</param>
        public Group(string groupName) {
            GroupName = groupName;
            groupProfileList = new ObservableCollection<Profile>();
        }

        /// <summary>
        /// Adding a Profile to a Group
        /// </summary>
        /// <param name="profile">Profile profile</param>
        public void AddAccountToGroup(Profile profile) {
            groupProfileList.Add(profile);
        }

        /// <summary>
        /// Removing a Profile from a Group
        /// </summary>
        /// <param name="profile">Profile profile</param>
        public void RemoveAccountFromGroup(Profile profile) {
            groupProfileList.Remove(profile);
        }
    }
}
