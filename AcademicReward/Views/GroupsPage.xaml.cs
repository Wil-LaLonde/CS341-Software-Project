using AcademicReward.Database;
using AcademicReward.ModelClass;
using System.Collections.ObjectModel;
using System.Windows.Input;

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

        // Hide add group button and text if user is not an admin
        if (!MauiProgram.Profile.IsAdmin)
        {
            AddGroupBtn.IsVisible = false;
            AddGroupLbl.IsVisible = false;
        }
    }

    private void AddGroup(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateGroupPage());
    }

    private void SelectedGroup(object sender, SelectedItemChangedEventArgs e)
    {
        ModelClass.Group selectedGroup = e.SelectedItem as ModelClass.Group;
        if (selectedGroup != null)
        {
            Navigation.PushAsync(new GroupPage(selectedGroup));
        }
    }
}