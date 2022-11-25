using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Group : ObservableObject {
        private ObservableCollection<Profile> groupMemberList;
        private ObservableCollection<Task> groupTaskList;
        private ObservableCollection<Notification> groupNotificationList;

        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public Profile GroupAdmin { get; private set; }
        public ObservableCollection<Profile> GroupMemberList { get { return groupMemberList; } }
        public ObservableCollection<Task> GroupTaskList { get { return groupTaskList; } }
        public ObservableCollection<Notification> GroupNotificationList { get { return groupNotificationList; } }

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
            groupTaskList = new ObservableCollection<Task>();
            groupNotificationList = new ObservableCollection<Notification>();
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
        /// Adds a Task to a Group
        /// </summary>
        /// <param name="task">Task task</param>
        public void AddTaskToGroup(Task task) {
            groupTaskList.Add(task);
        }

        /// <summary>
        /// Removes a Task from a Group
        /// </summary>
        /// <param name="task">Task task</param>
        public void RemoveTaskFromGroup(Task task) {
            groupTaskList.Remove(task);
        }

        /// <summary>
        /// Adds a Notification to a Group
        /// </summary>
        /// <param name="notification">Notification notification</param>
        public void AddNotificationToGroup(Notification notification) {
            groupNotificationList.Add(notification);
        }

        /// <summary>
        /// Removes a Notification from a Group
        /// </summary>
        /// <param name="notification">Notification notification</param>
        public void RemoveNotificationFromGroup(Notification notification) {
            groupNotificationList.Remove(notification);
        }
    }
}
