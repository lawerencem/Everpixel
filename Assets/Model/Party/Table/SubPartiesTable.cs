using Assets.Model.Party.Param;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Party.Table
{
    public class SubPartiesTable : AbstractSingleton<SubPartiesTable>
    {
        public Dictionary<string, List<SubPartyParams>> Table;

        public SubPartiesTable() { Table = new Dictionary<string, List<SubPartyParams>>(); }
    }
}
