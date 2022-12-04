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
    public ObservableCollection<Profile> Members = new ObservableCollection<Profile>();
    public GroupPage(Group group)
    {
        InitializeComponent();
        
        Members = GroupProfileRelationship.getProfilesInGroup(group);
        MembersLV.ItemsSource = Members;
        
        GroupDescriptionLbl.Text = group.GroupDescription;
        GroupNameLbl.Text = group.GroupName;
        ShowAdminName(group);
    }

    public void ShowAdminName(Group group)
    {
        base.OnAppearing();
        LoginDatabase loginDatabase= new LoginDatabase();
        Object admin = loginDatabase.FindById(group.AdminProfileID);
        Profile adminProfile = admin as Profile;
        GroupAdminLbl.Text = adminProfile.Username;
    }

    public void AddMemberButtonClicked(object sender, EventArgs e) {
        AddMemberPopUp addMemberPopUp = new AddMemberPopUp();
        this.ShowPopup(addMemberPopUp);
    }
}