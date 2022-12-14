using AcademicReward.Database;
using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Text;

namespace AcademicReward.PopUps;

/// <summary>
/// PopUp for when adding a member to a group
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Maximilian Patterson
/// </summary>
public partial class AddMemberPopUp : Popup {
	
	public Group group;
	// Reference to groupage so that the listview can be updated
	public ObservableCollection<Profile> memberList;

	/// <summary>
	/// AddMemberPopUp constructor
	/// </summary>
	/// <param name="group">Group group</param>
	/// <param name="memberList">ObservableCollection memberList</param>
	public AddMemberPopUp(Group group, ref ObservableCollection<Profile> memberList) {
		InitializeComponent();
		this.group = group;
		this.memberList = memberList;
		SetErrorMessageBox(false, string.Empty);
    }

	/// <summary>
	/// Method called when a user clicks the back button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

	/// <summary>
	/// Method called when a user clicks the add button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
	private void AddMemberButtonClicked(object sender, EventArgs e) {
		LogicErrorType logicError;
		// Grab the text from the entry box
		String memberName = MemberNameEntry.Text;
		if(IsMemberNotInGroup(memberName)) {
            // Add member logic
            var addMemberLogic = new AddMemberLogic();
            // Create an array with two elements, the current group and the member name
            object[] obj = new object[] { memberName, group };
			logicError = addMemberLogic.AddItemWithArgs(obj);
			if(LogicErrorType.NoError == logicError) {
                // Update the listview
                memberList = GroupProfileRelationship.getProfilesInGroup(group);
                Close(memberList);
            }
        } else {
			logicError = LogicErrorType.MemberAlreadyInGroup;
		}
		//Something went wrong, show error message box
		SetErrorMessageBox(true, SetErrorMessageBody(logicError));
	}

	/// <summary>
	/// Helper method used to check if a given username is already in the group
	/// </summary>
	/// <param name="username">string username</param>
	/// <returns>true/false</returns>
	private bool IsMemberNotInGroup(string username) {
		foreach(Profile member in memberList) {
			if(member.Username.Equals(username)) {
				return false;
			}
		}
		return true;
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
		switch (logicError) {
			case LogicErrorType.MemberAlreadyInGroup:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.AddMemberAlreadyInGroupMessage);
				break;
			case LogicErrorType.EmptyUsername:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.EmptyMemberNameMessage);
                break;
			case LogicErrorType.InvalidUsernameLength:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.InvalidMemberUsernameLengthMessage);
                break;
			case LogicErrorType.GroupAlreadyHasAdmin:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.GroupAlreadyHasAdminMessage);
                break;
			case LogicErrorType.GroupCreateError:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddMemberUserNameNotFoundMessage);
                break;
			default:
                errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
                errorMessageBuilder.Append(DataConstants.AddMemberUnknownErrorMessage);
                break;
        }
		return errorMessageBuilder.ToString();
    }
}
