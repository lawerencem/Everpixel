using Assets.Model.Party.Param;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Party.Table
{
    public class SubPartiesTable : ASingleton<SubPartiesTable>
    {
        public Dictionary<string, List<SubPartyParams>> Table;

        public SubPartiesTable() { Table = new Dictionary<string, List<SubPartyParams>>(); }
    }
}
