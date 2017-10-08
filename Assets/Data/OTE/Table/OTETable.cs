using Assets.Model.OTE;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.OTE.Table
{
    public class OTETable : ASingleton<OTETable>
    {
        public Dictionary<EOTE, OTEParams> Table;
        public OTETable()
        {
            Table = new Dictionary<EOTE, OTEParams>();
        }
    }
}
