using Generics;
using System.Collections.Generic;

namespace Model.Parties
{
    public class SubPartiesTable : AbstractSingleton<SubPartiesTable>
    {
        public Dictionary<string, List<SubPartyParams>> Table;

        public SubPartiesTable() { Table = new Dictionary<string, List<SubPartyParams>>(); }
    }
}
