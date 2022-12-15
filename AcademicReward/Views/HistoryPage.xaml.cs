using System.Collections.ObjectModel;
using AcademicReward.Database;

namespace AcademicReward.Views;

/// <summary>
///     HistoryPage is the page that shows all history items for a profile
///     Primary Author: Maximilian Patterson
///     Secondary Author:
///     Reviewer: Wil LaLonde
/// </summary>
public partial class HistoryPage : ContentPage {
    private readonly IDatabase _historyDatabase = new HistoryDatabase();

    // New observable collection of HistoryItem
    public ObservableCollection<object> HistoryItems = new();

    /// <summary>
    ///     HistoryPage constructor
    /// </summary>
    public HistoryPage() {
        InitializeComponent();
        HistoryItemsLV.ItemsSource = HistoryItems;

        string[] args = { MauiProgram.Profile.ProfileId.ToString() };
        _historyDatabase.LoadItems(HistoryItems, args);
    }
}
