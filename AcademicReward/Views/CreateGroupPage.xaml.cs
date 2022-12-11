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