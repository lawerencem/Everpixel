using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Class.Table
{
    public class ClassParamTable : ASingleton<ClassParamTable>
    {
        public Dictionary<EClass, ClassParams> Table;
        public ClassParamTable() { Table = new Dictionary<EClass, ClassParams>(); }
    }
}
