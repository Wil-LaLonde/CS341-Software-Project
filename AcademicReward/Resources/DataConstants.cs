namespace AcademicReward.Resources {
    /// <summary>
    /// DataConstants is the file that holds all data constants to avoid "magic values"
    /// Primary Author: Wil LaLonde 
    /// Secondary Author: Sean Stille, Xee Lo, Maximilian Patterson
    /// Reviewer: Wil LaLonde, Sean Stille, Xee Lo, Maximilian Patterson
    /// </summary>
    public static class DataConstants {
        //LoginPage constants START
        public const string EmptyUsernameTitle = "Empty Username";
        public const string EmptyUsernameMessage = "Please fill in the username entry box.";
        public const string EmptyPasswordTitle = "Empty Password";
        public const string EmptyPasswordMessage = "Please fill in the password entry box.";
        public const string EmptyAdminMemberRadioButtonMessage = "An account type is required.";
        public const string EmptyReEnterPasswordMessage = "Please fill in the re-enter password entry box.";
        public const string PasswordMismatchMessage = "Passwords must match.";
        public const string UsernameLengthTitle = "Invalid Username Length";
        public const string UsernameLengthMessage = "Username must be between 1-25 characters.";
        public const string PasswordLengthTitle = "Invalid Password Length";
        public const string PasswordLengthMessage = "Password must be between 1-50 characters.";
        public const string UsernameTakenMessage = "Username already being used.";
        public const string UsernameNotFoundTitle = "Username Not Found";
        public const string IncorrectPasswordTitle = "Incorrect Password";
        public const string IncorrectPasswordMessage = "The password provided was incorrect, please try again.";
        public const string UsernameNotFoundMessage = "The username entered was not found.";
        public const string AddProfileDBErrorTitle = "Add Profile Database Error";
        public const string AddProfileDBErrorMessage = "A database error occurred while adding your account.";
        public const string AddProfileSuccessTitle = "Account Creation Success!";
        public const string AddProfileSuccessMessage = "Your account has been created successfully.";
        public const string AddProfileUnknownTitle = "Account Creation Failure";
        public const string AddProfileUnknownMesage = "An unknown error occurred during account creation.";
        public const string SignProfileInDBTitle = "Sign In Database Error";
        public const string SignProfileInDBMessage = "An unexpected database error occurred while signing you in.";
        public const string SignProfileInUnkownTitle = "Sign In Unknown Error";
        public const string SignProfileInUnknownMessage = "An unknown error occurred while signing you in.";
        //LoginPage constants END

        //Login Group Collection START
        public const string LoginGroupCollectionTitle = "Group Collection Error";
        public const string LoginGroupCollectionMessage = "An error occurred while gathering your groups. The application can still be used, but no groups will load. Try logging out and in again.";
        //Login Group Collection END

        //Edit Profile Page START
        public const string UpdateProfileXpPointsLevelTitle = "Error Update Profile";
        public const string UpdateProfileXpPointsLevelMessage = "There was an error updating your profile.";
        public const string UpdatePasswordSuccessTitle = "Password Updated!";
        public const string UpdatePasswordSuccessMessage = "Your pasword has been updated successfully.";
        public const string EmptyOldPasswordTitle = "Empty Old Password";
        public const string EmptyOldPasswordMessage = "Please fill in the old password entry box.";
        public const string EmptyNewPasswordTitle = "Empty New Password";
        public const string EmptyNewPasswordMessage = "Please fill in the new password entry box.";
        public const string EmptyReEnterNewPasswordTitle = "Empty Re-Enter New Password";
        public const string EmptyReEnterNewPasswordMessage = "Please fill in the re-enter new password entry box.";
        public const string PasswordMismatchTitle = "Password Mismatch";
        public const string InvalidOldPasswordLengthTitle = "Invalid Old Password Length";
        public const string InvalidOldPasswordLengthMessage = "Old password must be between 1-50 characters.";
        public const string InvalidNewPasswordLengthTitle = "Invalid New Password Length";
        public const string InvalidNewPasswordLengthMessage = "New password must be between 1-50 characters.";
        public const string InvalidReEnterNewPasswordLengthTitle = "Invalid Re-enter New Password Length";
        public const string InvalidReEnterNewPasswordLengthMessage = "Re-enter new password must be between 1-50 characters.";
        public const string OldPasswordIncorrectMessage = "The old password entered is not correct, please try again.";
        public const string CurrentPasswordErrorTitle = "Old & New Password Error";
        public const string CurrentPasswordErrorMessage = "The new password cannot be the same as the old password, please try again.";
        public const string UpdatePasswordDBErrorTitle = "Update Password Databse Error";
        public const string UpdatePasswordDBErrorMessage = "An unexpected database error occurred while updating your password.";
        public const string UpdatePasswordUnknownTitle = "Update Password Unknown Error";
        public const string UpdatePasswordUnknownMessage = "An unknown error occurred while updating your password.";
        //Edit Profile Page END

        //Notification START
        public const string EmptyNotificationGroupMessage = "A notification group selection is required.";
        public const string EmptyNotificationTitleMessage = "Please fill in the notification title entry box.";
        public const string EmptyNotificationDescriptionMessage = "Please fill in the notification description entry box.";
        public const string InvalidNotificationTitleLengthMessage = "Notification title must be between 1-50 characters.";
        public const string InvalidNotificationDescriptionLengthMessage = "Notification description must be between 1-250 characters.";
        public const string CreateNotificationSuccessTitle = "Notification Creation Success!";
        public const string CreateNotificationSuccessMessage = "Your notification has been created successfully.";
        public const string AddNotificationUnknownMessage = "An unknown error occurred during notification creation.";
        public const string LookupNotificationDBErrorTitle = "Error Loading Notifications";
        public const string LookupNotificationDBErrorMessage = "There was an unexpected error loading your notifications.";
        //Notification END

        //Task START
        public const string EmptyTaskGroupMessage = "A task group selection is required.";
        public const string InvalidTaskPointsMessage = "Points must be a whole numeric value.";
        public const string CreateTaskSuccessTitle = "Task Creation Success!";
        public const string CreateTaskSuccessMessage = "Your task has been created successfully.";
        public const string EmptyTaskTitleMessage = "Please fill in the task title entry box.";
        public const string EmptyTaskDescriptionMessage = "Please fill in the task description entry box.";
        public const string NegativeTaskPointsMessage = "Points cannot be negative.";
        public const string InvalidTaskTitleLengthMessage = "Task title must be between 1-50 characters.";
        public const string InvalidTaskDescriptionLengthMessage = "Task description must be between 1-250 characters.";
        public const string AddTaskDBErrorMessage = "A database error occurred while adding your task.";
        public const string AddTaskUnknownMessage = "An unknown error occurred during task creation.";
        public const string LookupTaskDBErrorTitle = "Error Loading Tasks";
        public const string LookupTaskDBErrorMessage = "There was an unexpected error loading your tasks.";
        public const string UpdatingTask = "There was an unexpected error updating your task.";
        //Task END

        //History START
        public const string HistoryEditPasswordTitle = "Password Updated";
        public const string HistoryEditPasswordDescription = "Your password has been updated on {0}";
        public const string HistoryCreateNotificationTitle = "Notification Created";
        public const string HistoryCreateGroupTitle = "New Group Created";
        public const string HistoryCreateGroupDescription = "You created group: {0}";
        public const string HistoryAddMemberToGroupGroupTitle = "Member added to group";
        public const string HistoryAddMemberToGroupGroupDescription = "You added: {0} to group: {1}";
        public const string HistoryCreateNotificationDescription = "You created {0} notification for {1}";
        public const string HistoryCreateTaskTitle = "Task Created";
        public const string HistoryCreateTaskDescription = "You created {0} task for {1}";
        public const string HistoryAddShopItemTitle = "Shop Item Created";
        public const string HistoryAddShopItemDescription = "You created shop item: {0} for group: {1}.";
        //History END

        //Create Group START
        public const string EmptyGroupNameMessage = "Please fill in the group name entry box.";
        public const string EmptyGroupDescriptionMessage = "Please fill in the group description entry box.";
        public const string InvalidGroupNameLengthMessage = "Group name must be between 1-50 characters.";
        public const string InvalidGroupDescriptionMessage = "Group description must be between 1-250 characters.";
        public const string CreateGroupDBErrorMessage = "An unexpected database error occurred while creating your group.";
        public const string CreateGroupUnknownErrorMessage = "An unknown error occurred while creating your group.";
        //Create Group END

        //Add Member START
        public const string EmptyMemberNameMessage = "Please fill in the member username entry box.";
        public const string InvalidMemberUsernameLengthMessage = "Member username must be between 1-25 characters.";
        public const string AddMemberAlreadyInGroupMessage = "Member is already in the group.";
        public const string GroupAlreadyHasAdminMessage = "Only one admin can be in a group.";
        public const string AddMemberUserNameNotFoundMessage = "Member username not found.";
        public const string AddMemberDBErrorMessage = "An unexpected datbase error occurred while adding the member to the group.";
        public const string AddMemberUnknownErrorMessage = "An unknown error occurred while adding the member to the group.";
        //Add Member END

        //Home Page START
        public const string TaskSubmittedTitle = "Task Submitted!";
        public const string TaskSubmittedMessage = "Task was succesfully submitted. You are now waiting for approval from your group admin.";
        public const string TaskApprovedTitle = "Task Approved!";
        public const string TaskApprovedMessage = "Task was succesfully approved. The member will receive their points and XP the next time they sign in.";
        //Home Page END

        //Shop START
        public const string InvalidItemCostMessage = "Item cost must be a whole numeric value.";
        public const string InvalidLevelMessage = "Level requirement must be a whole numeric value.";
        public const string EmptyShopItemTitleMessage = "Please fill in the item title entry box.";
        public const string EmptyShopItemDescriptionMessage = "Please fill in the item descrription entry box.";
        public const string NegativeShopItemCostMessage = "Item cost cannot be negative.";
        public const string NegativeShopItemLevelRequirementMessage = "Level requirement must be at least 1.";
        public const string InvalidShopItemTitleLengthMessage = "Item title must be between 1-25 characters.";
        public const string InvalidShopItemDescriptionLengthMessage = "Item description must be between 1-250 characters.";
        public const string AddShopItemDBErrorMessage = "An unexpected database error occurred while adding the shop item.";
        public const string AddShopItemUnknownErrorMessage = "An unknown error occurred while adding the shop item.";
        //Shop END

        //Display Alert START
        public const string OK = "OK";
        public const string Cancel = "Cancel";
        //Display Alert END

        //General START
        public const string SpaceDashSpace = " - ";
        public const string Colon = ":";
        public const string GoBack = "..";
        public const string SpaceSlashSpace = " / ";
        //General END
    }
}
