namespace AcademicReward.Resources {
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

        //Notification START
        public const string EmptyNotificationGroupMessage = "A notification group selection is required.";
        //Notification END

        //Task START
        public const string EmptyTaskGroupMessage = "A task group selection is required.";
        //Task END

        //Display Alert START
        public const string OK = "OK";
        public const string Cancel = "Cancel";
        //Display Alert END

        //General START
        public const string SpaceDashSpace = " - ";
        public const string Colon = ":";
        //General END
    }
}
