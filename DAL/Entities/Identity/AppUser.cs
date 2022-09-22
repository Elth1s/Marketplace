using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class AppUser : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string SecondName { get; set; }
        [StringLength(100)]
        public string Photo { get; set; }

        public bool IsDeleted { get; set; }

        public string Address { get; set; }
        public string PostalCode { get; set; }

        public int? ShopId { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? GenderId { get; set; }

        [ForeignKey(nameof(ShopId))]
        public Shop Shop { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; }

        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }

        public ICollection<CharacteristicGroup> CharacteristicGroups { get; set; }
        public ICollection<CharacteristicName> CharacteristicNames { get; set; }
        public ICollection<CharacteristicValue> CharacteristicValues { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<QuestionVotes> QuestionVotes { get; set; }
        public ICollection<QuestionReply> QuestionReplies { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<ReviewVotes> ReviewVotes { get; set; }
        public ICollection<ReviewReply> ReviewReplies { get; set; }

        public ICollection<ShopReview> ShopReviews { get; set; }


        public ICollection<Product> ReviewedProducts { get; set; }
        public ICollection<Product> SelectedProducts { get; set; }
        public ICollection<Product> ComparisonProducts { get; set; }
    }
}
