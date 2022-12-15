using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
///     LoginPage is the page that allows users to sign in or create a new profile
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Xee Lo / Maximilian Patterson
/// </summary>
public partial class LoginPage : ContentPage {
    private readonly ILogic _loginLogic;
    private readonly ILogic _loginGroupLogic;

    /// <summary>
    ///     LoginPage constructor
    /// </summary>
    public LoginPage() {
        _loginLogic = new LoginLogic();
        _loginGroupLogic = new LoginGroupLogic();
        InitializeComponent();
    }

    /// <summary>
    ///     Method called when a user clicks on the sign in button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void SignInButtonClicked(object sender, EventArgs e) {
        //Gathering text from entry boxes
        string username = UsernameEntry.Text ?? string.Empty;
        string password = PasswordEntry.Text ?? string.Empty;
        //Create temp profile
        Profile profile = new(username, password);

        LogicErrorType loginType = _loginLogic.LookupItem(profile);
        if (LogicErrorType.NoError == loginType) {
            //Creating new AppShell to show different tab bars
            AppShell appShell = new();
            bool isAdmin = MauiProgram.Profile.IsAdmin;
            if (isAdmin)
                appShell.SetTabBars(isAdmin);
            else
                appShell.SetTabBars(isAdmin);
            Application.Current.MainPage = appShell;
            //Need to gather all the groups for the given profile.
            LogicErrorType loginGroupType = _loginGroupLogic.LookupItem(MauiProgram.Profile);
            //There was an issue gathering all the groups, display error message.
            if (LogicErrorType.LoginGroupCollectionDbError == loginGroupType)
                await DisplayAlert(DataConstants.LoginGroupCollectionTitle, DataConstants.LoginGroupCollectionMessage,
                    DataConstants.Ok);
            //Sending user off to the home page!
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
        else if (LogicErrorType.EmptyUsername == loginType) {
            await DisplayAlert(DataConstants.EmptyUsernameTitle, DataConstants.EmptyUsernameMessage, DataConstants.Ok);
        }
        else if (LogicErrorType.EmptyPassword == loginType) {
            await DisplayAlert(DataConstants.EmptyPasswordTitle, DataConstants.EmptyPasswordMessage, DataConstants.Ok);
        }
        else if (LogicErrorType.InvalidUsernameLength == loginType) {
            await DisplayAlert(DataConstants.UsernameLengthTitle, DataConstants.UsernameLengthMessage,
                DataConstants.Ok);
        }
        else if (LogicErrorType.InvalidPasswordLength == loginType) {
            await DisplayAlert(DataConstants.PasswordLengthTitle, DataConstants.PasswordLengthMessage,
                DataConstants.Ok);
        }
        else if (LogicErrorType.UsernameNotFound == loginType) {
            await DisplayAlert(DataConstants.UsernameNotFoundTitle, DataConstants.UsernameNotFoundMessage,
                DataConstants.Ok);
        }
        else if (LogicErrorType.PasswordIncorrect == loginType) {
            await DisplayAlert(DataConstants.IncorrectPasswordTitle, DataConstants.IncorrectPasswordMessage,
                DataConstants.Ok);
        }
        else if (LogicErrorType.SignProfileInDbError == loginType) {
            await DisplayAlert(DataConstants.SignProfileInDbTitle, DataConstants.SignProfileInDbMessage,
                DataConstants.Ok);
        }
        else {
            //some unknown error has happened here...how did we get here???
            await DisplayAlert(DataConstants.SignProfileInUnkownTitle, DataConstants.SignProfileInUnknownMessage,
                DataConstants.Ok);
        }
    }

    /// <summary>
    ///     Method called when a user clicks on the add account button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void AddAccountButtonClicked(object sender, EventArgs e) {
        //create a pop up window to allow the user to create an account.
        LoginPopUp loginPopUp = new();
        Profile newProfile = await this.ShowPopupAsync(loginPopUp) as Profile;
        if (newProfile != null)
            await DisplayAlert(DataConstants.AddProfileSuccessTitle, DataConstants.AddProfileSuccessMessage,
                DataConstants.Ok);
    }

    /// <summary>
    ///     Method called when a user clicks on the show password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void PasswordShowPasswordClicked(object sender, EventArgs e) {
        ShowPassword.IsVisible = false;
        HidePassword.IsVisible = true;
        PasswordEntry.IsPassword = false;
    }

    /// <summary>
    ///     Method called when a user clicks on the hide password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void PasswordHidePasswordClicked(object sender, EventArgs e) {
        ShowPassword.IsVisible = true;
        HidePassword.IsVisible = false;
        PasswordEntry.IsPassword = true;
    }
}
