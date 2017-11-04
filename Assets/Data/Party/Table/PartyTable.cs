using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class PartyTable : ASingleton<PartyTable>
    {
        public Dictionary<string, PartyParams> Table;

        public PartyTable() { Table = new Dictionary<string, PartyParams>(); }
    }
}
