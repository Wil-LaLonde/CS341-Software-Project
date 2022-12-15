using CommunityToolkit.Mvvm.ComponentModel;

namespace AcademicReward.ModelClass; 

/// <summary>
///     Model class used to represent a task
///     Primary Author: Xee Lo
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public class Task : ObservableObject {
    public const int MinTitleLength = 0;
    public const int MaxTitleLength = 50;
    public const int MinDescriptionLength = 0;
    public const int MaxDescriptionLength = 250;
    public const int MinPointValue = 0;

    private bool _approve;
    private bool _submitted;

    /// <summary>
    ///     Task constructor
    /// </summary>
    /// <param name="isChecked">bool isChecked</param>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="points">int points</param>
    public Task(bool isChecked, string title, string description, int points) {
        _approve = isChecked;
        Title = title;
        Description = description;
        Points = points;
    }

    /// <summary>
    ///     Task constructor (used when creating a new task)
    /// </summary>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="points">int points</param>
    /// <param name="groupId">int groupID</param>
    public Task(string title, string description, int points, int groupId) {
        _approve = false;
        _submitted = false;
        Title = title;
        Description = description;
        Points = points;
        GroupId = groupId;
    }

    /// <summary>
    ///     Task constructor (used when gathering a task)
    /// </summary>
    /// <param name="taskId">int taskID</param>
    /// <param name="title">string title</param>
    /// <param name="description">string description</param>
    /// <param name="points">int points</param>
    /// <param name="groupId">int groupID</param>
    /// <param name="isChecked">bool isChecked</param>
    public Task(int taskId, string title, string description, int points, int groupId, bool isChecked) {
        _approve = isChecked;
        TaskId = taskId;
        Title = title;
        Description = description;
        Points = points;
        GroupId = groupId;
    }

    /// <summary>
    ///     Task constructor (used for checking if the task should be displayed for memeber or admin)
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="title"></param>
    /// <param name="description"></param>
    /// <param name="points"></param>
    /// <param name="groupId"></param>
    /// <param name="isChecked"></param>
    /// <param name="isSubmitted"></param>
    public Task(int taskId, string title, string description, int points, int groupId, bool isChecked,
        bool isSubmitted) {
        _approve = isChecked;
        _submitted = isSubmitted;
        TaskId = taskId;
        Title = title;
        Description = description;
        Points = points;
        GroupId = groupId;
    }

    public int TaskId { get; }

    public bool IsApproved {
        get => _approve;
        set => SetProperty(ref _approve, value);
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public int GroupId { get; }

    public bool IsSubmitted {
        get => _submitted;
        set => SetProperty(ref _submitted, value);
    }
}