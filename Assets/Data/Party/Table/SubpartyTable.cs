using Assets.Model.Culture;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class SubpartyTable : ASingleton<SubpartyTable>
    {
        public Dictionary<ECulture, Dictionary<string, List<SubPartyParam>>> Table;

        public SubpartyTable() { Table = new Dictionary<ECulture, Dictionary<string, List<SubPartyParam>>>(); }
    }
}
