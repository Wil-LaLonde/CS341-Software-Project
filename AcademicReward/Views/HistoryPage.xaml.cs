using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: Wil LaLonde
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HistoryPage : ContentPage
{
	// New observable collection of HistoryItem
	public ObservableCollection<HistoryItem> HistoryItems = new ObservableCollection<HistoryItem>();

	public HistoryPage()
	{
		InitializeComponent();
		HistoryItemsLV.ItemsSource = HistoryItems;
		TestData();
	}

	public void TestData()
	{
		base.OnAppearing();

		// Add some test data
		HistoryItems.Add(new HistoryItem { Name = "Test 1", Description = "Description 1", Date = DateTime.Now });
		HistoryItems.Add(new HistoryItem { Name = "Test 2", Description = "Description 2", Date = DateTime.Now });
		HistoryItems.Add(new HistoryItem { Name = "Test 3", Description = "Description 3", Date = DateTime.Now });
		HistoryItems.Add(new HistoryItem { Name = "Test 4", Description = "Description 4", Date = DateTime.Now });
		HistoryItems.Add(new HistoryItem { Name = "Test 5", Description = "Description 5", Date = DateTime.Now });
	}

	// Private class for HistoryItem
	public class HistoryItem
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
	}
}