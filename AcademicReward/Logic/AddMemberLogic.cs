using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic; 

/// <summary>
///     AddMemberLogic is the logic behind adding a member to a group
///     Primary Author: Maximilian Patterson
///     Secondary Author: None
///     Reviewer: Wil LaLonde
/// </summary>
public class AddMemberLogic : ILogic {
    private readonly IDatabase _groupDb;
    private readonly IDatabase _historyDb;

    /// <summary>
    ///     LoginGroupLogic constructor
    /// </summary>
    public AddMemberLogic() {
        _groupDb = new GroupDatabase();
        _historyDb = new HistoryDatabase();
    }

    /// <summary>
    ///     Method used to add a profile to a group
    /// </summary>
    /// <param name="obj">object[] obj</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType AddItemWithArgs(object[] obj) {
        LogicErrorType logicError;
        // Convert object to array
        object[] arguments = obj;
        logicError = AddMemberCheck(arguments[0] as string);
        if (LogicErrorType.NoError == logicError) {
            // Find profile by username
            Profile profile = GroupProfileRelationship.FindByUsername(arguments[0] as string);

            // Set group from arguments
            Group group = arguments[1] as Group;
            logicError = GroupProfileRelationship.AddProfileToGroup(profile, group);
            if (LogicErrorType.NoError == logicError)
                //Add history item
                _historyDb.AddItem(new HistoryItem(MauiProgram.Profile.ProfileId,
                    DataConstants.HistoryAddMemberToGroupGroupTitle,
                    string.Format(DataConstants.HistoryAddMemberToGroupGroupDescription, profile.Username,
                        group.GroupName)));
        }

        return logicError;
    }

    /// <summary>
    ///     Method used to update a group
    /// </summary>
    /// <param name="obj">object obj</param>
    /// <returns>LogicErrorType logicType</returns>
    public LogicErrorType UpdateItem(object obj) {
        // Update database
        DatabaseErrorType dbError = _groupDb.UpdateItem(obj);
        return LogicErrorType.NoError;
    }

    //Currently not needed
    public LogicErrorType DeleteItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType LookupItem(object profile) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType AddItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    /// <summary>
    ///     Helper method used to check the given member username
    /// </summary>
    /// <param name="username">string username</param>
    /// <returns>LogicErrorType logicError</returns>
    private LogicErrorType AddMemberCheck(string username) {
        LogicErrorType logicError;
        if (string.IsNullOrEmpty(username))
            logicError = LogicErrorType.EmptyUsername;
        else if (CheckUsernameLength(username))
            logicError = LogicErrorType.InvalidUsernameLength;
        else
            logicError = LogicErrorType.NoError;
        return logicError;
    }

    /// <summary>
    ///     Helper method used to check the username length
    /// </summary>
    /// <param name="username">string username</param>
    /// <returns>true/false</returns>
    private bool CheckUsernameLength(string username) {
        int usernameLength = username.Length;
        return usernameLength < Profile.MinUsernameLength || usernameLength > Profile.MaxUsernameLength;
    }
}