using Assets.Model.Party.Param;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Party.Table
{
    public class PartyTable : ASingleton<PartyTable>
    {
        public Dictionary<string, PartyParams> Table;

        public PartyTable() { Table = new Dictionary<string, PartyParams>(); }
    }
}
