using Assets.Model.Culture;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class ArmyTable : ASingleton<ArmyTable>
    {
        public Dictionary<ECulture, Dictionary<string, ArmyParams>> Table;

        public ArmyTable() { Table = new Dictionary<ECulture, Dictionary<string, ArmyParams>>(); }
    }
}
