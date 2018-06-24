using Assets.Data.Character.Table;
using Assets.Data.Character.XML;
using Assets.Data.Equipment.Table;
using Assets.Data.Mount.Table;
using Assets.Model.Ability;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Characters.Params;
using Assets.Model.Class.Builder;
using Assets.Model.Equipment.Armor;
using Assets.Model.Equipment.Factory;
using Assets.Model.Equipment.Table;
using Assets.Model.Equipment.Weapon;
using Assets.Model.Mount;
using Assets.Model.Perk;
using Assets.Model.Weapon;
using Assets.Template.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Character.Builder
{
    public class CharBuilder
    {
        private const string SINGULAR = "One";

        public MChar Build(PreCharParams arg)
        {
            return BuildHelper(arg);
        }

        private PStats GetRaceStats(PreCharParams c)
        {
            if (RaceParamsTable.Instance.Table.ContainsKey(c.Race))
                return RaceParamsTable.Instance.Table[c.Race].PrimaryStats.Clone();
            else
                return null;
        }

        private SStats GetSecondaryStats(PStats p)
        {
            return new SStats(p);
        }

        private MChar BuildHelper(PreCharParams preCharParams)
        {
            var character = new MChar(preCharParams.Race);
            if (preCharParams.Race != ERace.Critter && preCharParams.Race != ERace.None)
            {
                var racialPerks = RaceParamsTable.Instance.Table[preCharParams.Race].DefaultPerks;
                PerkMediator.Instance.SetCharacterPerks(character, racialPerks);
            }
            PerkMediator.Instance.SetCharacterPerks(character, preCharParams.Perks);
            this.BuildBaseClassHelper(preCharParams, character);
            this.BuildAbilities(preCharParams, character);
            this.BuildCharStats(preCharParams, character);
            this.BuildClassPrimaryStats(character);
            var secondary = GetSecondaryStats(character.GetBaseStats().GetPrimaryStats());
            character.GetBaseStats().SetSecondaryStats(secondary);
            this.BuildClassSecondaryStats(character);
            this.BuildCurStats(character);
            this.BuildCurPoints(character);
            character.SetType(preCharParams.Type);
            character.SetParams(preCharParams);
            this.BuildCharMount(preCharParams, character);
            character.SetAIRole(preCharParams.AIRole);
            return character;
        }

        private void BuildBaseClassHelper(PreCharParams p, MChar c)
        {
            var builder = new ClassBuilder();

            foreach (var kvp in p.Classes)
            {
                var toAdd = builder.Build(kvp.Key);
                toAdd.Level = kvp.Value;
                c.GetBaseClasses().Add(kvp.Key, toAdd);
            }
        }

        private void BuildAbilities(PreCharParams p, MChar c)
        {
            foreach(var kvp in p.AbilityEffectDict)
            {
                var ability = AbilityFactory.Instance.CreateNewObject(kvp.Key);
                ability.Data.Effects.AddRange(kvp.Value);
                c.GetAbilitiesContainer().AddAbility(ability);
            }
            var wpnAbs = WeaponAbilityFactory.Instance.CreateNewObject(p.WpnAbilities);
            foreach (var v in wpnAbs)
            {
                c.GetAbilitiesContainer().AddAbility(v);
            }
        }

        private void BuildCharMount(PreCharParams preCharParams, MChar character)
        {
            if (preCharParams.Type == Enum.ECharType.Humanoid)
            {
                var builder = new MountBuilder();
                this.BuildArmorHelper(character, preCharParams);
                this.BuildWeaponHelper(character, preCharParams);
                var mountParams = this.GetMountParams(preCharParams);
                if (mountParams != null)
                    character.SetMount(builder.Build(mountParams));
            }
        }

        private void BuildCharStats(PreCharParams preCharParams, MChar character)
        {
            var raceStats = this.GetRaceStats(preCharParams);
            if (raceStats == null)
            {
                var specificStats = PredefinedCharTable.Instance.Table[preCharParams.Name];
                character.GetBaseStats().SetPrimaryStats(specificStats.Stats);
            }
            else
            {
                character.GetBaseStats().SetPrimaryStats(raceStats);
            }
        }

        private void BuildClassPrimaryStats(MChar c)
        {
            foreach (var kvp in c.GetBaseClasses())
            {
                var classStats = kvp.Value.GetParams();
                foreach (var stat in classStats.PrimaryStats)
                    c.GetBaseStats().AddStat(stat.Key, stat.Value);
            }
        }

        private void BuildClassSecondaryStats(MChar c)
        {
            foreach (var kvp in c.GetBaseClasses())
            {
                var stats = kvp.Value.GetParams();
                foreach (var stat in stats.SecondaryStats)
                    c.GetBaseStats().AddStat(stat.Key, stat.Value);
            }
        }

        private void BuildCurStats(MChar c)
        {
            c.GetCurStats().Init(c.GetBaseStats());
        }

        private void BuildCurPoints(MChar c)
        {
            c.GetPoints().Init(c.GetCurStats());
        }

        private void BuildWeaponHelper(MChar c, PreCharParams p)
        {
            var rWpnParams = this.GetWpnParams(p, p.RWeapons);
            var lWpnParams = this.GetWpnParams(p, p.LWeapons);
            if (rWpnParams != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(
                    rWpnParams.Name, 
                    rWpnParams.Tier);
                c.GetEquipment().AddWeapon(weapon, false);
            }
            if (lWpnParams != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(
                    lWpnParams.Name,
                    lWpnParams.Tier);
                c.GetEquipment().AddWeapon(weapon, true);
            }
        }

        private void BuildArmorHelper(MChar c, PreCharParams p)
        {
            var armorParams = this.GetArmorParams(p);
            if (armorParams != null)
            {
                var armor = ArmorFactory.Instance.CreateNewObject(
                    armorParams.Name, 
                    armorParams.Tier);
                c.GetEquipment().AddArmor(armor);
            }
            var helmParams = this.GetHelmParams(p);
            if (helmParams != null)
            {
                var helm = HelmFactory.Instance.CreateNewObject(
                    helmParams.Name,
                    helmParams.Tier);
                c.GetEquipment().AddHelm(helm);
            }
        }

        private ArmorParams GetArmorParams(PreCharParams arg)
        {
            var csv = new List<string>();
            if (arg.Armors.ContainsKey(SINGULAR))
                csv = GetEquipmentCSV(arg.Armors[SINGULAR]);
            if (csv != null && csv.Count >= 3)
            {
                try
                {
                    var key = csv[PredefinedEquipmentXMLIndexes.NAME] + "_" + csv[PredefinedEquipmentXMLIndexes.TIER];
                    var armor = ArmorParamTable.Instance.Table[key];
                    armor.Sprites = EquipmentSpritesTable.Instance.Table[key];
                    return armor;
                }
                catch(KeyNotFoundException e)
                {
                    var key = csv[PredefinedEquipmentXMLIndexes.NAME] + "_" + csv[PredefinedEquipmentXMLIndexes.TIER];
                    Debug.LogError(key + " was not found");
                    Debug.LogError(e.Message);
                    return null;
                }
            }
            else
                return null;
        }

        private List<string> GetEquipmentCSV(List<List<string>> potentials)
        {
            double tally = 0;
            double chance = RNG.Instance.NextDouble();
            for (int i = 0; i < potentials.Count; i++)
            {
                tally += Double.Parse(potentials[i][PredefinedEquipmentXMLIndexes.CHANCE]);
                if (tally >= chance)
                    return potentials[i];
            }
            return null;
        }

        private ArmorParams GetHelmParams(PreCharParams arg)
        {
            var csv = new List<string>();
            if (arg.Helms.ContainsKey(SINGULAR))
                csv = GetEquipmentCSV(arg.Helms[SINGULAR]);
            if (csv != null && csv.Count >= 3)
            {
                var key = csv[PredefinedEquipmentXMLIndexes.NAME] + "_" + csv[PredefinedEquipmentXMLIndexes.TIER];
                var helm = ArmorParamTable.Instance.Table[key];
                helm.Sprites = EquipmentSpritesTable.Instance.Table[key];
                return helm;
            }
            else
                return null;
        }

        private MountParams GetMountParams(PreCharParams arg)
        {
            if (arg.Mount != EMount.None)
            {
                var mount = new MountParams();
                var mParams = MountsTable.Instance.Table[arg.Mount];
                mount.Type = arg.Mount;
                return mount;
            }
            else
                return null;
        }

        private WeaponParams GetWpnParams(PreCharParams arg, Dictionary<string, List<List<string>>> table)
        {
            var csv = new List<string>();
            if (table.ContainsKey(SINGULAR))
                csv = GetEquipmentCSV(table[SINGULAR]);
            if (csv != null && csv.Count >= 3)
            {
                var key = csv[PredefinedEquipmentXMLIndexes.NAME] + "_" + csv[PredefinedEquipmentXMLIndexes.TIER];
                if (WeaponParamTable.Instance.Table.ContainsKey(key))
                {
                    var weapon = WeaponParamTable.Instance.Table[key];
                    weapon.Sprites = EquipmentSpritesTable.Instance.Table[key];
                    return weapon;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }
    }
}
