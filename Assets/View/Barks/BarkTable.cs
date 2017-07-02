using Generics;
using System.Collections.Generic;

namespace View.Barks
{
    public class BarkTable : AbstractSingleton<BarkTable>
    {
        public Dictionary<BarkCategoryEnum, List<string>> Table;
        public BarkTable()
        {
            Table = new Dictionary<BarkCategoryEnum, List<string>>();
        }
    }
}
