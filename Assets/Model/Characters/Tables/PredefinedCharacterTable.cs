using Generics;
using System.Collections.Generic;

namespace Model.Characters
{
    public class PredefinedCharacterTable : AbstractSingleton<PredefinedCharacterTable>
    {
        public Dictionary<string, PredefinedCharacterParams> Table;
        public PredefinedCharacterTable()
        {
            Table = new Dictionary<string, PredefinedCharacterParams>();
        }
    }
}
