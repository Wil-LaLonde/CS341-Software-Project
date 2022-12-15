using System.Collections.ObjectModel;
using System.Diagnostics;
using AcademicReward.Database;
using AcademicReward.ModelClass;

namespace AcademicReward.Views;

/// <summary>
///     PurchaseHistoryPage shows all purchased items for a profile
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public partial class PurchaseHistoryPage : ContentPage {
    public ObservableCollection<PurchaseHistoryItem> PurchaseHistoryItems = new();

    public PurchaseHistoryPage() {
        InitializeComponent();
        GetAllPurchaseHistory();
    }


    public void GetAllPurchaseHistory() {
        base.OnAppearing();
        PurchaseHistoryItems = PurchaseHistoryProfileRelationship.GetPurchaseHistory(MauiProgram.Profile);
        Debug.WriteLine(PurchaseHistoryItems.Count);
        PurchaseHistoryItemsLV.ItemsSource = PurchaseHistoryItems;
    }
}