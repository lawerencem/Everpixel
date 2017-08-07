using Generics;
using System.Collections.Generic;

namespace Assets.View.Barks
{
    public class BarkTable : AbstractSingleton<BarkTable>
    {
        public Dictionary<EBark, List<string>> Table;
        public BarkTable()
        {
            Table = new Dictionary<EBark, List<string>>();
        }
    }
}
