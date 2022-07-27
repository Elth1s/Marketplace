namespace WebAPI.ViewModels.Response.Users
{
    /// <summary>
    /// User class returned from the controller
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// User identifier
        /// </summary>
        /// <example>94ddd073</example>
        public string Id { get; set; }
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
    }
}

