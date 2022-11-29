using CommunityToolkit.Maui.Views;
using AcademicReward.ModelClass;
using AcademicReward.Logic;
using AcademicReward.Resources;
using System.Text;

namespace AcademicReward.PopUps;

public partial class AddTaskPopUp : Popup {
	ILogic taskLogic;

	/// <summary>
	/// AddTaskPopUp constructor
	/// </summary>
	public AddTaskPopUp() {
		InitializeComponent();
		//Gather all the groups for the select list
		GroupPicker.ItemsSource = MauiProgram.Profile.GroupList;
		taskLogic = new TaskLogic();
        //Hide all the error elements
        SetErrorMessageBox(false, string.Empty);
    }

	/// <summary>
	/// Method called when a user clicks the back arrow image button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs</param>
    private void BackButtonClicked(object sender, EventArgs e) => Close();

	/// <summary>
	/// Method called when a user clicks the create task button
	/// </summary>
	/// <param name="sender">object sender</param>
	/// <param name="e">EventArgs e</param>
    private void CreateTaskButtonClicked(object sender, EventArgs e) {
		LogicErrorType logicError;
		Group selectedGroup = GroupPicker.SelectedItem as Group;
		if(selectedGroup != null) {
			logicError = LogicErrorType.NoError;
            Close(new ModelClass.Task(false, string.Empty, string.Empty, 0, string.Empty));
        } else {
			logicError = LogicErrorType.EmptyTaskGroup;
		}
		//There was some kind of error, show messages
		SetErrorMessageBox(true, SetErrorMessageBody(logicError));

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
			case LogicErrorType.EmptyTaskGroup:
				errorMessageBuilder.Append(DataConstants.SpaceDashSpace);
				errorMessageBuilder.Append(DataConstants.EmptyTaskGroupMessage);
				break;
		}
		return errorMessageBuilder.ToString();
	}
}
