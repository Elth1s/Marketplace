namespace WebAPI.ViewModels.Response.Categories
{
    /// <summary>
    ///  Category for select class returned from the controller
    /// </summary>
    public class CategoryForSelectResponse
    {
        /// <summary>
        /// Category identifier
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Name of category
        /// </summary>
        /// <example>Computer equipment and software</example>
        public string Name { get; set; }
    }
}
