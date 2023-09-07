using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Titkarno.Lekerdezes.Serialization
{
    /// <summary>
    /// Az eredmények sorosítása XML fájlba
    /// </summary>
    public class EredmenyXmlSerializer : EredmenySerializer
    {
        public EredmenyXmlSerializer(string fileNev) : base(fileNev)
        {
        }

        public override void Serialize(List<Eredmeny> eredmeny)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Eredmeny>));

            using (var stream = new StreamWriter(FileNev))
            {
                xmlSerializer.Serialize(stream, eredmeny);
            }
        }
    }
}
