using Generics;
using System.Collections.Generic;

namespace Model.Classes
{
    public class ClassParamTable : AbstractSingleton<ClassParamTable>
    {
        public Dictionary<ClassEnum, ClassParams> Table;
        public ClassParamTable() { Table = new Dictionary<ClassEnum, ClassParams>(); }
    }
}
