using Assets.Template.Other;
using System.Collections.Generic;

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
