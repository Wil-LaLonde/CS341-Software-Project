namespace AcademicReward.Views;

using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using AcademicReward.Resources;

public partial class LoginPage : ContentPage {
	private ILogic loginLogic;

	public LoginPage() {
		loginLogic = new LoginLogic();
		InitializeComponent();
	}

	private async void SignInButtonClicked(object sender, EventArgs e) {
		//Gathering text from entry boxes
		string username = UsernameEntry.Text ?? string.Empty;
		username = username.Trim();
		string password = PasswordEntry.Text ?? string.Empty;
		password = password.Trim();
		//Create temp profile
		Profile profile = new Profile(username, password);
		
		LogicErrorType loginType = loginLogic.LookupItem(profile);
		if(LogicErrorType.NoError == loginType) {
            //For now, true -> admin & false -> member
            MauiProgram.Profile = new Profile("TestUsername", "TestPassword", false);
            AppShell appShell = new AppShell();
            bool isAdmin = MauiProgram.Profile.IsAdmin;
            if (isAdmin) {
                appShell.SetTabBars(isAdmin);
            } else {
                appShell.SetTabBars(isAdmin);
            }
            Application.Current.MainPage = appShell;
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        } else if(LogicErrorType.EmptyUsername == loginType) {
            await DisplayAlert(DataConstants.EmptyUsernameTitle, DataConstants.EmptyUsernameMessage, DataConstants.OK);
        } else if(LogicErrorType.EmptyPassword == loginType) {
            await DisplayAlert(DataConstants.EmptyPasswordTitle, DataConstants.EmptyPasswordMessage, DataConstants.OK);
        } else {
			//some unknown error has happened here...how did we get here???
		}
	}

	private void AddAccountButtonClicked(object sender, EventArgs e) {
		//create a pop up window to allow the user to create an account.
		LoginPopUp loginPopUp = new LoginPopUp();
		this.ShowPopup(loginPopUp);
	}
}