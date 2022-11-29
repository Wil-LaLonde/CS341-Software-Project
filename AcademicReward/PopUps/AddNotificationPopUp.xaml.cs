using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde 
/// Secondary Author: None
/// Reviewer: Xee Lo
/// </summary>
public partial class AddNotificationPopUp : Popup {
	public AddNotificationPopUp() {
		InitializeComponent();
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

    private void BackButtonClicked(object sender, EventArgs e) => Close();
    private void CreateNotificationButtonClicked(object sender, EventArgs e) => Close();

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
        return String.Empty;
    }
}
