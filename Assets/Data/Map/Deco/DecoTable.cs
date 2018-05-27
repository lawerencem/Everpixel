using Assets.Model.Map.Deco;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Environment
{
    public class DecoTable : ASingleton<DecoTable>
    {
        public Dictionary<EDeco, decoParam> Table;
        public DecoTable()
        {
            this.Table = new Dictionary<EDeco, decoParam>();
        }
    }
}
