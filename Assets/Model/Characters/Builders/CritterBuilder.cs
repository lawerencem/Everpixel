using Generics;
using Model.Classes;
using Model.Slot;
using System;
using System.Collections.Generic;

namespace Model.Characters
{
    public class CritterBuilder : GenericBuilder<CharacterParams, GenericCharacter>
    {
        public override GenericCharacter Build()
        {
            throw new NotImplementedException();
        }

        public override GenericCharacter Build(List<CharacterParams> args)
        {
            throw new NotImplementedException();
        }

        public override GenericCharacter Build(CharacterParams arg)
        {
            return BuildHelper(arg);
        }

        private PrimaryStats GetRaceStats(CharacterParams c)
        {
            if (DefaultRaceStatsTable.Instance.Table.ContainsKey(c.Race))
                return DefaultRaceStatsTable.Instance.Table[c.Race].Clone();
            else
                return null;
        }

        private SecondaryStats GetSecondaryStats(PrimaryStats p)
        {
            return new SecondaryStats(p);
        }

        private SlotCollection GetSlotCollection()
        {
            var collection = new SlotCollection();
            return collection;
        }

        private GenericCharacter BuildHelper(CharacterParams c)
        {
            var character = new GenericCharacter();
            BuildBaseClassHelper(c, character);
            var stats = PredefinedCharacterTable.Instance.Table[c.Name];
            character.PrimaryStats = stats.Stats;
            BuildClassPrimaryStats(character);
            var secondary = GetSecondaryStats(character.PrimaryStats);
            character.SecondaryStats = secondary;
            BuildClassSecondaryStats(character);
            character.Type = c.Type;
            character.CurrentAP = character.GetCurrentStatValue(SecondaryStatsEnum.AP);
            character.CurrentHP = character.GetCurrentStatValue(SecondaryStatsEnum.HP);
            character.CurrentMorale = character.GetCurrentStatValue(SecondaryStatsEnum.Morale);
            character.CurrentStamina = character.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
            return character;
        }

        private void BuildBaseClassHelper(CharacterParams p, GenericCharacter c)
        {
            var builder = new ClassBuilder();

            foreach (var kvp in p.BaseClasses)
            {
                var toAdd = builder.Build(kvp.Key);
                toAdd.Level = kvp.Value;
                c.BaseClasses.Add(kvp.Key, toAdd);
            }
        }

        private void BuildClassPrimaryStats(GenericCharacter c)
        {
            foreach (var kvp in c.BaseClasses)
            {
                var classStats = kvp.Value.GetParams();
                foreach (var stat in classStats.PrimaryStats)
                {
                    switch (stat.Key)
                    {
                        case (PrimaryStatsEnum.Agility): { c.PrimaryStats.Agility += stat.Value; } break;
                        case (PrimaryStatsEnum.Constitution): { c.PrimaryStats.Constitution += stat.Value; } break;
                        case (PrimaryStatsEnum.Intelligence): { c.PrimaryStats.Intelligence += stat.Value; } break;
                        case (PrimaryStatsEnum.Might): { c.PrimaryStats.Might += stat.Value; } break;
                        case (PrimaryStatsEnum.Perception): { c.PrimaryStats.Perception += stat.Value; } break;
                        case (PrimaryStatsEnum.Resolve): { c.PrimaryStats.Resolve += stat.Value; } break;
                    }
                }
            }
        }

        private void BuildClassSecondaryStats(GenericCharacter c)
        {
            foreach (var kvp in c.BaseClasses)
            {
                var stats = kvp.Value.GetParams();

                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.AP))
                    c.SecondaryStats.MaxAP += stats.SecondaryStats[SecondaryStatsEnum.AP];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Block))
                    c.SecondaryStats.Block += stats.SecondaryStats[SecondaryStatsEnum.Block];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Concentration))
                    c.SecondaryStats.Concentration += stats.SecondaryStats[SecondaryStatsEnum.Concentration];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Critical_Chance))
                    c.SecondaryStats.CriticalChance += stats.SecondaryStats[SecondaryStatsEnum.Critical_Chance];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Critical_Multiplier))
                    c.SecondaryStats.CriticalMultiplier += stats.SecondaryStats[SecondaryStatsEnum.Critical_Multiplier];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Dodge))
                    c.SecondaryStats.DodgeSkill += stats.SecondaryStats[SecondaryStatsEnum.Dodge];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Fortitude))
                    c.SecondaryStats.Fortitude += stats.SecondaryStats[SecondaryStatsEnum.Fortitude];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Initiative))
                    c.SecondaryStats.MaxHP += stats.SecondaryStats[SecondaryStatsEnum.HP];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.HP))
                    c.SecondaryStats.Initiative += stats.SecondaryStats[SecondaryStatsEnum.Initiative];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Melee))
                    c.SecondaryStats.MeleeSkill += stats.SecondaryStats[SecondaryStatsEnum.Melee];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Morale))
                    c.SecondaryStats.Morale += stats.SecondaryStats[SecondaryStatsEnum.Morale];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Parry))
                    c.SecondaryStats.ParrySkill += stats.SecondaryStats[SecondaryStatsEnum.Parry];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Power))
                    c.SecondaryStats.Power += stats.SecondaryStats[SecondaryStatsEnum.Power];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Ranged))
                    c.SecondaryStats.RangedSkill += stats.SecondaryStats[SecondaryStatsEnum.Ranged];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Reflex))
                    c.SecondaryStats.Reflex += stats.SecondaryStats[SecondaryStatsEnum.Reflex];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Spell_Duration))
                    c.SecondaryStats.SpellDuration += stats.SecondaryStats[SecondaryStatsEnum.Spell_Duration];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Spell_Penetration))
                    c.SecondaryStats.SpellPenetration += stats.SecondaryStats[SecondaryStatsEnum.Spell_Penetration];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Stamina))
                    c.SecondaryStats.Stamina += stats.SecondaryStats[SecondaryStatsEnum.Stamina];
                if (stats.SecondaryStats.ContainsKey(SecondaryStatsEnum.Will))
                    c.SecondaryStats.Will += stats.SecondaryStats[SecondaryStatsEnum.Will];
            }
        }
    }
}
