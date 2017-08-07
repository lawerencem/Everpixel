using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Mount
{
    public class MountsTable : ASingleton<MountsTable>
    {
        public Dictionary<EMount, MountParams> Table;

        public MountsTable() { Table = new Dictionary<EMount, MountParams>(); }
    }
}
