using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [ForeignKey("Schooling")]
        public int SchoolingId { get; set; }

        public Schooling? Schooling { get; set; }

        [Column(TypeName = "int")]
        public int HistoricSchooling { get; set; }
    }
}
