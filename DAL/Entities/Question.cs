using DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Question : BaseEntity, IAggregateRoot
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

    }
}
