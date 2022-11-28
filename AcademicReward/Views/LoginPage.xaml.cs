namespace AcademicReward.Views;

using CommunityToolkit.Maui.Views;
using AcademicReward.PopUps;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using AcademicReward.Resources;

public partial class LoginPage : ContentPage {
	private ILogic loginLogic, loginGroupLogic;

	public LoginPage() {
		loginLogic = new LoginLogic();
        loginGroupLogic = new LoginGroupLogic();
		InitializeComponent();
	}

	private async void SignInButtonClicked(object sender, EventArgs e) {
		//Gathering text from entry boxes
		string username = UsernameEntry.Text ?? string.Empty;
		string password = PasswordEntry.Text ?? string.Empty;
		//Create temp profile
		Profile profile = new Profile(username, password);
		
		LogicErrorType loginType = loginLogic.LookupItem(profile);
		if(LogicErrorType.NoError == loginType) {
			//Creating new AppShell to show different tab bars
            AppShell appShell = new AppShell();
            bool isAdmin = MauiProgram.Profile.IsAdmin;
            if (isAdmin) {
                appShell.SetTabBars(isAdmin);
            } else {
                appShell.SetTabBars(isAdmin);
            }
            Application.Current.MainPage = appShell;
            //Need to gather all the groups for the given profile.
            LogicErrorType loginGroupType = loginGroupLogic.LookupItem(MauiProgram.Profile);
            //There was an issue gathering all the groups, display error message.
            if(LogicErrorType.LoginGroupCollectionDBError == loginGroupType) {
                await DisplayAlert(DataConstants.LoginGroupCollectionTitle, DataConstants.LoginGroupCollectionMessage, DataConstants.OK);
            }
            //Sending user off to the home page!
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        } else if(LogicErrorType.EmptyUsername == loginType) {
            await DisplayAlert(DataConstants.EmptyUsernameTitle, DataConstants.EmptyUsernameMessage, DataConstants.OK);
        } else if(LogicErrorType.EmptyPassword == loginType) {
            await DisplayAlert(DataConstants.EmptyPasswordTitle, DataConstants.EmptyPasswordMessage, DataConstants.OK);
        } else if(LogicErrorType.InvalidUsernameLength == loginType) {
            await DisplayAlert(DataConstants.UsernameLengthTitle, DataConstants.UsernameLengthMessage, DataConstants.OK);
        } else if(LogicErrorType.InvalidPasswordLength == loginType) {
            await DisplayAlert(DataConstants.PasswordLengthTitle, DataConstants.PasswordLengthMessage, DataConstants.OK);
        } else if(LogicErrorType.UsernameNotFound == loginType) {
            await DisplayAlert(DataConstants.UsernameNotFoundTitle, DataConstants.UsernameNotFoundMessage, DataConstants.OK);
        } else if(LogicErrorType.PasswordIncorrect == loginType) {
            await DisplayAlert(DataConstants.IncorrectPasswordTitle, DataConstants.IncorrectPasswordMessage, DataConstants.OK);
        } else if(LogicErrorType.SignProfileInDBError == loginType) {
            await DisplayAlert(DataConstants.SignProfileInDBTitle, DataConstants.SignProfileInDBMessage, DataConstants.OK);
        } else {
            //some unknown error has happened here...how did we get here???
            await DisplayAlert(DataConstants.SignProfileInUnkownTitle, DataConstants.SignProfileInUnknownMessage, DataConstants.OK);
        }
    }

	private async void AddAccountButtonClicked(object sender, EventArgs e) {
		//create a pop up window to allow the user to create an account.
		LoginPopUp loginPopUp = new LoginPopUp();
		Profile newProfile = await this.ShowPopupAsync(loginPopUp) as Profile;
		if(newProfile != null) {
			await DisplayAlert(DataConstants.AddProfileSuccessTitle, DataConstants.AddProfileSuccessMessage, DataConstants.OK);
		} 
    }
}