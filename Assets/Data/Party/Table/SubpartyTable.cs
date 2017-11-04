using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class SubpartyTable : ASingleton<SubpartyTable>
    {
        public Dictionary<string, List<SubPartyParams>> Table;

        public SubpartyTable() { Table = new Dictionary<string, List<SubPartyParams>>(); }
    }
}
