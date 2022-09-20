using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Schooling
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }
    }
}
