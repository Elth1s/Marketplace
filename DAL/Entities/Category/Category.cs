using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string UrlSlug { get; set; }
        public string Image { get; set; }

        public string LightIcon { get; set; }
        public string DarkIcon { get; set; }
        public string ActiveIcon { get; set; }


        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<FilterValue> FilterValues { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
    }
}
