using System.Collections.ObjectModel;
using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.Views;

/// <summary>
///     GroupPage is the page that is shown when a user clicks on a group
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class GroupPage : ContentPage {
    public Group group;
    public ObservableCollection<Profile> Members = new();

    /// <summary>
    ///     GroupPage constructor
    /// </summary>
    /// <param name="group">Group group</param>
    public GroupPage(Group group) {
        InitializeComponent();

        this.group = group;

        Members = GroupProfileRelationship.getProfilesInGroup(group);
        UpdateMembersXP();
        MembersLV.ItemsSource = Members;

        GroupDescriptionLbl.Text = group.GroupDescription;
        GroupNameLbl.Text = group.GroupName;
        ShowAdminName(group);

        // Hide add group button and text if user is not an admin
        if (!MauiProgram.Profile.IsAdmin) {
            AddMemberBtn.IsVisible = false;
            AddMemberLbl.IsVisible = false;
        }
    }

    /// <summary>
    ///     Method used to show the admin name
    /// </summary>
    /// <param name="group">Group group</param>
    public void ShowAdminName(Group group) {
        base.OnAppearing();
        LoginDatabase loginDatabase = new();
        object admin = loginDatabase.FindById(group.AdminProfileID);
        Profile adminProfile = admin as Profile;
        GroupAdminLbl.Text = adminProfile.Username;
    }

    /// <summary>
    ///     Method called when a user clicks on the add member button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    public async void AddMemberButtonClickedAsync(object sender, EventArgs e) {
        // Include reference to this page so that the listview of members can be updated
        AddMemberPopUp addMemberPopUp = new(group, ref Members);

        object result = await this.ShowPopupAsync(addMemberPopUp);

        if (result is ObservableCollection<Profile> membersResult)
            if (membersResult != null) {
                Members = GroupProfileRelationship.getProfilesInGroup(group);
                UpdateMembersXP();
                MembersLV.ItemsSource = Members;
            }
    }

    /// <summary>
    ///     Helper method used to properly show the correct amount of XP for a member
    /// </summary>
    private void UpdateMembersXP() {
        foreach (Profile member in Members) member.XP = member.GetCurrentXPInt();
    }
}