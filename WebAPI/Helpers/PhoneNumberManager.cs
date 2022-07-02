using PhoneNumbers;

namespace WebAPI.Helpers
{
    public class PhoneNumberManager
    {
        private readonly PhoneNumberUtil phoneUtil;
        public PhoneNumberManager()
        {
            phoneUtil = PhoneNumberUtil.GetInstance();
        }
        public bool IsValidNumber(string phoneNumber, string countryCode = null)
        {
            try
            {
                PhoneNumber parsedPhone = phoneUtil.Parse(phoneNumber, countryCode);
                return phoneUtil.IsValidNumber(parsedPhone);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

        public string GetPhoneE164Format(string phoneNumber, string countryCode = null)
        {
            if (!IsValidNumber(phoneNumber))
                return String.Empty;

            PhoneNumber parsedPhone = phoneUtil.Parse(phoneNumber, countryCode);
            return phoneUtil.Format(parsedPhone, PhoneNumberFormat.E164);
        }
    }
}
