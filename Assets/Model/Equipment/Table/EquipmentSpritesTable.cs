using System.Collections.Generic;
using Template.Other;

namespace Model.Equipment.Table
{
    public class EquipmentSpritesTable : ASingleton<EquipmentSpritesTable>
    {
        public Dictionary<string, List<int>> Table;

        public EquipmentSpritesTable() { Table = new Dictionary<string, List<int>>(); }
    }
}
