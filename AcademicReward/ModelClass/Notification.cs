using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Model class used to represent a Notification
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class Notification : ObservableObject {
    public const int MinTitleLength = 0;
    public const int MaxTitleLength = 50;
    public const int MinDescriptionLength = 0;
    public const int MaxDescriptionLength = 250;

    /// <summary>
    ///     Notification constructor (when making a new Notification)
    /// </summary>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="groupId">int groupID</param>
    public Notification(string title, string description, int groupId) {
        Title = title;
        Description = description;
        GroupId = groupId;
    }

    /// <summary>
    ///     Notification constructor (when getting exisiting Notifications)
    /// </summary>
    /// <param name="notificationId">int notificationID</param>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="groupId">int groupID</param>
    public Notification(int notificationId, string title, string description, int groupId) {
        NotificationId = notificationId;
        Title = title;
        Description = description;
        GroupId = groupId;
    }

    public int NotificationId { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int GroupId { get; set; }
}