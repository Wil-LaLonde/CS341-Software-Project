using AcademicReward.ModelClass;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// GroupsPage is the page that shows all groups for a given profile
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class GroupsPage : ContentPage {
    public ObservableCollection<object> Groups = new ObservableCollection<object>();

    /// <summary>
    /// GroupPage constructor
    /// </summary>
    public GroupsPage() {
        InitializeComponent();
        GroupsLV.ItemsSource = MauiProgram.Profile.GroupList;

        // Hide add group button and text if user is not an admin
        if (!MauiProgram.Profile.IsAdmin) {
            AddGroupBtn.IsVisible = false;
            AddGroupLbl.IsVisible = false;
        }
    }

    /// <summary>
    /// Method called when a user clicks on the add group button
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">EventArgs e</param>
    private void AddGroup(object sender, EventArgs e) {
        Navigation.PushAsync(new CreateGroupPage());
    }

    /// <summary>
    /// Method called when a user selects a group
    /// </summary>
    /// <param name="sender">object sender</param>
    /// <param name="e">SelectedItemChangedEventArgs e</param>
    private void SelectedGroup(object sender, SelectedItemChangedEventArgs e) {
        Group selectedGroup = e.SelectedItem as Group;
        if (selectedGroup != null) {
            Navigation.PushAsync(new GroupPage(selectedGroup));
        }
    }
}
