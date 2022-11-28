namespace AcademicReward.Resources {
    /// <summary>
    /// Primary Author: Wil LaLonde 
    /// Secondary Author: Sean Stille, Xee Lo, Maximilian Patterson
    /// Reviewer: Wil LaLonde, Sean Stille, Xee Lo, Maximilian Patterson
    /// </summary>

    //Add logic error types to this enum
    public enum LogicErrorType {
        //LoginPage START
        EmptyUsername,
        EmptyPassword,
        EmptyReEnterPassword,
        EmptyAdminMemberRadioButton,
        PasswordMismatch,
        InvalidUsernameLength,
        InvalidPasswordLength,
        UsernameTaken,
        UsernameNotFound,
        PasswordIncorrect,
        AddProfileDBError,
        SignProfileInDBError,
        //LoginPage END

        //General START
        NoError
        //General END
    }

    //Add database error types to this enum
    public enum DatabaseErrorType {
        //LoginPage START
        AddProfileDBError,
        LoginProfileDBError,
        UsernameNotFoundDBError,
        UsernameTakenDBError,
        //LoginPage END

        //General START
        NoError
        //General END
    }
}
