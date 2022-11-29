using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using System.Text;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Xee Lo / Maximilian Patterson
/// </summary>
public partial class LoginPopUp : Popup {
	private ILogic loginLogic;

	public LoginPopUp() {
		loginLogic = new LoginLogic();
		InitializeComponent();
		//Hide all the error elements
		SetErrorMessageBox(false, string.Empty);
	}

	/// <summary>
	/// Method to simply close the popup
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void BackButtonClicked(object sender, EventArgs e) => Close();

	/// <summary>
	/// Method called when a user clicks on the sign up button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void SignUpButtonClicked(object sender, EventArgs e) {
		LogicErrorType addProfileError;
		//First checking if the radio buttons are filled in
		if (AdminRadioButton.IsChecked || MemberRadioButton.IsChecked) {
			//Gathering all user input
			string username = UsernameEntry.Text;
			string password = PasswordEntry.Text;
			string reEnterPassword = ReEnterPasswordEntry.Text;
			bool admin = AdminRadioButton.IsChecked ? true : false;
			//Create a new profile based on user input
			Profile newProfile = new Profile(username, password, reEnterPassword, string.Empty, admin); ;
			//Need to make some profile checks here
			addProfileError = loginLogic.AddItem(newProfile);
			//If all is good, the account was created successfully!
			if(LogicErrorType.NoError == addProfileError) {
				Close(newProfile);
			} 
		} else {
			addProfileError = LogicErrorType.EmptyAdminMemberRadioButton;
		}
		//There was an error, show error message box.
        SetErrorMessageBox(true, SetErrorMessageBody(addProfileError));
    }

	/// <summary>
	/// Method called when a user clicks on the show password image button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void PasswordShowPasswordClicked(object sender, EventArgs e) {
        ShowPassword.IsVisible = false;
        HidePassword.IsVisible = true;
        PasswordEntry.IsPassword = false;
    }

	/// <summary>
	/// Method called when a user clicks on the hide password image button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void PasswordHidePasswordClicked(object sender, EventArgs e) {
        ShowPassword.IsVisible = true;
        HidePassword.IsVisible = false;
        PasswordEntry.IsPassword = true;
    }

	/// <summary>
	/// Method called when a user clicks on the show re-enter password image button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void ReEnterPasswordShowPasswordClicked(object sender, EventArgs e) {
        ShowReEnterPassword.IsVisible = false;
        HideReEnterPassword.IsVisible = true;
        ReEnterPasswordEntry.IsPassword = false;
    }

	/// <summary>
	/// Method called when a user clicks on the show re-enter password image button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void ReEnterPasswordHidePasswordClicked(object sender, EventArgs e) {
        ShowReEnterPassword.IsVisible = true;
        HideReEnterPassword.IsVisible = false;
        ReEnterPasswordEntry.IsPassword = true;
    }

    /// <summary>
    /// Helper method used to either show or hide the error message box
    /// This is done since a popup cannot have another popup
    /// </summary>
    /// <param name="isVisible">bool isVisible</param>
    /// <param name="errorMessage">string errorMessage</param>
    private void SetErrorMessageBox(bool isVisible, string errorMessage) {
		ErrorFrame.IsVisible = isVisible;
		ErrorStackLayout.IsVisible = isVisible;
		ErrorMessageHeader.IsVisible = isVisible;
		ErrorMessageBody.IsVisible = isVisible;
		ErrorMessageBody.Text = errorMessage;
    }

	/// <summary>
	/// Helper method used to determine what error message to display.
	/// </summary>
	/// <param name="logicError">LogicErrorType logicError</param>
	/// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType logicError) {
		StringBuilder errorMessageBuilder = new StringBuilder();
		switch(logicError) {
			case LogicErrorType.EmptyUsername:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyUsernameMessage);
				break;
			case LogicErrorType.EmptyPassword:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.EmptyPasswordMessage);
				break;
			case LogicErrorType.EmptyReEnterPassword:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.EmptyReEnterPasswordMessage);
				break;
			case LogicErrorType.PasswordMismatch:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.PasswordMismatchMessage);
				break;
			case LogicErrorType.InvalidUsernameLength:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.UsernameLengthMessage);
				break;
			case LogicErrorType.InvalidPasswordLength:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.PasswordLengthMessage);
				break;
			case LogicErrorType.EmptyAdminMemberRadioButton:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.EmptyAdminMemberRadioButtonMessage);
				break;
			case LogicErrorType.UsernameTaken:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.UsernameTakenMessage);
				break;
			case LogicErrorType.AddProfileDBError:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.AddProfileDBErrorMessage);
				break;
			default:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.AddProfileUnknownMesage);
				break;
        }
		return errorMessageBuilder.ToString();
	}
}