using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Class.Table
{
    public class ClassParamTable : ASingleton<ClassParamTable>
    {
        public Dictionary<EClass, ClassParams> Table;
        public ClassParamTable() { Table = new Dictionary<EClass, ClassParams>(); }
    }
}
