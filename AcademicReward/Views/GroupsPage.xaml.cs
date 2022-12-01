using AcademicReward.Database;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class GroupsPage : ContentPage
{
    public ObservableCollection<object> Groups = new ObservableCollection<object>();
    public GroupsPage()
    {
        InitializeComponent();
        GroupsLV.ItemsSource = MauiProgram.Profile.GroupList;
    }

    private void AddGroup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateGroupPage());
    }

    private void ShowSampleGroupPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new GroupPage());
    }
}