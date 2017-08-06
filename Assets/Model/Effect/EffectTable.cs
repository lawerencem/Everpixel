using Generics;
using System.Collections.Generic;

namespace Assets.Model.Effect
{
    public class EffectTable : AbstractSingleton<EffectTable>
    {
        public Dictionary<EEffect, MEffect> Table;
        public EffectTable()
        {
            Table = new Dictionary<EEffect, MEffect>();
        }
    }
}
