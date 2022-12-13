using AcademicReward.Database;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// HistoryPage is the page that shows all history items for a profile
/// Primary Author: Maximilian Patterson
/// Secondary Author:
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HistoryPage : ContentPage {
    IDatabase HistoryDatabase = new HistoryDatabase();
    // New observable collection of HistoryItem
    public ObservableCollection<object> HistoryItems = new ObservableCollection<object>();

    /// <summary>
    /// HistoryPage constructor
    /// </summary>
    public HistoryPage() {
        InitializeComponent();
        HistoryItemsLV.ItemsSource = HistoryItems;
        
        string[] args = new string[] { MauiProgram.Profile.ProfileID.ToString() };
        HistoryDatabase.LoadItems(HistoryItems, args);
    }
}
