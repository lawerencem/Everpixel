﻿using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using Assets.Model.Culture;
using Assets.Model.Mount;
using Assets.Model.Perk;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Characters.Params
{
    public class PreCharParams
    {
        public PreCharParams()
        {
            this.ActiveAbilities = new List<EAbility>();
            this.Armors = new Dictionary<string, List<List<string>>>();
            this.Classes = new Dictionary<EClass, int>();
            this.DefaultWpnAbilities = new List<EAbility>();
            this.Helms = new Dictionary<string, List<List<string>>>();
            this.LWeapons = new Dictionary<string, List<List<string>>>();
            this.Perks = new List<EPerk>();
            this.RWeapons = new Dictionary<string, List<List<string>>>();
            this.Spells = new List<Pair<int, EAbility>>();
            this.Stats = new PrimaryStats();
        }

        public Dictionary<string, List<List<string>>> Armors { get; set; }
        public List<EAbility> ActiveAbilities { get; set; }
        public Dictionary<EClass, int> Classes { get; set; }
        public ECulture Culture { get; set; }
        public List<EAbility>  DefaultWpnAbilities { get; set; }
        public Dictionary<string, List<List<string>>> Helms { get; set; }
        public Dictionary<string, List<List<string>>> LWeapons { get; set; }
        public EMount Mount { get; set; }
        public string Name { get; set; }
        public List<EPerk> Perks { get; set; }
        public ERace Race { get; set; }
        public Dictionary<string, List<List<string>>> RWeapons { get; set; }
        public List<Pair<int, EAbility>> Spells { get; set; }
        public PrimaryStats Stats { get; set; }
        public ECharType Type { get; set; }
    }
}