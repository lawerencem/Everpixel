using Generics;
using System.Collections.Generic;

namespace Model.Characters
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
