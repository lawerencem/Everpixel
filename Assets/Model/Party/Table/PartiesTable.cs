using Assets.Model.Party.Param;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Party.Table
{
    public class PartyTable : AbstractSingleton<PartyTable>
    {
        public Dictionary<string, PartyParams> Table;

        public PartyTable() { Table = new Dictionary<string, PartyParams>(); }
    }
}
