using Generics;
using System.Collections.Generic;

namespace Model.Parties
{
    public class PartiesTable : AbstractSingleton<PartiesTable>
    {
        public Dictionary<string, PartyParams> Table;

        public PartiesTable() { Table = new Dictionary<string, PartyParams>(); }
    }
}
