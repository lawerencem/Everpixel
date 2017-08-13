using Assets.Model.Ability;
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
    public class CharBuilder : GBuilder<CharParams, MChar>
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
            PerkMediator.Instance.SetCharacterPerks(character, c.Perks);
            BuildBaseClassHelper(c, character);
            BuildDefaultAbilities(c, character);
            var stats = PredefinedCharTable.Instance.Table[c.Name];
            character.GetCurrentStats().SetPrimaryStats(stats.Stats);
            BuildClassPrimaryStats(character);
            var secondary = GetSecondaryStats(character.GetCurrentStats().GetPrimaryStats());
            character.GetCurrentStats().SetSecondaryStats(secondary);
            BuildClassSecondaryStats(character);
            character.SetType(c.Type);
            character.SetParams(c);

            if (c.Type == Enum.ECharType.Humanoid)
            {
                this.BuildArmorHelper(character, c);
                this.BuildWeaponHelper(character, c);
            }

            return character;
        }

        private void BuildBaseClassHelper(CharParams p, MChar c)
        {
            var builder = new ClassBuilder();

            foreach (var kvp in p.BaseClasses)
            {
                var toAdd = builder.Build(kvp.Key);
                toAdd.Level = kvp.Value;
                c.GetBaseClasses().Add(kvp.Key, toAdd);
            }
        }

        private void BuildDefaultAbilities(CharParams p, MChar c)
        {
            var activeAbs = AbilityFactory.Instance.CreateNewObject(p.Abilities);
            foreach (var v in activeAbs)
                c.GetAbilities().GetActiveAbilities().Add(v);

            var wpnAbs = WeaponAbilityFactory.Instance.CreateNewObject(p.DefaultWpnAbilities);
            foreach (var v in wpnAbs)
                c.GetAbilities().GetDefaultAbilities().Add(v);
        }

        private void BuildClassPrimaryStats(MChar c)
        {
            foreach (var kvp in c.GetBaseClasses())
            {
                var classStats = kvp.Value.GetParams();
                foreach (var stat in classStats.PrimaryStats)
                    c.GetCurrentStats().SetStat(stat.Key, stat.Value);
            }
        }

        private void BuildClassSecondaryStats(MChar c)
        {
            foreach (var kvp in c.GetBaseClasses())
            {
                var stats = kvp.Value.GetParams();
                foreach (var stat in stats.SecondaryStats)
                    c.GetCurrentStats().AddStat(stat.Key, stat.Value);
            }
        }

        private void BuildWeaponHelper(MChar c, CharParams p)
        {
            if (p.LWeapon != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(p.LWeapon.Name, p.LWeapon.Tier);
                c.GetEquipment().AddWeapon(weapon, true);
            }
            if (p.RWeapon != null)
            {
                var weapon = WeaponFactory.Instance.CreateNewObject(p.RWeapon.Name, p.RWeapon.Tier);
                c.GetEquipment().AddWeapon(weapon, false);
            }
        }

        private void BuildArmorHelper(MChar c, CharParams p)
        {
            if (p.Armor != null)
            {
                var armor = ArmorFactory.Instance.CreateNewObject(p.Armor.Name, p.Armor.Tier);
                c.GetEquipment().AddArmor(armor);
            }
            if (p.Helm != null)
            {
                var helm = HelmFactory.Instance.CreateNewObject(p.Helm.Name, p.Helm.Tier);
                c.GetEquipment().AddHelm(helm);
            }
        }
    }
}
