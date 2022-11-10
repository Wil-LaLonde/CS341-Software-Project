using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Profile : ObservableObject {
        private ObservableCollection<Group> groupList;

        public string Username { get; set; }
        public string Password { get; set; }

        public ObservableCollection<Group> GroupList { get { return groupList; } }

        /// <summary>
        /// Profile constructor
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        public Profile(string username, string password) {
            Username = username;
            Password = password;
            groupList = new ObservableCollection<Group>();
        }

        /// <summary>
        /// Adding a group to a Profile
        /// </summary>
        /// <param name="group">Group group</param>
        public void AddGroupToProfile(Group group) {
            groupList.Add(group);
        }

        /// <summary>
        /// Removing a group from a Profile
        /// </summary>
        /// <param name="group">Group group</param>
        public void RemoveGroupFromProfile(Group group) {
            groupList.Remove(group);
        }

        /// <summary>
        /// Update a Profile's username
        /// </summary>
        /// <param name="username">string password</param>
        public void UpdateProfileUsername(string username) {
            Username = username;
        }

        /// <summary>
        /// Update a Profile's password
        /// </summary>
        /// <param name="password">string password</param>
        public void UpdateProfilePassword(string password) {
            Password = password;
        }
    }
}
