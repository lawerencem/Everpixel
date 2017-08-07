using Generics;
using System.Collections.Generic;

namespace Assets.View.Character.Table
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
