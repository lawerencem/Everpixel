using Assets.Model.Mount;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Mount.Table
{
    public class MountsTable : ASingleton<MountsTable>
    {
        public Dictionary<EMount, MountParams> Table;

        public MountsTable() { Table = new Dictionary<EMount, MountParams>(); }
    }
}
