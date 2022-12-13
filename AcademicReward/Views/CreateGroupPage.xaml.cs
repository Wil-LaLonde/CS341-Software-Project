using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Views;

/// <summary>
/// CreateGroupPage is the page that is shown for creating a new group
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class CreateGroupPage : ContentPage {
    // Group logic
    private GroupLogic groupLogic;

    /// <summary>
    /// CreateGroupPage constructor
    /// </summary>
    public CreateGroupPage() {
        // Instantiate groupLogic
        groupLogic = new GroupLogic();
        InitializeComponent();
    }

    /// <summary>
    /// Method called when a user clicks on the create group button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void CreateGroupButtonClicked(object sender, EventArgs e) {
        // Grab the text from the entry boxes
        var groupName = GroupNameEntry.Text;
        var groupDescription = GroupDescriptionEntry.Text;

        // Error checking for empty name
        if (groupName == null || groupName == "") {
            // Set error label text
            ErrorLabel.Text = "Please enter a group name";
            return;
        }

        // Error checking for empty description
        if (groupDescription == null || groupDescription == "") {
            // Set error label text
            ErrorLabel.Text = "Please enter a group description";
            return;
        }

        // Create group object from information supplied by admin user
        var group = new Group
        (
            groupName,
            groupDescription,
            MauiProgram.Profile // Admin profile
         );

        var error = groupLogic.AddItem(group);
        if (error != LogicErrorType.NoError) {
            ErrorLabel.Text = "Error adding group";
            return;
        }
        
        // Navigate back to the group page
        Navigation.PopAsync();
    }
}
