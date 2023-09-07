using System.Collections.Generic;

namespace Titkarno.Lekerdezes.Serialization
{
    /// <summary>
    /// A sorosító osztályok absztrakt ősosztálya
    /// </summary>
    public abstract class EredmenySerializer
    {
        public string FileNev { get; }

        public EredmenySerializer(string fileNev)
        {
            FileNev = fileNev;
        }

        public abstract void Serialize(List<Eredmeny> eredmeny);
    }
}
