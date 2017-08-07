using System.Collections.Generic;
using Template.Other;

namespace Assets.View.Barks
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
