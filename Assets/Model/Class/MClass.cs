using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using Model.Perks;
using System.Collections.Generic;

namespace Assets.Model.Class
{
    public class MClass
    {
        private Dictionary<int, List<MPerk>> _perksByLevel { get; set; }
        private Dictionary<EPrimaryStat, int> _primaryStatsPerLevel { get; set; }
        private Dictionary<ESecondaryStat, int> _secondaryStatsPerLevel { get; set; }
        private EClass _type { get; set; }

        public int Level { get; set; }

        public ClassParams GetParams()
        {
            var cParams = this.GetPrimaryStatsByLevel();
            cParams.SecondaryStats = this.GetSecondaryStatsByLevel();
            cParams.Perks = this.GetPerksByLevel();

            return cParams;
        }

        public MClass(ClassParams c, EClass t)
        {
            this._perksByLevel = new Dictionary<int, List<MPerk>>();
            this._primaryStatsPerLevel = c.PrimaryStats;
            this._secondaryStatsPerLevel = c.SecondaryStats;
            this.Level = 0;
            this._type = t;
        }

        protected ClassParams GetPrimaryStatsByLevel()
        {
            var cParams = new ClassParams(); 

            foreach(var kvp in _primaryStatsPerLevel)
                cParams.PrimaryStats.Add(kvp.Key, kvp.Value * this.Level);

            return cParams;
        }

        protected Dictionary<ESecondaryStat, int> GetSecondaryStatsByLevel()
        {
            var c = new Dictionary<ESecondaryStat, int>();

            foreach (var kvp in _secondaryStatsPerLevel)
                c.Add(kvp.Key, kvp.Value * this.Level);

            return c;
        }

        protected List<MPerk> GetPerksByLevel()
        {
            var perks = new List<MPerk>();

            for (int i = 0; i < this.Level; i++)
                if (this._perksByLevel.ContainsKey(i))
                    perks.AddRange(this._perksByLevel[i]);

            return perks;
        }
    }
}
