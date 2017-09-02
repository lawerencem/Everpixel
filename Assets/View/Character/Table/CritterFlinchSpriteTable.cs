using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.View.Character.Table
{
    public class CritterFlinchSpriteTable : ASingleton<CritterFlinchSpriteTable>
    {
        public Dictionary<string, int> Table;
        public CritterFlinchSpriteTable()
        {
            Table = new Dictionary<string, int>();
        }
    }
}
