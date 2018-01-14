using Assets.Model.Culture;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class PartyTable : ASingleton<PartyTable>
    {
        public Dictionary<ECulture, Dictionary<string, List<Pair<string, double>>>> Table;

        public PartyTable() { Table = new Dictionary<ECulture, Dictionary<string, List<Pair<string, double>>>>(); }
    }
}
