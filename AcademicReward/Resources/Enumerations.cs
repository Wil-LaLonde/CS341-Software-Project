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

        //Login Group Collection START
        LoginGroupCollectionDBError,
        //Login Group Collection END

        //Edit Profile Page START
        EmptyOldPassword,
        EmptyNewPassword,
        EmptyReEnterNewPassword,
        InvalidOldPasswordLength,
        InvalidNewPasswordLength,
        InvalidReEnterNewPasswordLength,
        CurrentPasswordError,
        UpdatePasswordDBError,
        //Edit Profile Page END

        //Notification START
        EmptyNotificationGroup,
        EmptyNotificationTitle,
        EmptyNotificationDescription,
        InvalidNotificationtitleLength,
        InvalidNotificationDescriptionLength,
        AddNotificationDBError,
        LookupAllNotificationsDBError,
        //Notification END

        //Task START
        EmptyTaskGroup,
        InvalidTaskPoints,
        EmptyTaskTitle,
        EmptyTaskDescription,
        NegativeTaskPoints,
        InvalidTaskTitleLength,
        InvalidTaskDescriptionLength,
        AddTaskDBError,
        LookupAllTasksDBError,
        UpdateTaskDBError,
        DeleteTaskDBError,
        //Task END

        //Shop START
        InvalidCost,
        InvalidLevel,
        UnsuccessfulDBAdd,
        NotEnoughDoubloons,
        NeedHigherLevel,
        //Shop END

        //History START (history calls are often made from LogicClasses
        HistoryAddError,
        //History END

        //Group START
        GroupCreateError,
        GroupAlreadyHasAdmin,
        //Group END

        //Profile START
        UpdateProfileDBError,
        //Profile END

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

        //Login Group Collection START
        LoginGroupCollectionDBError,
        //Login Group Collection END

        //Edit Profile Page START
        UpdatePasswordDBError,
        //Edit Profile Page END

        //Notification START
        AddNotificationDBError,
        LookupAllNotificationsDBError,
        //Notification END

        //Task START
        AddTaskDBError,
        LookupTaskDBError,
        LookupAllTasksDBError,
        UpdateTaskDbError,
        DeleteTaskDBError,
        //Task END

        //Shop START
        BuyItemError,
        //Shop END

        // History START
        AddHistoryDBError,
        LookupAllHistoryDBError,
        LoadHistoryDBError,
        UpdateHistoryDBError,
        DeleteHistoryDBError,
        // History END

        // Group START
        UpdateGroupDBError,
        AddGroupDBError,
        // Group END

        //Profile START
        UpdateProfileDBError,
        //Profile END

        //General START
        NoError
        //General END
    }
}
