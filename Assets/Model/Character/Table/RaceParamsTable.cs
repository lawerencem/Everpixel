using Assets.Model.Character.Enum;
using Assets.Model.Character.Param.Race;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Character.Table
{
    public class RaceParamsTable : ASingleton<RaceParamsTable>
    {
        public Dictionary<ERace, RaceParams> Table;
        public RaceParamsTable() { Table = new Dictionary<ERace, RaceParams>(); }
    }
}
