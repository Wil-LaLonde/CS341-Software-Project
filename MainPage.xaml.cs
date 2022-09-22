using System.Collections.ObjectModel;

namespace Lab2;

public partial class MainPage : ContentPage {
	private ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
	private IBusinessLogic businessLogic;
	public MainPage() {
		InitializeComponent();
		businessLogic = new BusinessLogic();
		//Might have to set our entries list here???
	}
	private void AddEntryButtonClick(Object sender, EventArgs e) {
		//Add logic here to add the entry (call businessLogic)
		
		//Testing the Entry stuff
		int id = 0;
		String clue = "";
		String answer = "";
		String difficulty = "";
		String date = "";
		//Need to check the difficulty first
		int difficultyInt = int.TryParse(difficulty, out difficultyInt) ? difficultyInt : Entry.InvalidDifficultyEntry;

		Entry testEntry = new Entry(id, clue, answer, difficultyInt, date);
		bool test = false;
	}

	private void DeleteEntryButtonClick(Object sender, EventArgs e) {
		//Delete logic here to delete the entry (call businessLogic)
	}

	private void EditEntryButtonClick(Object sender, EventArgs e) {
		//Edit logic here to edit the entry (call businessLogic)
	}
}

