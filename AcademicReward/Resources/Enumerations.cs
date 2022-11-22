namespace AcademicReward.Resources {
    //Add logic error types to this enum
    public enum LogicErrorType {
        //LoginPage START
        EmptyUsername,
        EmptyPassword,
        EmptyAdminMemberRadioButton,
        InvalidUsernameLength,
        InvalidPasswordLength,
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
