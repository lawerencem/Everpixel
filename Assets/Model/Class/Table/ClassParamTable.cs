using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using Generics;
using System.Collections.Generic;

namespace Assets.Model.Class.Table
{
    public class ClassParamTable : AbstractSingleton<ClassParamTable>
    {
        public Dictionary<EClass, ClassParams> Table;
        public ClassParamTable() { Table = new Dictionary<EClass, ClassParams>(); }
    }
}
