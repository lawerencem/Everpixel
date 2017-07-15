using Generics;
using System.Collections.Generic;

namespace Model.Effects
{
    public class EffectsTable : AbstractSingleton<EffectsTable>
    {
        public Dictionary<EffectsEnum, GenericEffect> Table;
        public EffectsTable()
        {
            Table = new Dictionary<EffectsEnum, GenericEffect>();
        }
    }
}
