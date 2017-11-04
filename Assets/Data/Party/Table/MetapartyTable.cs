using Assets.Model.Culture;
using Assets.Model.Party.Param;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Party.Table
{
    public class MetapartyTable : ASingleton<MetapartyTable>
    {
        public Dictionary<ECulture, Dictionary<string, MetapartyParams>> Table;

        public MetapartyTable() { Table = new Dictionary<ECulture, Dictionary<string, MetapartyParams>>(); }
    }
}
