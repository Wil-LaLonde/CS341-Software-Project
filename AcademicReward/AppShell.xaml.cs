using AcademicReward.Views;

namespace AcademicReward;

/// <summary>
/// Primary Author: Generated File
/// Secondary Author: Wil LaLonde
/// Reviewer: Xee Lo / Maximilian Patterson
/// </summary>
public partial class AppShell : Shell {

	public AppShell() {
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
	}

	/// <summary>
	/// Helper method used to change the tab bar depending on the user type
	/// </summary>
	/// <param name="isAdmin">bool isAdmin</param>
	public void SetTabBars(bool isAdmin) {
		if(isAdmin) {
			NotificationPage.IsVisible = false;
		} else {
			TaskPage.IsVisible = false;
		}
	}
}
