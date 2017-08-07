using Assets.Model.Characters.Params;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Character.Table
{
    public class PredefinedCharTable : AbstractSingleton<PredefinedCharTable>
    {
        public Dictionary<string, PreCharParams> Table;
        public PredefinedCharTable()
        {
            Table = new Dictionary<string, PreCharParams>();
        }
    }
}
