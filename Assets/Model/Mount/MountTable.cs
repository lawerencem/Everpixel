using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Mount
{
    public class MountsTable : ASingleton<MountsTable>
    {
        public Dictionary<EMount, MountParams> Table;

        public MountsTable() { Table = new Dictionary<EMount, MountParams>(); }
    }
}
