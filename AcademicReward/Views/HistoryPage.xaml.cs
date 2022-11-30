using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;
using Npgsql;
using System.Collections.ObjectModel;


namespace AcademicReward.Views;

/// <summary>
/// Primary Author: Maximilian Patterson
/// Secondary Author: Wil LaLonde
/// Reviewer: Wil LaLonde
/// </summary>
public partial class HistoryPage : ContentPage
{
    IDatabase HistoryDatabase = new HistoryDatabase();
    // New observable collection of HistoryItem
    public ObservableCollection<object> HistoryItems = new ObservableCollection<object>();

    public HistoryPage()
    {
        InitializeComponent();
        HistoryItemsLV.ItemsSource = HistoryItems;
        
        string[] args = new string[] { MauiProgram.Profile.ProfileID.ToString() };
        HistoryDatabase.LoadItems(HistoryItems, args);
    }
}