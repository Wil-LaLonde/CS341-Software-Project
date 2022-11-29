using AcademicReward.Logic;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Maximilian Patterson
/// </summary>
public partial class NotificationPage : ContentPage {
    ILogic notificationLogic;

	public NotificationPage() {
		InitializeComponent();
        notificationLogic = new NotificationLogic();
    }
}