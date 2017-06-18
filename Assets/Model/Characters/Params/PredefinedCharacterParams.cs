using Assets.Generics;
using Assets.Model;
using Model.Abilities;
using Model.Classes;
using Model.Mounts;
using Model.Perks;
using System.Collections.Generic;

namespace Model.Characters
{
    public class PredefinedCharacterParams
    {
        public PredefinedCharacterParams()
        {
            this.ActiveAbilities = new List<ActiveAbilitiesEnum>();
            this.Armors = new Dictionary<string, List<List<string>>>();
            this.Classes = new Dictionary<ClassEnum, int>();
            this.DefaultWpnAbilities = new List<WeaponAbilitiesEnum>();
            this.Helms = new Dictionary<string, List<List<string>>>();
            this.LWeapons = new Dictionary<string, List<List<string>>>();
            this.Perks = new List<PerkEnum>();
            this.RWeapons = new Dictionary<string, List<List<string>>>();
            this.Spells = new List<Pair<int, ActiveAbilitiesEnum>>();
            this.Stats = new PrimaryStats();
        }

        public Dictionary<string, List<List<string>>> Armors { get; set; }
        public List<ActiveAbilitiesEnum> ActiveAbilities { get; set; }
        public Dictionary<ClassEnum, int> Classes { get; set; }
        public CultureEnum Culture { get; set; }
        public List<WeaponAbilitiesEnum>  DefaultWpnAbilities { get; set; }
        public Dictionary<string, List<List<string>>> Helms { get; set; }
        public Dictionary<string, List<List<string>>> LWeapons { get; set; }
        public MountEnum Mount { get; set; }
        public string Name { get; set; }
        public List<PerkEnum> Perks { get; set; }
        public RaceEnum Race { get; set; }
        public Dictionary<string, List<List<string>>> RWeapons { get; set; }
        public List<Pair<int, ActiveAbilitiesEnum>> Spells { get; set; }
        public PrimaryStats Stats { get; set; }
        public CharacterTypeEnum Type { get; set; }
    }
}
