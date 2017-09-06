using Assets.Template.Other;
using Assets.View.Bark;
using System.Collections.Generic;

namespace Assets.Data.Bark.Table
{
    public class BarkTable : ASingleton<BarkTable>
    {
        public Dictionary<EBark, List<string>> Table;
        public BarkTable()
        {
            Table = new Dictionary<EBark, List<string>>();
        }
    }
}
