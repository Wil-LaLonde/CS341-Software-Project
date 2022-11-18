using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AcademicReward.ModelClass {
    public class Profile : ObservableObject {
        private ObservableCollection<Group> groupList;
        private ObservableCollection<ShopItem> purchaseItems;

        public int ProfileID { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ReEnterPassword { get; set; }
        public string Salt { get; private set; }
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
        public Profile(string username, string password, string reEnterPassword, bool isAdmin) {
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            XP = 0;
            Level = 1;
            Points = 0;
        }

        /// <summary>
        /// Profile constructor (exisiting account)
        /// </summary>
        /// <param name="profileID">int profileID</param>
        /// <param name="username">string username</param>
        /// <param name="password">string password</param>
        /// <param name="isAdmin">bool isAdmin</param>
        /// <param name="xp">int xp</param>
        /// <param name="level">int level</param>
        /// <param name="points">int points</param>
        public Profile(int profileID, string username, string password, bool isAdmin, int xp, int level, int points) {
            ProfileID = profileID;
            Username = username;
            Password = password;
            IsAdmin = isAdmin;
            XP = xp;
            Level = level;
            Points = points;
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
    }
}
