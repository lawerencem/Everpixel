using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Party.Table
{
    public class SubPartiesTable : ASingleton<SubPartiesTable>
    {
        public Dictionary<string, List<SubPartyParams>> Table;

        public SubPartiesTable() { Table = new Dictionary<string, List<SubPartyParams>>(); }
    }
}
