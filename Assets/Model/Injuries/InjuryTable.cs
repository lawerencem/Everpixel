using Assets.Model.Injuries;
using Generics;
using System.Collections.Generic;

namespace Model.Injuries
{
    public class InjuryTable : AbstractSingleton<InjuryTable>
    {
        public Dictionary<InjuryEnum, GenericInjuryParam> Table;
        public InjuryTable()
        {
            Table = new Dictionary<InjuryEnum, GenericInjuryParam>();
        }
    }
}
