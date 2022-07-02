namespace WebAPI.ViewModels.Response
{
    /// <summary>
    /// Profile class returned from the controller
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>
        /// User first name
        /// </summary>
        /// <example>Nick</example>
        public string FirstName { get; set; }
        /// <summary>
        /// User second name
        /// </summary>
        /// <example>Smith</example>
        public string SecondName { get; set; }
        /// <summary>
        /// User phone number
        /// </summary>
        /// <example>+380 50 638 8216</example>
        public string Phone { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
        /// <summary>
        /// User photo
        /// </summary>
        /// <example>https://some_user_photo_example_url.com</example>
        public string Photo { get; set; }

        /// <summary>
        /// Is user email confirmed
        /// </summary>
        /// <example>true</example>
        public bool IsEmailConfirmed { get; set; }
        /// <summary>
        /// Is user phone number confirmed
        /// </summary>
        /// <example>false</example>
        public bool IsPhoneConfirmed { get; set; }
        /// <summary>
        /// Does user have password
        /// </summary>
        /// <example>true</example>
        public bool HasPassword { get; set; }
    }
}
