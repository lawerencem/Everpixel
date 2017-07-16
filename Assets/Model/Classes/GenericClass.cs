using Model.Characters;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Classes
{
    public class GenericClass
    {
        private Dictionary<int, List<GenericPerk>> _perksByLevel { get; set; }
        private Dictionary<PrimaryStatsEnum, int> _primaryStatsPerLevel { get; set; }
        private Dictionary<SecondaryStatsEnum, int> _secondaryStatsPerLevel { get; set; }
        private ClassEnum _type { get; set; }

        public int Level { get; set; }

        public ClassParams GetParams()
        {
            var cParams = this.GetPrimaryStatsByLevel();
            cParams.SecondaryStats = this.GetSecondaryStatsByLevel();
            cParams.Perks = this.GetPerksByLevel();

            return cParams;
        }

        public GenericClass(ClassParams c, ClassEnum t)
        {
            this._perksByLevel = new Dictionary<int, List<GenericPerk>>();
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

        protected Dictionary<SecondaryStatsEnum, int> GetSecondaryStatsByLevel()
        {
            var c = new Dictionary<SecondaryStatsEnum, int>();

            foreach (var kvp in _secondaryStatsPerLevel)
                c.Add(kvp.Key, kvp.Value * this.Level);

            return c;
        }

        protected List<GenericPerk> GetPerksByLevel()
        {
            var perks = new List<GenericPerk>();

            for (int i = 0; i < this.Level; i++)
                if (this._perksByLevel.ContainsKey(i))
                    perks.AddRange(this._perksByLevel[i]);

            return perks;
        }
    }
}
