using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Index(nameof(ShortenedUrl), IsUnique = true)]
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }

        [Required]
        public string RealUrl { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")] 
        public virtual User User{ get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
