using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.PopUps;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson 
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class GroupPage : ContentPage
{
    public Group group;
    public ObservableCollection<Profile> Members = new ObservableCollection<Profile>();
    public GroupPage(Group group)
    {
        InitializeComponent();

        this.group = group;

        Members = GroupProfileRelationship.getProfilesInGroup(group);
        MembersLV.ItemsSource = Members;

        GroupDescriptionLbl.Text = group.GroupDescription;
        GroupNameLbl.Text = group.GroupName;
        ShowAdminName(group);
    }

    public void ShowAdminName(Group group)
    {
        base.OnAppearing();
        LoginDatabase loginDatabase = new LoginDatabase();
        Object admin = loginDatabase.FindById(group.AdminProfileID);
        Profile adminProfile = admin as Profile;
        GroupAdminLbl.Text = adminProfile.Username;
    }

    public async void AddMemberButtonClickedAsync(object sender, EventArgs e)
    {
        // Include reference to this page so that the listview of members can be updated
        AddMemberPopUp addMemberPopUp = new AddMemberPopUp(group, ref Members);

        var result = await this.ShowPopupAsync(addMemberPopUp);

        if (result is ObservableCollection<Profile> membersResult)
        {
            if (membersResult != null)
            {
                Members = GroupProfileRelationship.getProfilesInGroup(group);
                MembersLV.ItemsSource = Members;
            }
        }
    }
}