using AcademicReward.Logic;
using AcademicReward.ModelClass;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class CreateGroupPage : ContentPage
{
    // Group logic
    private GroupLogic groupLogic;
    public CreateGroupPage()
    {
        // Instantiate groupLogic
        groupLogic = new GroupLogic();
        InitializeComponent();
    }

    private void CreateGroupButtonClicked(object sender, EventArgs e)
    {
        // Grab the text from the entry boxes
        var groupName = GroupNameEntry.Text;
        var groupDescription = GroupDescriptionEntry.Text;

        // Error checking for empty name
        if (groupName == null || groupName == "")
        {
            // Set error label text
            ErrorLabel.Text = "Please enter a group name";
            return;
        }

        // Error checking for empty description
        if (groupDescription == null || groupDescription == "")
        {
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

        groupLogic.AddItem(group);
        
        // Navigate back to the group page
        Navigation.PopAsync();
    }
}