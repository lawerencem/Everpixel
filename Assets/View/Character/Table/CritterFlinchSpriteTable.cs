using Generics;
using System.Collections.Generic;

namespace Assets.View.Character.Table
{
    public class CritterFlinchSpriteTable : AbstractSingleton<CritterFlinchSpriteTable>
    {
        public Dictionary<string, int> Table;
        public CritterFlinchSpriteTable()
        {
            Table = new Dictionary<string, int>();
        }
    }
}
