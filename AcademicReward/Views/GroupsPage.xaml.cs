using System.Collections.ObjectModel;

namespace AcademicReward.Views;

public partial class GroupsPage : ContentPage
{
    public ObservableCollection<Group> Groups = new ObservableCollection<Group>();
    public GroupsPage()
    {
        InitializeComponent();
        GroupsLV.ItemsSource = Groups;
        TestData();
    }

    public void TestData()
    {
        base.OnAppearing();

        // Add some test data
        Groups.Add(new Group { Name = "Group 1" });
        Groups.Add(new Group { Name = "Group 2" });
        Groups.Add(new Group { Name = "Group 3" });
        Groups.Add(new Group { Name = "Group 4" });
        Groups.Add(new Group { Name = "Group 5" });
    }

    // Private class for HistoryItem
    public class Group
    {
        public string Name { get; set; }
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