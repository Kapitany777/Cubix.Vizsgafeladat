using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adatbazis.Models
{
    public class Bejelentes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(4)]
        public string Iranyitoszam { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Varos { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Cim { get; set; } = string.Empty;

        [Required]
        public DateTime BejelentesDatuma { get; set; }

        public DateTime? JavitasDatuma { get; set; }

        public int? DolgozoId { get; set; }

        public Dolgozo? Dolgozo { get; set; }

        public int? JavitasTipusId { get; set; }

        public JavitasTipus? JavitasTipus { get; set; }
    }
}
