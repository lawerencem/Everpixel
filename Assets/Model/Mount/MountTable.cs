using Generics;
using System.Collections.Generic;

namespace Assets.Model.Mount
{
    public class MountsTable : AbstractSingleton<MountsTable>
    {
        public Dictionary<EMount, MountParams> Table;

        public MountsTable() { Table = new Dictionary<EMount, MountParams>(); }
    }
}
