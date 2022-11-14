using AcademicReward.Views;

namespace AcademicReward;

public partial class AppShell : Shell {
	public AppShell() {
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
	}
}
