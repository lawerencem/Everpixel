using Generics;
using System.Collections.Generic;

namespace Model.Mounts
{
    public class MountsTable : AbstractSingleton<MountsTable>
    {
        public Dictionary<MountEnum, MountParams> Table;

        public MountsTable() { Table = new Dictionary<MountEnum, MountParams>(); }
    }
}
