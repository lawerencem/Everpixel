using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Effect
{
    public class EffectTable : ASingleton<EffectTable>
    {
        public Dictionary<EEffect, MEffect> Table;
        public EffectTable()
        {
            Table = new Dictionary<EEffect, MEffect>();
        }
    }
}
