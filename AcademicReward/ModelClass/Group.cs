using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Group : ObservableObject {
        private ObservableCollection<Member> groupMemberList;
        private ObservableCollection<Task> groupTaskList;
        private ObservableCollection<Notification> groupNotificationList;

        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public Admin GroupAdmin { get; private set; }
        public ObservableCollection<Member> GroupMemberList { get { return groupMemberList; } }
        public ObservableCollection<Task> GroupTaskList { get { return groupTaskList; } }
        public ObservableCollection<Notification> GroupNotificationList { get { return groupNotificationList; } }

        /// <summary>
        /// Group constructor
        /// </summary>
        /// <param name="groupName">string groupName</param>
        /// <param name="groupDescription">string groupDescription</param>
        /// <param name="groupAdmin">Admin groupAdmin</param>
        public Group(string groupName, string groupDescription, Admin groupAdmin) {
            GroupName = groupName;
            GroupDescription = groupDescription;
            GroupAdmin = groupAdmin;
            groupMemberList = new ObservableCollection<Member>();
            groupTaskList = new ObservableCollection<Task>();
            groupNotificationList = new ObservableCollection<Notification>();
        }

        /// <summary>
        /// Adds a Member to a Group
        /// </summary>
        /// <param name="member">Member member</param>
        public void AddMemberToGroup(Member member) {
            groupMemberList.Add(member);
        }

        /// <summary>
        /// Removes a Member from a Group
        /// </summary>
        /// <param name="member">Member member</param>
        public void RemoveMemberFromGroup(Member member) {
            groupMemberList.Remove(member);
        }

        /// <summary>
        /// Updates the Group Admin
        /// </summary>
        /// <param name="admin">Admin admin</param>
        public void UpdateGroupAdmin(Admin admin) {
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
