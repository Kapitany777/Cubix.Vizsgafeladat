using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adatbazis.Models
{
    public class Dolgozo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string VezetekNev { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string KeresztNev { get; set; } = string.Empty;
    }
}
