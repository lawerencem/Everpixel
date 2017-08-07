using Assets.Model.Characters.Params;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Character.Table
{
    public class PredefinedCharTable : ASingleton<PredefinedCharTable>
    {
        public Dictionary<string, PreCharParams> Table;
        public PredefinedCharTable()
        {
            Table = new Dictionary<string, PreCharParams>();
        }
    }
}
