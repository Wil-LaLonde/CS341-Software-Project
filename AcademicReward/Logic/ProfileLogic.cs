using AcademicReward.Database;
using AcademicReward.ModelClass;
using AcademicReward.Resources;

namespace AcademicReward.Logic; 

/// <summary>
///     ProfileLogic is the logic behind updating a profile (xp, level, points)
///     Primary Author: Xee Lo
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class ProfileLogic : ILogic {
    private readonly IDatabase profileDB;

    /// <summary>
    ///     ProfileLogic constructor
    /// </summary>
    public ProfileLogic() {
        profileDB = new ProfileDatabase();
    }

    //Currently not needed
    public LogicErrorType AddItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method used to update a profile (logic)
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType UpdateItem(object profile) {
        LogicErrorType logicError;
        Profile profileToUpdate = profile as Profile;

        //Making database call to update profile XP,Points,and Level
        DatabaseErrorType dbError = profileDB.UpdateItem(profileToUpdate);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.UpdateProfileDBError;

        return logicError;
    }

    //Currently not needed
    public LogicErrorType DeleteItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType LookupItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType AddItemWithArgs(object[] obj) {
        return LogicErrorType.NotImplemented;
    }
}