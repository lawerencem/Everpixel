using Assets.Model.Map.Combat.Tile;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Environment
{
    public class EnvironmentTable : ASingleton<EnvironmentTable>
    {
        public Dictionary<EEnvironment, EnvironmentParam> Table;
        public EnvironmentTable()
        {
            this.Table = new Dictionary<EEnvironment, EnvironmentParam>();
        }
    }
}
