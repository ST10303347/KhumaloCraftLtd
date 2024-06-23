using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KhumaloCraftLtd.Models
{
    public class ProductModel
    {

        [Key]
        public int Id { get; set; }
        public string productName { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }

        public int Availability { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        //public List<Comment>? Comments { get; set; }
    }
}
