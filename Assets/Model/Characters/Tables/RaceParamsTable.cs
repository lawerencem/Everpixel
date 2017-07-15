using Assets.Model.Characters.Params;
using Generics;
using System.Collections.Generic;

namespace Model.Characters
{
    public class RaceParamsTable : AbstractSingleton<RaceParamsTable>
    {
        public Dictionary<RaceEnum, RaceParams> Table;
        public RaceParamsTable() { Table = new Dictionary<RaceEnum, RaceParams>(); }
    }
}
