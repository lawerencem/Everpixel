using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Injury
{
    public class InjuryTable : ASingleton<InjuryTable>
    {
        public Dictionary<EInjury, MInjuryParam> Table;
        public InjuryTable()
        {
            Table = new Dictionary<EInjury, MInjuryParam>();
        }
    }
}
