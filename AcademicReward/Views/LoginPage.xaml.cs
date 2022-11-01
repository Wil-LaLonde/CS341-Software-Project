namespace AcademicReward.Views;

public partial class LoginPage : ContentPage {
	public LoginPage() {
		InitializeComponent();
	}

	private async void LogInButtonClicked(object sender, EventArgs e) {
		//Some validation here on the user's credentials. For now, just send to home page.
		//Will also need a way to tell the difference between users???
		await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
	}

	private void AddAccountButtonClicked(object sender, EventArgs e) {
		//create a pop up window to allow the user to create an account.
	}
}