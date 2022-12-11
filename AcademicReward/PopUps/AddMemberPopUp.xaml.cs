using AcademicReward.Logic;
using AcademicReward.ModelClass;
using AcademicReward.Views;
using CommunityToolkit.Maui.Views;

namespace AcademicReward.PopUps;

/// <summary>
/// Primary Author: Wil LaLonde
/// Secondary Author: None
/// Reviewer: Maximilian Patterson
/// </summary>
public partial class AddMemberPopUp : Popup {
	
	public Group group;
	public AddMemberPopUp(Group group) {
		InitializeComponent();
		this.group = group;
	}

    private void BackButtonClicked(object sender, EventArgs e) => Close();

	private void AddMemberButtonClicked(object sender, EventArgs e)
	{
		// Grab the text from the entry box
		String memberName = MemberNameEntry.Text;

		// Add member logic
		var addMemberLogic = new AddMemberLogic();

		// Create an array with two elements, the current group and the member name
		object[] obj = new object[] { memberName, group };

		var error = addMemberLogic.AddItemWithArgs(obj);

		Close();
	}
}