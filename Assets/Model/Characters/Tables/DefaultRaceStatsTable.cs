using Generics;
using System.Collections.Generic;

namespace Model.Characters
{
    public class DefaultRaceStatsTable : AbstractSingleton<DefaultRaceStatsTable>
    {
        public Dictionary<RaceEnum, PrimaryStats> Table;
        public DefaultRaceStatsTable() { Table = new Dictionary<RaceEnum, PrimaryStats>(); }
    }
}
