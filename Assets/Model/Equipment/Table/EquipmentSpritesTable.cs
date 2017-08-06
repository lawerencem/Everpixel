using Generics;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class EquipmentSpritesTable : AbstractSingleton<EquipmentSpritesTable>
    {
        public Dictionary<string, List<int>> Table;

        public EquipmentSpritesTable() { Table = new Dictionary<string, List<int>>(); }
    }
}
