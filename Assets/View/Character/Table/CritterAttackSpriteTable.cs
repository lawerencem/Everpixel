using System.Collections.Generic;
using Template.Other;

namespace Assets.View.Character.Table
{
    public class CritterAttackSpriteTable : ASingleton<CritterAttackSpriteTable>
    {
        public Dictionary<string, int> Table;
        public CritterAttackSpriteTable()
        {
            Table = new Dictionary<string, int>();
        }
    }
}
