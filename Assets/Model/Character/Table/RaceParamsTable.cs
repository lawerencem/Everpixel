using Assets.Model.Character.Enum;
using Assets.Model.Character.Param.Race;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Character.Table
{
    public class RaceParamsTable : AbstractSingleton<RaceParamsTable>
    {
        public Dictionary<ERace, RaceParams> Table;
        public RaceParamsTable() { Table = new Dictionary<ERace, RaceParams>(); }
    }
}
