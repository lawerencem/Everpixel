using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.View.Character.Table
{
    public class CritterDeadSpriteTable : ASingleton<CritterDeadSpriteTable>
    {
        public Dictionary<string, int> Table;
        public CritterDeadSpriteTable()
        {
            Table = new Dictionary<string, int>();
        }
    }
}
