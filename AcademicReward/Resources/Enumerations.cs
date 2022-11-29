﻿namespace AcademicReward.Resources {
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
        //Task END

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

        //Notification START
        AddNotificationDBError,
        LookupAllNotificationsDBError,
        //Notification END

        //Task START
        AddTaskDBError,
        LookupAllTasksDBError,
        //Task END

        //General START
        NoError
        //General END
    }
}
