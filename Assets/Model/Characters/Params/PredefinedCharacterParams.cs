using Assets.Model;
using Model.Classes;
using Model.Mounts;
using System.Collections.Generic;

namespace Model.Characters
{
    public class PredefinedCharacterParams
    {
        public PredefinedCharacterParams()
        {
            this.Armors = new Dictionary<string, List<List<string>>>();
            this.Classes = new Dictionary<ClassEnum, int>();
            this.Helms = new Dictionary<string, List<List<string>>>();
            this.LWeapons = new Dictionary<string, List<List<string>>>();
            this.RWeapons = new Dictionary<string, List<List<string>>>();
            this.Stats = new PrimaryStats();
        }

        public Dictionary<string, List<List<string>>> Armors { get; set; }
        public Dictionary<ClassEnum, int> Classes { get; set; }
        public CultureEnum Culture { get; set; }
        public Dictionary<string, List<List<string>>> Helms { get; set; }
        public Dictionary<string, List<List<string>>> LWeapons { get; set; }
        public MountEnum Mount { get; set; }
        public string Name { get; set; }
        public RaceEnum Race { get; set; }
        public Dictionary<string, List<List<string>>> RWeapons { get; set; }
        public PrimaryStats Stats { get; set; }
        public CharacterTypeEnum Type { get; set; }
        
    }
}
