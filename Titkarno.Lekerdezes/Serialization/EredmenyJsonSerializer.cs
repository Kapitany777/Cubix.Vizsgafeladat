using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Titkarno.Lekerdezes.Serialization
{
    /// <summary>
    /// Az eredmények sorosítása JSON fájlba
    /// </summary>
    public class EredmenyJsonSerializer : EredmenySerializer
    {
        public EredmenyJsonSerializer(string fileNev) : base(fileNev)
        {
        }

        public override void Serialize(List<Eredmeny> eredmeny)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(eredmeny, options);
            File.WriteAllText(FileNev, jsonString);
        }
    }
}
