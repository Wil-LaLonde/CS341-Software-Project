using AcademicReward.Logic;
using AcademicReward.Resources;
using AcademicReward.ModelClass;
using AcademicReward.Database;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class EditProfilePage : ContentPage {
    ILogic loginLogic;
    IDatabase historyDB;

    /// <summary>
    /// EditProfilePage constructor
    /// </summary>
	public EditProfilePage() {
		InitializeComponent();
        loginLogic = new LoginLogic();
        historyDB = new HistoryDatabase();
	}

    /// <summary>
    /// Method called when the user clicks the save button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private async void SaveButtonClicked(object sender, EventArgs e) {
        LogicErrorType logicError;
        //Gathering user input
        string oldPassword = OldPasswordEntry.Text ?? string.Empty;
        string newPassword = NewPasswordEntry.Text ?? string.Empty;
        string reEnterNewPassword = ReEnterNewPasswordEntry.Text ?? string.Empty;
        //Creating temp profile object
        Profile profile = new Profile(MauiProgram.Profile.Username, oldPassword, newPassword, reEnterNewPassword);
        //Trying to update the password
        logicError = loginLogic.UpdateItem(profile);
        if(LogicErrorType.NoError == logicError) {
            //Adding a history item for updating a user's password
            historyDB.AddItem(new HistoryItem(MauiProgram.Profile.ProfileID, DataConstants.HistoryEditPasswordTitle, string.Format(DataConstants.HistoryEditPasswordDescription, DateTime.Now.ToString())));
            await DisplayAlert(DataConstants.UpdatePasswordSuccessTitle, DataConstants.UpdatePasswordSuccessMessage, DataConstants.OK);
            //Sending the user back to the profile page, see ya!
            await Shell.Current.GoToAsync(DataConstants.GoBack);
        } else if(LogicErrorType.EmptyOldPassword == logicError) {
            await DisplayAlert(DataConstants.EmptyOldPasswordTitle, DataConstants.EmptyOldPasswordMessage, DataConstants.OK);
        } else if(LogicErrorType.EmptyNewPassword == logicError) {
            await DisplayAlert(DataConstants.EmptyNewPasswordTitle, DataConstants.EmptyNewPasswordMessage, DataConstants.OK);
        } else if(LogicErrorType.EmptyReEnterNewPassword == logicError) {
            await DisplayAlert(DataConstants.EmptyReEnterNewPasswordTitle, DataConstants.EmptyReEnterNewPasswordMessage, DataConstants.OK);
        } else if(LogicErrorType.PasswordMismatch == logicError) {
            await DisplayAlert(DataConstants.PasswordMismatchTitle, DataConstants.PasswordMismatchMessage, DataConstants.OK);
        } else if(LogicErrorType.InvalidOldPasswordLength == logicError) {
            await DisplayAlert(DataConstants.InvalidOldPasswordLengthTitle, DataConstants.InvalidOldPasswordLengthMessage, DataConstants.OK);
        } else if(LogicErrorType.InvalidNewPasswordLength == logicError) {
            await DisplayAlert(DataConstants.InvalidNewPasswordLengthTitle, DataConstants.InvalidNewPasswordLengthMessage, DataConstants.OK);
        } else if(LogicErrorType.InvalidReEnterNewPasswordLength == logicError) {
            await DisplayAlert(DataConstants.InvalidReEnterNewPasswordLengthTitle, DataConstants.InvalidReEnterNewPasswordLengthMessage, DataConstants.OK);
        } else if(LogicErrorType.PasswordIncorrect == logicError) {
            await DisplayAlert(DataConstants.IncorrectPasswordTitle, DataConstants.OldPasswordIncorrectMessage, DataConstants.OK);
        } else if(LogicErrorType.CurrentPasswordError == logicError) {
            await DisplayAlert(DataConstants.CurrentPasswordErrorTitle, DataConstants.CurrentPasswordErrorMessage, DataConstants.OK);
        } else if(LogicErrorType.UpdatePasswordDBError == logicError) {
            await DisplayAlert(DataConstants.UpdatePasswordDBErrorTitle, DataConstants.UpdatePasswordDBErrorMessage, DataConstants.OK);
        } else {
            //Something odd happened, how did we get here???
            await DisplayAlert(DataConstants.UpdatePasswordUnknownTitle, DataConstants.UpdatePasswordUnknownMessage, DataConstants.OK);
        }
    }

    /// <summary>
    /// Method called when a user clicks on the show old password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void OldPasswordShowClicked(object sender, EventArgs e) {
        ShowOldPassword.IsVisible = false;
        HideOldPassword.IsVisible = true;
        OldPasswordEntry.IsPassword = false;
    }

    /// <summary>
    /// Method called when a user clicks on the hide old password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void OldPasswordHideClicked(object sender, EventArgs e) {
        ShowOldPassword.IsVisible = true;
        HideOldPassword.IsVisible = false;
        OldPasswordEntry.IsPassword = true;
    }

    /// <summary>
    /// Method called when a user clicks on the show new password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void NewPasswordShowClicked(object sender, EventArgs e) {
        ShowNewPassword.IsVisible = false;
        HideNewPassowrd.IsVisible = true;
        NewPasswordEntry.IsPassword = false;
    }

    /// <summary>
    /// Method called when a user clicks on the hide new password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void NewPasswordHideClicked(object sender, EventArgs e) {
        ShowNewPassword.IsVisible = true;
        HideNewPassowrd.IsVisible = false;
        NewPasswordEntry.IsPassword = true;
    }

    /// <summary>
    /// Method called when a user clicks on the show new password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void ReEnterPasswordShowClicked(object sender, EventArgs e) {
        ShowReEnterPassword.IsVisible = false;
        HideReEnterPassword.IsVisible = true;
        ReEnterNewPasswordEntry.IsPassword = false;
    }

    /// <summary>
    /// Method called when a user clicks on the hide new password image button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void ReEnterPasswordHideClicked(object sender, EventArgs e) {
        ShowReEnterPassword.IsVisible = true;
        HideReEnterPassword.IsVisible = false;
        ReEnterNewPasswordEntry.IsPassword = true;
    }
}