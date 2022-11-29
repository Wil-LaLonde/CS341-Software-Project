using System.Runtime.CompilerServices;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

	private async void ShowEditProfileView(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new EditProfilePage());
	}

	private async void ShowHistoryView(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new HistoryPage());
	}

	private async void ShowPurchaseHistoryView(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new PurchaseHistoryPage());
	}

	private async void ShowGroupsView(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new GroupsPage());
	}

	private async void SignOutButtonClicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}