using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace KhumaloCraftLtd.Models
{
    public class Comment
    {

        public int Id { get; set; }
        public string actualComment { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public int? productId { get; set; }
        [ForeignKey("productId")]
        public ProductModel? Listing { get; set; }
    }
}
