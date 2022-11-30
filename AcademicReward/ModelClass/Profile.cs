using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    /// <summary>
    /// Primary Author: Wil LaLonde
    /// Secondary Author: None
    /// Reviewer: Maximilian Patterson
    /// </summary>
    public class Profile : ObservableObject {
        public const int MinUsernameLength = 0;
        public const int MaxUsernameLength = 25;
        public const int MinPasswordLength = 0;
        public const int MaxPasswordLength = 50;

        private ObservableCollection<Group> groupList;
        private ObservableCollection<ShopItem> purchaseItems;

        public int ProfileID { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ReEnterPassword { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; private set; }
        public int XP { get; set; }
        public int Level { get; set; }
        public int Points { get; set; }

        public ObservableCollection<Group> GroupList { get { return groupList; } }
        public ObservableCollection<ShopItem> PurchaseItems { get { return purchaseItems; } }

        /// <summary>
        /// Profile constructor (when searching a profile)
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        public Profile(string username, string password) {
            Username = username;
            Password = password;
        }

        /// <summary>
        /// Profile constructor (when making a new account)
        /// The ProfileID should be auto created by the database
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        /// <param name="isAdmin">bool isAdmin</param>
        public Profile(string username, string password, string reEnterPassword, string salt, bool isAdmin) {
            Username = username;
            Password = password;
            ReEnterPassword = reEnterPassword;
            Salt = salt;
            IsAdmin = isAdmin;
            XP = 0;
            Level = 1;
            Points = 0;
        }

        /// <summary>
        /// Profile constructor (when attempting to log on)
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="salt">string salt</param>
        /// <param name="password">string password</param>
        public Profile(string username, string salt, string password) {
            Username = username;
            Salt = salt;
            Password = password;
        }

        /// <summary>
        /// Profile constructor (when updating a password)
        /// </summary>
        /// <param name="username">string username</param>
        /// <param name="oldPassword">string oldPassword</param>
        /// <param name="newPassword">string newPassword</param>
        /// <param name="reEnterNewPassword">string reEnterPassword</param>
        public Profile(string username, string oldPassword, string newPassword, string reEnterNewPassword) {
            Username = username;
            Password = oldPassword;
            NewPassword = newPassword;
            ReEnterPassword = reEnterNewPassword;
        }

        /// <summary>
        /// Profile constructor (when fully logged in)
        /// Parameters match the DB columns
        /// </summary>
        /// <param name="profileID">int profileID</param>
        /// <param name="username">string username</param>
        /// <param name="xp">int xp</param>
        /// <param name="points">int points</param>
        /// <param name="level">int level</param>
        /// <param name="isAdmin">bool isAdmin</param>
        /// <param name="salt">string salt</param>
        /// <param name="password">string password</param>
        public Profile(int profileID, string username, int xp, int points, int level, bool isAdmin, string salt, string password) {
            ProfileID = profileID;
            Username = username;
            XP = xp;
            Points = points;
            Level = level;
            IsAdmin = isAdmin;
            Salt = salt;
            Password = password;
            groupList = new ObservableCollection<Group>();
            purchaseItems = new ObservableCollection<ShopItem>();
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

        /// <summary>
        /// Adds XP to a Member
        /// </summary>
        /// <param name="xp">int xp</param>
        public void AddXPToMember(int xp) {
            //Maybe add some logic here for different levels?
            //This could call the LevelUpMember method to do so.
            if (xp > 0) {
                XP += xp;
                LevelUpMember();
            }
        }

        /// <summary>
        /// Add a level to a Member
        /// </summary>
        private void LevelUpMember() { Level++; }

        /// <summary>
        /// Adds a ShopItem to a Member's purchased items list
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void AddShopItemToPurchaseList(ShopItem shopItem) {
            purchaseItems.Add(shopItem);
        }

        /// <summary>
        /// Removes a ShopItem from a Member's purchased items list
        /// </summary>
        /// <param name="shopItem">ShopItem shopItem</param>
        public void RemoveShopItemFromPurchaseList(ShopItem shopItem) { 
                purchaseItems.Remove(shopItem);
        }

        /// <summary>
        /// Add Points to a Member's profile
        /// </summary>
        /// <param name="points">int profile</param>
        public void AddPointsToMember(int points) {
            Points += points;
        }

        /// <summary>
        /// Method used to get the group name from the groupID
        /// </summary>
        /// <param name="groupID">int groupID</param>
        /// <returns>string GroupName</returns>
        public string GetGroupNameUsingGroupID(int groupID) {
            foreach(Group group in groupList) {
                if(group.GroupID == groupID) {
                    return group.GroupName;
                }
            }
            return string.Empty;
        }
    }
}
