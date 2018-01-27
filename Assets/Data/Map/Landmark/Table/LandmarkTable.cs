using Assets.Model.Map.Combat.Landmark;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Data.Map.Landmark.Table
{
    public class LandmarkTable : ASingleton<LandmarkTable>
    {
        public Dictionary<ELandmark, LandmarkParams> Table;
        public LandmarkTable()
        {
            this.Table = new Dictionary<ELandmark, LandmarkParams>();
        }
    }
}
