using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: None
/// Reviewer: Wil LaLonde
/// </summary>
public partial class PurchaseHistoryPage : ContentPage
{
    public ObservableCollection<PurchaseHistoryItem> PurchaseHistoryItems = new ObservableCollection<PurchaseHistoryItem>();

    public PurchaseHistoryPage()
    {
        InitializeComponent();
        PurchaseHistoryItemsLV.ItemsSource = PurchaseHistoryItems;
        TestData();
    }


    public void TestData()
    {
        base.OnAppearing();

        // Add some test data
        PurchaseHistoryItems.Add(new PurchaseHistoryItem { Name = "Purchase 1", Description = "Purchase Description 1", Date = DateTime.Now });
        PurchaseHistoryItems.Add(new PurchaseHistoryItem { Name = "Purchase 2", Description = "Purchase Description 2", Date = DateTime.Now });
        PurchaseHistoryItems.Add(new PurchaseHistoryItem { Name = "Purchase 3", Description = "Purchase Description 3", Date = DateTime.Now });
        PurchaseHistoryItems.Add(new PurchaseHistoryItem { Name = "Purchase 4", Description = "Purchase Description 4", Date = DateTime.Now });
        PurchaseHistoryItems.Add(new PurchaseHistoryItem { Name = "Purchase 5", Description = "Purchase Description 5", Date = DateTime.Now });
    }

    // Private class for HistoryItem
    public class PurchaseHistoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}