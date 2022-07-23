﻿namespace WebAPI.ViewModels.Response
{
    public class QuestionResponse
    {
        /// <summary>
        /// FullName 
        /// </summary>
        /// <example>Novak Vova</example>
        public string FullName { get; set; }
        /// <summary>
        /// Email 
        /// </summary>
        /// <example>vova@gmail.com</example>

        public string Email { get; set; }
        /// <summary>
        /// Message 
        /// </summary>
        /// <example>Lorem Ipsum Dolor</example>

        public string Message { get; set; }
        /// <summary>
        /// UserId 
        /// </summary>
        /// <example></example>
        public string UserId { get; set; }

        /// <summary>
        /// Product 
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }
    }
}
