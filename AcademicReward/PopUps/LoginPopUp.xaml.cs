using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Android.Text;
using CommunityToolkit.Maui.Views;
using System.Text;

namespace AcademicReward.PopUps;

public partial class LoginPopUp : Popup {
	private ILogic loginLogic;
	public LoginPopUp() {
		loginLogic = new LoginLogic();
		InitializeComponent();
		//Hide all the error elements
		SetErrorMessageBox(false, string.Empty);
	}

	//Back button was clicked, simply close.
	private void BackButtonClicked(object sender, EventArgs e) => Close();

	//Sign up has been clicked, do some verification of user data
	private void SignUpButtonClicked(object sender, EventArgs e) {
		//First checking if the radio buttons are filled in
		if (AdminRadioButton.IsChecked || MemberRadioButton.IsChecked) {
			//Gathering all user input
			string username = UsernameEntry.Text;
			string password = PasswordEntry.Text;
			string reEnterPassword = ReEnterPasswordEntry.Text;
			bool admin = AdminRadioButton.IsChecked ? true : false;
			//Create a new profile based on user input
			Profile newProfile = new Profile(username, password, reEnterPassword, admin);
			//Need to make some profile checks here (MORE NEEDS TO BE DONE HERE)
			LogicErrorType addProfileError = loginLogic.AddItem(newProfile);
			Close(newProfile);
		} else {
			//Show error message
			SetErrorMessageBox(true, SetErrorMessageBody(LogicErrorType.EmptyAdminMemberRadioButton));
		}
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
    /// Helper method to build out an error message based on the error type
    /// </summary>
    /// <param name="loginErrorType">LogicErrorType loginErrorType</param>
    /// <returns>string errorMessage</returns>
    private string SetErrorMessageBody(LogicErrorType loginErrorType) {
		StringBuilder errorMessageBuilder = new StringBuilder();
		switch(loginErrorType) {
			case LogicErrorType.EmptyAdminMemberRadioButton:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.EmptyAdminMemberRadioButtonMessage);
				break;
        }
		return errorMessageBuilder.ToString();
	}
}