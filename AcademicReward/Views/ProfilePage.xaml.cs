using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Views;

/// <summary>
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class ProfilePage : ContentPage {
	/// <summary>
	///     ProfilePage constructor
	/// </summary>
	public ProfilePage() {
        InitializeComponent();
        BindingContext = MauiProgram.Profile;
        Points.SetBinding(Label.TextProperty, nameof(MauiProgram.Profile.Points));
        UsernameDisplay(MauiProgram.Profile.IsAdmin);
        // If user is an admin, hide PurchaseHistoryBtn
        if (MauiProgram.Profile.IsAdmin) PurchaseHistoryBtn.IsVisible = false;
    }

	/// <summary>
	///     Method called when a user clicks on the edit profile button
	/// </summary>
	/// <param name="sender">objet sender</param>
	/// <param name="e">EventArgs e</param>
	private async void ShowEditProfileView(object sender, EventArgs e) {
        await Navigation.PushAsync(new EditProfilePage());
    }

	/// <summary>
	///     Method called when a user clicks on the show history button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private async void ShowHistoryView(object sender, EventArgs e) {
        await Navigation.PushAsync(new HistoryPage());
    }

	/// <summary>
	///     Method called when a user clicks on the purchase history button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private async void ShowPurchaseHistoryView(object sender, EventArgs e) {
        await Navigation.PushAsync(new PurchaseHistoryPage());
    }

	/// <summary>
	///     Method called when a user clicks on the groups button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private async void ShowGroupsView(object sender, EventArgs e) {
        await Navigation.PushAsync(new GroupsPage());
    }

	/// <summary>
	///     Method called when a user clicks on the sign out button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private async void SignOutButtonClicked(object sender, EventArgs e) {
        //Resetting the current profile to do an actual "logout"
        MauiProgram.Profile = null;
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

	/// <summary>
	///     Method is called when opening the Profile page to show profile values
	/// </summary>
	/// <param name="isAdmin">bool isAdmin</param>
	private void UsernameDisplay(bool isAdmin) {
        //Bind user name to signed in profile
        Username.Text = MauiProgram.Profile.Username;
        if (isAdmin) {
            PointsLabel.IsVisible = false;
            Points.IsVisible = false;
            LevelLabel.IsVisible = false;
            Level.IsVisible = false;
            ProgressBar.IsVisible = false;
            ExpLabel.IsVisible = false;
            Exp.IsVisible = false;
        }
        else {
            Level.Text = MauiProgram.Profile.Level.ToString();
            ProgressBar.Progress = MauiProgram.Profile.GetCurrentXpDouble();
            Exp.Text = MauiProgram.Profile.GetCurrentXpInt() + DataConstants.SpaceSlashSpace +
                Profile.LevelUpRequirementInt;
        }
    }
}
