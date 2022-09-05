namespace WebAPI.Helpers
{
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        public CustomLanguageManager()
        {
            //en-US
            AddTranslation("en-US", "NotEmptyValidator",
                           "{PropertyName} is required");
            AddTranslation("en-US", "LengthValidator",
                           "{PropertyName} should be between {MinLength} and {MaxLength} characters");
            AddTranslation("en-US", "EmailValidator",
                           "Invalid format of email address");
            AddTranslation("en-US", "MaximumLengthValidator",
                          "The length of {PropertyName} must be {MaxLength} characters or fewer");
            AddTranslation("en-US", "MinimumLengthValidator",
                         "The length of {PropertyName} must be at least {MinLength} characters");
            AddTranslation("en-US", "EqualValidator",
                          "{ComparisonProperty} and {PropertyName} do not match");
            AddTranslation("en-US", "InclusiveBetweenValidator",
                           "{PropertyName} should be between {From} and {To}");
            AddTranslation("en-US", "GreaterThanValidator",
                           "{PropertyName} must be greater than {ComparisonValue}");
            AddTranslation("en-US", "GreaterThanOrEqualValidator",
                           "{PropertyName} must be greater than or equal to {ComparisonValue}");
            AddTranslation("en-US", "ExactLengthValidator",
                           "{PropertyName} must be {MaxLength} characters in length");

            //uk-UA
            AddTranslation("uk-UA", "NotEmptyValidator",
                           "{PropertyName} є обов'язковим");
            AddTranslation("uk-UA", "LengthValidator",
                           "{PropertyName} має бути довжиною від {MinLength} до {MaxLength} символів");
            AddTranslation("uk-UA", "EmailValidator",
                           "Не вірний формат email-адреси");
            AddTranslation("uk-UA", "MinimumLengthValidator",
                           "Довжина {PropertyName} має бути не меншою ніж {MinLength} символів");
            AddTranslation("uk-UA", "MaximumLengthValidator",
                           "Довжина {PropertyName} має бути не більшою ніж {MaxLength} символів");
            AddTranslation("uk-UA", "EqualValidator",
                           "{ComparisonValue} та {PropertyName} не співпадають");
            AddTranslation("uk-UA", "InclusiveBetweenValidator",
                           "{PropertyName} має бути між {From} та {To}");
            AddTranslation("uk-UA", "GreaterThanValidator",
                           "{PropertyName} має бути більшим за {ComparisonValue}");
            AddTranslation("uk-UA", "GreaterThanOrEqualValidator",
                           "{PropertyName} має бути більшим, або дорівнювати {ComparisonValue}");
            AddTranslation("uk-UA", "ExactLengthValidator",
                          "{PropertyName} має бути довжиною {MaxLength} символів");

        }
    }
}
