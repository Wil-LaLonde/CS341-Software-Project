using System.Collections.ObjectModel;

namespace AcademicReward.Views;

public partial class GroupPage : ContentPage
{
    public ObservableCollection<Member> Members = new ObservableCollection<Member>();
    public GroupPage()
    {
        InitializeComponent();
        MembersLV.ItemsSource = Members;
        TestData();
    }

    public void TestData()
    {
        base.OnAppearing();

        // Add some test data
        Members.Add(new Member { Name = "Billy" });
        Members.Add(new Member { Name = "Tom" });
        Members.Add(new Member { Name = "Bob" });        
    }

    // Private class for HistoryItem
    public class Member
    {
        public string Name { get; set; }
    }
}