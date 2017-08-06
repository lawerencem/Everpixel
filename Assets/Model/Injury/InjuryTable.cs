using Generics;
using System.Collections.Generic;

namespace Assets.Model.Injury
{
    public class InjuryTable : AbstractSingleton<InjuryTable>
    {
        public Dictionary<EInjury, MInjuryParam> Table;
        public InjuryTable()
        {
            Table = new Dictionary<EInjury, MInjuryParam>();
        }
    }
}
