using Assets.Model.Characters.Params;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Character.Table
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
