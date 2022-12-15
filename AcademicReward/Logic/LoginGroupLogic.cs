using AcademicReward.Database;
using AcademicReward.Resources;

namespace AcademicReward.Logic; 

/// <summary>
///     LoginGroupLogic is the logic behind gathering all groups for a profile upon signing in
///     Primary Author: Wil LaLonde
///     Secondary Author: None
///     Reviewer: Maximilian Patterson
/// </summary>
public class LoginGroupLogic : ILogic {
    private readonly IDatabase _loginGroupDb;

    /// <summary>
    ///     LoginGroupLogic constructor
    /// </summary>
    public LoginGroupLogic() {
        _loginGroupDb = new LoginGroupDatabase();
    }

    //Currently not needed
    public LogicErrorType AddItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType UpdateItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    //Currently not needed
    public LogicErrorType DeleteItem(object obj) {
        return LogicErrorType.NotImplemented;
    }

    /// <summary>
    ///     Method that calls the database to gather
    /// </summary>
    /// <param name="profile">object profile</param>
    /// <returns>LogicErrorType logicError</returns>
    public LogicErrorType LookupItem(object profile) {
        LogicErrorType logicError;
        DatabaseErrorType dbError = _loginGroupDb.LookupFullItem(profile);
        if (DatabaseErrorType.NoError == dbError)
            logicError = LogicErrorType.NoError;
        else
            logicError = LogicErrorType.LoginGroupCollectionDbError;
        return logicError;
    }

    //Currently not needed
    public LogicErrorType AddItemWithArgs(object[] obj) {
        throw new NotImplementedException();
    }
}