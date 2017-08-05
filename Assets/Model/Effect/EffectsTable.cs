using Generics;
using System.Collections.Generic;

namespace Model.Effects
{
    public class EffectsTable : AbstractSingleton<EffectsTable>
    {
        public Dictionary<EnumEffect, Effect> Table;
        public EffectsTable()
        {
            Table = new Dictionary<EnumEffect, Effect>();
        }
    }
}
