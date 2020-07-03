using backend.Data;
using backend.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Models
{
    using static Validation.Cat;
    public class Cat
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
