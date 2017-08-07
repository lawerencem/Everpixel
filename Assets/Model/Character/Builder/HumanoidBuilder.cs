using Assets.Model.Ability;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Character.Table;
using Assets.Model.Class.Builder;
using Assets.Model.Equipment.Factory;
using Assets.Model.Perk;
using Assets.Model.Weapon;
using System;
using System.Collections.Generic;
using Template.Builder;

namespace Assets.Model.Character.Builder
{
    public class HumanoidBuilder : GBuilder<CharParams, MChar>
    {
        public override MChar Build()
        {
            throw new NotImplementedException();
        }

        public override MChar Build(List<CharParams> args)
        {
            throw new NotImplementedException();
        }

        public override MChar Build(CharParams arg)
        {
            return BuildHelper(arg);
        }

        private PrimaryStats GetRaceStats(CharParams c)
        {
            if (RaceParamsTable.Instance.Table.ContainsKey(c.Race))
                return RaceParamsTable.Instance.Table[c.Race].PrimaryStats.Clone();
            else
                return null;
        }

        private SecondaryStats GetSecondaryStats(PrimaryStats p)
        {
            return new SecondaryStats(p);
        }

        private MChar BuildHelper(CharParams c)
        {
            var character = new MChar(c.Race);
            var primary = GetRaceStats(c);
            if (primary != null)
            {
                BuildBaseClassHelper(c, character);
                BuildDefaultAbilities(c, character);
                PerkMediator.Instance.SetCharacterPerks(character, c.Perks);
                character.PrimaryStats = primary;
                BuildClassPrimaryStats(character);
                var secondary = GetSecondaryStats(primary);
                character.SecondaryStats = secondary;
                BuildClassSecondaryStats(character);
                character.Type = c.Type;
                this.BuildArmorHelper(character, c);
                this.BuildWeaponHelper(character, c);
                character.SetCurrentAP((int)character.GetCurrentStatValue(ESecondaryStat.AP));
                character.SetCurrentHP((int)character.GetCurrentStatValue(ESecondaryStat.HP));
                character.SetCurrentMorale((int)character.GetCurrentStatValue(ESecondaryStat.Morale));
                character.SetCurrentStam((int)character.GetCurrentStatValue(ESecondaryStat.Stamina));
                return character;
            }
            else
                return null;
        }

        private void BuildBaseClassHelper(CharParams p, MChar c)
        {
            var builder = new ClassBuilder();

            foreach(var kvp in p.BaseClasses)
            {
                var toAdd = builder.Build(kvp.Key);
                toAdd.Level = kvp.Value;
                c.BaseClasses.Add(kvp.Key, toAdd);
            }
        }

        private void BuildClassPrimaryStats(MChar c)
        {
            foreach (var kvp in c.BaseClasses)
            {
                var classStats = kvp.Value.GetParams();
                foreach (var stat in classStats.PrimaryStats)
                {
                    switch(stat.Key)
                    {
                        case (EPrimaryStat.Agility): { c.PrimaryStats.Agility += stat.Value; } break;
                        case (EPrimaryStat.Constitution): { c.PrimaryStats.Constitution += stat.Value; } break;
                        case (EPrimaryStat.Intelligence): { c.PrimaryStats.Intelligence += stat.Value; } break;
                        case (EPrimaryStat.Might): { c.PrimaryStats.Might += stat.Value; } break;
                        case (EPrimaryStat.Perception): { c.PrimaryStats.Perception += stat.Value; } break;
                        case (EPrimaryStat.Resolve): { c.PrimaryStats.Resolve += stat.Value; } break;
                    }
                }
            }
        }

        private void BuildClassSecondaryStats(MChar c)
        {
            foreach (var kvp in c.BaseClasses)
            {
                var stats = kvp.Value.GetParams();

                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.AP))
                    c.SecondaryStats.MaxAP += stats.SecondaryStats[ESecondaryStat.AP];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Block))
                    c.SecondaryStats.Block += stats.SecondaryStats[ESecondaryStat.Block];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Concentration))
                    c.SecondaryStats.Concentration += stats.SecondaryStats[ESecondaryStat.Concentration];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Critical_Chance))
                    c.SecondaryStats.CriticalChance += stats.SecondaryStats[ESecondaryStat.Critical_Chance];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Critical_Multiplier))
                    c.SecondaryStats.CriticalMultiplier += stats.SecondaryStats[ESecondaryStat.Critical_Multiplier];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Dodge))
                    c.SecondaryStats.DodgeSkill += stats.SecondaryStats[ESecondaryStat.Dodge];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Fortitude))
                    c.SecondaryStats.Fortitude += stats.SecondaryStats[ESecondaryStat.Fortitude];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.HP))
                    c.SecondaryStats.MaxHP += stats.SecondaryStats[ESecondaryStat.HP];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Initiative))
                    c.SecondaryStats.Initiative += stats.SecondaryStats[ESecondaryStat.Initiative];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Melee))
                    c.SecondaryStats.MeleeSkill += stats.SecondaryStats[ESecondaryStat.Melee];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Morale))
                    c.SecondaryStats.Morale += stats.SecondaryStats[ESecondaryStat.Morale];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Parry))
                    c.SecondaryStats.ParrySkill += stats.SecondaryStats[ESecondaryStat.Parry];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Power))
                    c.SecondaryStats.Power += stats.SecondaryStats[ESecondaryStat.Power];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Ranged))
                    c.SecondaryStats.RangedSkill += stats.SecondaryStats[ESecondaryStat.Ranged];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Reflex))
                    c.SecondaryStats.Reflex += stats.SecondaryStats[ESecondaryStat.Reflex];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Spell_Duration))
                    c.SecondaryStats.SpellDuration += stats.SecondaryStats[ESecondaryStat.Spell_Duration];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Spell_Penetration))
                    c.SecondaryStats.SpellPenetration += stats.SecondaryStats[ESecondaryStat.Spell_Penetration];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Stamina))
                    c.SecondaryStats.Stamina += stats.SecondaryStats[ESecondaryStat.Stamina];
                if (stats.SecondaryStats.ContainsKey(ESecondaryStat.Will))
                    c.SecondaryStats.Will += stats.SecondaryStats[ESecondaryStat.Will];
            }
        }

        private void BuildDefaultAbilities(CharParams p, MChar c)
        {
            var activeAbs = AbilityFactory.Instance.CreateNewObject(p.Abilities);
            foreach (var v in activeAbs) { c.ActiveAbilities.Add(v); }

            var wpnAbs = WeaponAbilityFactory.Instance.CreateNewObject(p.DefaultWpnAbilities);
            foreach (var v in wpnAbs) { c.DefaultWpnAbilities.Add(v); }
        }

        private void BuildWeaponHelper(MChar c, CharParams p)
        {
            if (p.LWeapon != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(p.LWeapon.Name, p.LWeapon.Tier);
                c.AddWeapon(weapon, true);
            }
            if (p.RWeapon != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(p.RWeapon.Name, p.RWeapon.Tier);
                c.AddWeapon(weapon, false);
            }
        }

        private void BuildArmorHelper(MChar c, CharParams p)
        {
            if (p.Armor != null)
            {
                var armor = ArmorFactory.Instance.CreateNewObject(p.Armor.Name, p.Armor.Tier);
                c.AddArmor(armor);
            }
            if (p.Helm != null)
            {
                var helm = HelmFactory.Instance.CreateNewObject(p.Helm.Name, p.Helm.Tier);
                c.AddHelm(helm);
            }
        }
    }
}
