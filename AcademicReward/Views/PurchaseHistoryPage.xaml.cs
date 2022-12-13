using AcademicReward.Database;
using AcademicReward.ModelClass;
using System.Collections.ObjectModel;

namespace AcademicReward.Views;

/// <summary>
/// PurchaseHistoryPage shows all purchased items for a profile
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
        GetAllPurchaseHistory();
    }


    public void GetAllPurchaseHistory()
    {
        base.OnAppearing();
        PurchaseHistoryItems = PurchaseHistoryProfileRelationship.GetPurchaseHistory(MauiProgram.Profile);
        System.Diagnostics.Debug.WriteLine(PurchaseHistoryItems.Count);
        PurchaseHistoryItemsLV.ItemsSource = PurchaseHistoryItems;
    }
}