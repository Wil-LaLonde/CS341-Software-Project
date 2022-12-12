using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class Group : ObservableObject {
        private ObservableCollection<Profile> groupMemberList;

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public Profile GroupAdmin { get; private set; }
        public int AdminProfileID { get; set; }
        public ObservableCollection<Profile> GroupMemberList { get { return groupMemberList; } }

        /// <summary>
        /// Group constructor
        /// </summary>
        /// <param name="groupName">string groupName</param>
        /// <param name="groupDescription">string groupDescription</param>
        /// <param name="groupAdmin">Profile groupAdmin</param>
        public Group(string groupName, string groupDescription, Profile groupAdmin) {
            GroupName = groupName;
            GroupDescription = groupDescription;
            //This needs to be an admin
            GroupAdmin = groupAdmin;
            groupMemberList = new ObservableCollection<Profile>();
        }

        /// <summary>
        /// Group constructor (when gathering all groups for a profile upon login)
        /// </summary>
        /// <param name="groupID">int groupID</param>
        /// <param name="groupName">string groupName</param>
        /// <param name="groupDescription">string groupDescription</param>
        /// <param name="adminProfileID">int adminProfileID</param>
        public Group(int groupID, string groupName, string groupDescription, int adminProfileID) {
            GroupID = groupID;
            GroupName = groupName;
            GroupDescription = groupDescription;
            AdminProfileID = adminProfileID;
            groupMemberList = new ObservableCollection<Profile>();
        }

        /// <summary>
        /// Adds a Member to a Group
        /// </summary>
        /// <param name="member">Profile member</param>
        public void AddMemberToGroup(Profile member) {
            groupMemberList.Add(member);
        }

        /// <summary>
        /// Removes a Member from a Group
        /// </summary>
        /// <param name="member">Profile member</param>
        public void RemoveMemberFromGroup(Profile member) {
            groupMemberList.Remove(member);
        }

        /// <summary>
        /// Updates the Group Admin
        /// </summary>
        /// <param name="admin">Profile admin</param>
        public void UpdateGroupAdmin(Profile admin) {
            GroupAdmin = admin;
        }

        /// <summary>
        /// ToString method to display the GroupName
        /// </summary>
        /// <returns>Group name</returns>
        public override string ToString() {
            return GroupName;
        }
    }
}
