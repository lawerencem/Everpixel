using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Equipment.Table
{
    public class EquipmentSpritesTable : ASingleton<EquipmentSpritesTable>
    {
        public Dictionary<string, List<int>> Table;

        public EquipmentSpritesTable() { Table = new Dictionary<string, List<int>>(); }
    }
}
