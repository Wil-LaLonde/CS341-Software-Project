namespace AcademicReward.Resources {
    /// <summary>
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
        //Task END

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
