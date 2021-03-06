﻿using Assets.Model.Ability.Enum;
using Assets.Model.AI.Agent;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Class.Enum;
using Assets.Model.Culture;
using Assets.Model.Effect;
using Assets.Model.Mount;
using Assets.Model.Perk;
using System.Collections.Generic;

namespace Assets.Model.Characters.Params
{
    public class PreCharParams
    {
        public EAgentRole AIRole { get; set; }
        public Dictionary<string, List<List<string>>> Armors { get; set; }
        public Dictionary<EClass, int> Classes { get; set; }
        public ECulture Culture { get; set; }
        public Dictionary<EAbility, List<MEffect>> AbilityEffectDict { get; set; }
        public List<EAbility>  WpnAbilities { get; set; }
        public Dictionary<string, List<List<string>>> Helms { get; set; }
        public Dictionary<string, List<List<string>>> LWeapons { get; set; }
        public EMount Mount { get; set; }
        public string Name { get; set; }
        public List<EPerk> Perks { get; set; }
        public ERace Race { get; set; }
        public Dictionary<string, List<List<string>>> RWeapons { get; set; }
        public PStats Stats { get; set; }
        public ECharType Type { get; set; }

        public PreCharParams()
        {
            this.Armors = new Dictionary<string, List<List<string>>>();
            this.Classes = new Dictionary<EClass, int>();
            this.AbilityEffectDict = new Dictionary<EAbility, List<MEffect>>();
            this.WpnAbilities = new List<EAbility>();
            this.Helms = new Dictionary<string, List<List<string>>>();
            this.LWeapons = new Dictionary<string, List<List<string>>>();
            this.Perks = new List<EPerk>();
            this.RWeapons = new Dictionary<string, List<List<string>>>();
            this.Stats = new PStats();
        }
    }
}
