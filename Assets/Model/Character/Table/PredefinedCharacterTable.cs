using Assets.Model.Characters.Params;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Character.Table
{
    public class PredefinedCharacterTable : AbstractSingleton<PredefinedCharacterTable>
    {
        public Dictionary<string, PreCharParams> Table;
        public PredefinedCharacterTable()
        {
            Table = new Dictionary<string, PreCharParams>();
        }
    }
}
