using System;

namespace Titkarno.Lekerdezes.Serialization
{
    /// <summary>
    /// Adatosztály az eredmények mentéséhez
    /// </summary>
    [Serializable]
    public class Eredmeny
    {
        public int Id { get; set; }

        public string Iranyitoszam { get; set; } = string.Empty;

        public string Varos { get; set; } = string.Empty;

        public string Cim { get; set; } = string.Empty;

        public string HibaLeiras { get; set; } = string.Empty;

        public DateTime BejelentesDatuma { get; set; }

        public DateTime? JavitasDatuma { get; set; }

        public int? DolgozoId { get; set; }

        public string DolgozoNev { get; set; } = string.Empty;

        public int? JavitasTipusId { get; set; }

        public string? TipusNev { get; set; } = string.Empty;
    }
}
