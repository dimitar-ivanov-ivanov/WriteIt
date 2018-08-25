namespace WriteIt.Utilities.Constants
{
    public class ValidationConstants
    {
        public const int MinUserFullNameLength = 5;

        public const int MaxUserFullNameLength = 60;

        public const int MinPasswordLength = 4;

        public const int MaxPasswordLength = 40;

        public const int MinPasswordRequiredUniqueChars = 1;

        public const bool PasswordRequireLowercase = true;

        public const bool PasswordRequireDigit = true;

        public const bool PasswordRequireUppercase = false;

        public const bool PasswordRequireNonAlphanumeric = false;

        public const int MinUsernameLength = 4;

        public const int MaxUsernameLength = 35;

        public const int MinEmailLength = 5;

        public const int MaxEmailLength = 40;

        public const int MinThreadNameLength = 3;

        public const int MaxThreadNameLength = 40;

        public const int MinThreadDescriptionLength = 10;

        public const int MaxThreadDescriptionLength = 200;

        public const int MinThreadInstanceNameLength = 3;

        public const int MaxThreadInstanceNameLength = 40;

        public const int MinThreadInstanceDescriptionLength = 10;

        public const int MaxThreadInstanceDescriptionLength = 200;

        public const int MinPostNameLength = 5;

        public const int MaxPostNameLength = 80;

        public const int MinPostDescriptionLength = 20;

        public const int MaxPostDescriptionLength = 1000;

        public const int MinCommentDescriptionLength = 4;

        public const int MaxCommentDescriptionLength = 500;
    }
}