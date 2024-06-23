using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KhumaloCraftLtd.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int quantity { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? productId { get; set; }
        [ForeignKey("productId")]
        public ProductModel? Listing { get; set; }
    }
}
