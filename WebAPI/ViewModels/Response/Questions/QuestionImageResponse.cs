namespace WebAPI.ViewModels.Response.Questions
{
    /// <summary>
    /// Question image class returned from the controller
    /// </summary>
    public class QuestionImageResponse
    {
        /// <summary>
        /// Question image identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of question image
        /// </summary>
        /// <example>https://some_question_image_example.jpg</example>
        public string Name { get; set; }
    }
}
