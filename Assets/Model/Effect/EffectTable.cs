using Assets.Template.Other;
using System.Collections.Generic;

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
