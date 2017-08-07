using System.Collections.Generic;
using Template.Other;

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
