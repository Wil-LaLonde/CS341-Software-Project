namespace AcademicReward.Resources; 

/// <summary>
///     Enumerations holds all our LogicErrorTypes and DatabaseErrorTypes
///     Primary Author: Wil LaLonde
///     Secondary Author: Sean Stille, Xee Lo, Maximilian Patterson
///     Reviewer: Wil LaLonde, Sean Stille, Xee Lo, Maximilian Patterson
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
    AddProfileDbError,
    SignProfileInDbError,
    //LoginPage END

    //Login Group Collection START
    LoginGroupCollectionDbError,
    //Login Group Collection END

    //Edit Profile Page START
    EmptyOldPassword,
    EmptyNewPassword,
    EmptyReEnterNewPassword,
    InvalidOldPasswordLength,
    InvalidNewPasswordLength,
    InvalidReEnterNewPasswordLength,
    CurrentPasswordError,
    UpdatePasswordDbError,
    //Edit Profile Page END

    //Notification START
    EmptyNotificationGroup,
    EmptyNotificationTitle,
    EmptyNotificationDescription,
    InvalidNotificationtitleLength,
    InvalidNotificationDescriptionLength,
    AddNotificationDbError,
    LookupAllNotificationsDbError,
    //Notification END

    //Task START
    EmptyTaskGroup,
    InvalidTaskPoints,
    EmptyTaskTitle,
    EmptyTaskDescription,
    NegativeTaskPoints,
    InvalidTaskTitleLength,
    InvalidTaskDescriptionLength,
    AddTaskDbError,
    LookupAllTasksDbError,
    UpdateTaskDbError,
    DeleteTaskDbError,
    //Task END

    //Shop START
    EmptyShopItemGroup,
    InvalidCost,
    InvalidLevel,
    EmptyShopItemTitle,
    EmptyShopItemDescription,
    NegativeShopItemCost,
    NegativeShopItemLevelRequirement,
    InvalidShopItemLength,
    InvalidShopItemDescriptionLength,
    LookupAllShopItemsDbError,
    AddShopItemDbError,
    UpdateShopItemDbError,
    DeleteShopItemDbError,
    UnsuccessfulDbAdd,
    NotEnoughDoubloons,
    NeedHigherLevel,
    BuyItemError,
    //Shop END

    //History START (history calls are often made from LogicClasses
    HistoryAddError,
    //History END

    //Group START
    EmptyGroupName,
    EmptyGroupDescription,
    InvalidGroupNameLength,
    InvalidGroupDescriptionLength,
    GroupCreateError,
    //Group END

    //Add Member START
    GroupAlreadyHasAdmin,
    MemberAlreadyInGroup,
    //Add Member END

    //Profile START
    UpdateProfileDbError,
    //Profile END

    //General START
    NoError,

    NotImplemented
    //General END
}

//Add database error types to this enum
public enum DatabaseErrorType {
    //LoginPage START
    AddProfileDbError,
    LoginProfileDbError,
    UsernameNotFoundDbError,
    UsernameTakenDbError,
    //LoginPage END

    //Login Group Collection START
    LoginGroupCollectionDbError,
    //Login Group Collection END

    //Edit Profile Page START
    UpdatePasswordDbError,
    //Edit Profile Page END

    //Notification START
    AddNotificationDbError,
    LookupAllNotificationsDbError,
    //Notification END

    //Task START
    AddTaskDbError,
    LookupTaskDbError,
    LookupAllTasksDbError,
    UpdateTaskDbError,
    DeleteTaskDbError,
    //Task END

    //Shop START
    AddShopItemDbError,
    UpdateShopItemDbError,
    LookupAllShopItemsDbError,
    DeleteShopItemDbError,
    BuyItemError,
    //Shop END

    // History START
    AddHistoryDbError,
    LookupAllHistoryDbError,
    LoadHistoryDbError,
    UpdateHistoryDbError,
    DeleteHistoryDbError,
    // History END

    // Group START
    UpdateGroupDbError,
    AddGroupDbError,
    // Group END

    //Profile START
    UpdateProfileDbError,
    //Profile END

    //General START
    NoError,

    NotImplemented
    //General END
}