using Generics;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CritterAttackSpriteTable : AbstractSingleton<CritterAttackSpriteTable>
    {
        public Dictionary<string, int> Table;
        public CritterAttackSpriteTable()
        {
            Table = new Dictionary<string, int>();
        }
    }
}
