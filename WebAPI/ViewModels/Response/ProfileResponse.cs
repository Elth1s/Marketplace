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
        /// User name
        /// </summary>
        /// <example>someUserName</example>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>https://some_user_photo_example_url.com</example>
        public string Photo { get; set; }
        /// <summary>
        /// User email address
        /// </summary>
        /// <example>email@gmail.com</example>
        public string Email { get; set; }
    }
}
