namespace AcademicReward.Views;

using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using AcademicReward.ModelClass;

public partial class LoginPage : ContentPage {
	public LoginPage() {
		InitializeComponent();
	}

	private async void SignInButtonClicked(object sender, EventArgs e) {
		//Gathering text from entry boxes
		string username = UsernameEntry.Text ?? string.Empty;
		username.Trim();
		string password = PasswordEntry.Text ?? string.Empty;
		password.Trim();
		if(string.IsNullOrEmpty(username)) {
			//Most likely make these into constants somewhere
			await DisplayAlert("Empty Username", "Please fill in the username box", "OK");
		} else if(string.IsNullOrEmpty(password)) {
            await DisplayAlert("Empty Password", "Please fill in the password box", "OK");
        } else {
			//For now, true -> admin & false -> member
            MauiProgram.Profile = new Profile("TestUsername", "TestPassword", true);
            AppShell appShell = new AppShell();
            bool isAdmin = MauiProgram.Profile.IsAdmin;
            if (isAdmin) {
                appShell.SetTabBars(isAdmin);
            }
            else {
                appShell.SetTabBars(isAdmin);
            }
            Application.Current.MainPage = appShell;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
	}

	private void AddAccountButtonClicked(object sender, EventArgs e) {
		//create a pop up window to allow the user to create an account.
		LoginPopUp loginPopUp = new LoginPopUp();
		this.ShowPopup(loginPopUp);
	}
}