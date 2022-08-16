using DAL.Constants;
using System.Globalization;

namespace WebAPI.Helpers
{
    public static class CurrentLanguage
    {
        public static int Id => CultureInfo.CurrentCulture.Name == LanguageCulture.English ? LanguageId.English : LanguageId.Ukrainian;
    }
}
