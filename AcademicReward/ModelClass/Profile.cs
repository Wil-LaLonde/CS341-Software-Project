using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Model class used to represent a profile
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class Profile : ObservableObject {
    public const int MinUsernameLength = 0;
    public const int MaxUsernameLength = 25;
    public const int MinPasswordLength = 0;
    public const int MaxPasswordLength = 50;
    public const int LevelUpRequirementInt = 100;
    public const double LevelUpRequirementDouble = 100.0;

    private int _points;

    /// <summary>
    ///     Profile constructor (when searching a profile)
    /// </summary>
    /// <param name="username">string username</param>
    /// <param name="password">string password</param>
    public Profile(string username, string password) {
        Username = username;
        Password = password;
    }

    /// <summary>
    ///     Profile constructor (when making a new account)
    ///     The ProfileID should be auto created by the database
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
        Xp = 0;
        Level = 1;
        Points = 0;
    }

    /// <summary>
    ///     Profile constructor (when attempting to log on)
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
    ///     Profile constructor (when updating a password)
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
    ///     Profile constructor (when showing members in a group)
    /// </summary>
    /// <param name="username">string username</param>
    /// <param name="level">int level</param>
    /// <param name="xp">int xp</param>
    public Profile(string username, int xp, int level) {
        Username = username;
        Xp = xp;
        Level = level;
    }

    /// <summary>
    ///     Profile constructor (when fully logged in)
    ///     Parameters match the DB columns
    /// </summary>
    /// <param name="profileId">int profileID</param>
    /// <param name="username">string username</param>
    /// <param name="xp">int xp</param>
    /// <param name="points">int points</param>
    /// <param name="level">int level</param>
    /// <param name="isAdmin">bool isAdmin</param>
    /// <param name="salt">string salt</param>
    /// <param name="password">string password</param>
    public Profile(int profileId, string username, int xp, int points, int level, bool isAdmin, string salt,
        string password) {
        ProfileId = profileId;
        Username = username;
        Xp = xp;
        Points = points;
        Level = level;
        IsAdmin = isAdmin;
        Salt = salt;
        Password = password;
        GroupList = new ObservableCollection<Group>();
        PurchaseItems = new ObservableCollection<ShopItem>();
        NotificationList = new ObservableCollection<Notification>();
        TaskList = new ObservableCollection<Task>();
        ProfileShop = new Shop();
    }

    public int ProfileId { get; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ReEnterPassword { get; set; }
    public string Salt { get; set; }
    public bool IsAdmin { get; }
    public int Xp { get; set; }
    public int Level { get; set; }

    public int Points {
        get => _points;
        set => SetProperty(ref _points, value);
    }

    public Shop ProfileShop { get; }

    public ObservableCollection<Group> GroupList { get; }

    public ObservableCollection<ShopItem> PurchaseItems { get; }

    public ObservableCollection<Notification> NotificationList { get; }

    public ObservableCollection<Task> TaskList { get; }

    /// <summary>
    ///     Adding a group to a Profile
    /// </summary>
    /// <param name="group">Group group</param>
    public void AddGroupToProfile(Group group) {
        GroupList.Add(group);
    }

    /// <summary>
    ///     Removing a group from a Profile
    /// </summary>
    /// <param name="group">Group group</param>
    public void RemoveGroupFromProfile(Group group) {
        GroupList.Remove(group);
    }

    /// <summary>
    ///     Add Points to a Member's profile
    /// </summary>
    /// <param name="points">int points</param>
    public void AddPointsToMember(int points) {
        if (points > 0) {
            Points += points;
            //Points also count towards XP, add XP to the profile
            AddXpToMember(points);
        }
    }

    /// <summary>
    ///     Removes Points from a Member's profile
    /// </summary>
    /// <param name="points">int points</param>
    public void RemovePointsFromMember(int points) {
        if (points > 0) Points -= points;
    }

    /// <summary>
    ///     Adds XP to a Member
    /// </summary>
    /// <param name="xp">int xp</param>
    public void AddXpToMember(int xp) {
        if (xp > 0) {
            Xp += xp;
            //Checking for a level up
            int currentLevel = Xp / LevelUpRequirementInt + 1;
            //Could be a case where we need to level up more than once
            while (currentLevel > Level) LevelUpMember();
        }
    }

    /// <summary>
    ///     Add a level to a Member
    /// </summary>
    private void LevelUpMember() {
        Level++;
    }

    /// <summary>
    ///     Adds a ShopItem to a Member's purchased items list
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    public void AddShopItemToPurchaseList(ShopItem shopItem) {
        PurchaseItems.Add(shopItem);
    }

    /// <summary>
    ///     Removes a ShopItem from a Member's purchased items list
    /// </summary>
    /// <param name="shopItem">ShopItem shopItem</param>
    public void RemoveShopItemFromPurchaseList(ShopItem shopItem) {
        PurchaseItems.Remove(shopItem);
    }

    /// <summary>
    ///     Method used to get the group name from the groupID
    /// </summary>
    /// <param name="groupId">int groupID</param>
    /// <returns>string GroupName</returns>
    public string GetGroupNameUsingGroupId(int groupId) {
        foreach (Group group in GroupList)
            if (group.GroupId == groupId)
                return group.GroupName;
        return string.Empty;
    }

    /// <summary>
    ///     Method used to get the group object using the groupID
    /// </summary>
    /// <param name="groupId">int groupID</param>
    /// <returns>Group group</returns>
    public Group GetGroupUsingGroupId(int groupId) {
        foreach (Group group in GroupList)
            if (group.GroupId == groupId)
                return group;
        return null;
    }

    /// <summary>
    ///     Method used to get current XP (90 / 100)
    /// </summary>
    /// <returns>int xp</returns>
    public int GetCurrentXpInt() {
        return Xp % LevelUpRequirementInt;
    }

    /// <summary>
    ///     Method used to get current XP (90 / 100)
    ///     For the progress bar since it requires a double
    /// </summary>
    /// <returns>double xp</returns>
    public double GetCurrentXpDouble() {
        return Xp % LevelUpRequirementDouble / LevelUpRequirementInt;
    }

    /// <summary>
    ///     Adds a Task to a Profile
    /// </summary>
    /// <param name="task">Task task</param>
    public void AddTaskToProfile(Task task) {
        TaskList.Add(task);
    }

    /// <summary>
    ///     Removes a Task from a Profile
    /// </summary>
    /// <param name="task">Task task</param>
    public void RemoveTaskFromProfile(Task task) {
        TaskList.Remove(task);
    }

    /// <summary>
    ///     Adds a Notification to a Profile
    /// </summary>
    /// <param name="notification">Notification notification</param>
    public void AddNotificationToProfile(Notification notification) {
        NotificationList.Add(notification);
    }

    /// <summary>
    ///     Removes a Notification from a Profile
    /// </summary>
    /// <param name="notification">Notification notification</param>
    public void RemoveNotificationFromProfile(Notification notification) {
        NotificationList.Remove(notification);
    }
}