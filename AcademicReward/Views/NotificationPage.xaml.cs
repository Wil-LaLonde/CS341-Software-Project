using AcademicReward.Logic;

namespace AcademicReward.Views;

public partial class NotificationPage : ContentPage {
    ILogic notificationLogic;

	public NotificationPage() {
		InitializeComponent();
        notificationLogic = new NotificationLogic();
    }
}