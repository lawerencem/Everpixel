using Assets.Model.Character.Param;
using Assets.Model.Character.XML;
using Assets.Model.Characters.Params;
using Assets.Model.Equipment.Param;
using Assets.Model.Equipment.Table;
using Assets.Model.Mount;
using Generics;
using Generics.Utilities;
using Model.Equipment;
using Model.Equipment.Table;
using System;
using System.Collections.Generic;

namespace Assets.Model.Character.Builder
{
    public class CharacterParamBuilder : AbstractBuilder<PreCharParams, CharParams>
    {
        private const string SINGULAR = "One";

        public override CharParams Build()
        {
            throw new NotImplementedException();
        }

        public override CharParams Build(List<PreCharParams> args)
        {
            throw new NotImplementedException();
        }

        public override CharParams Build(PreCharParams arg)
        {
            var cParams = new CharParams();
            cParams.Armor = GetArmor(arg);
            foreach (var v in arg.ActiveAbilities) { cParams.Abilities.Add(v); }
            foreach (var kvp in arg.Classes) { cParams.BaseClasses.Add(kvp.Key, kvp.Value); }
            foreach (var v in arg.DefaultWpnAbilities) { cParams.DefaultWpnAbilities.Add(v); }
            cParams.Helm = GetHelm(arg);
            cParams.LWeapon = GetWeapon(arg, arg.LWeapons);
            cParams.Mount = this.GetMount(arg);
            cParams.Name = arg.Name;
            cParams.Perks = arg.Perks;
            cParams.Race = arg.Race;
            cParams.RWeapon = GetWeapon(arg, arg.RWeapons);
            cParams.Type = arg.Type;
            return cParams;
        }

        private ArmorParams GetArmor(PreCharParams arg)
        {
            var csv = new List<string>();
            if (arg.Armors.ContainsKey(SINGULAR))
                csv = GetEquipmentCSV(arg.Armors[SINGULAR]);
            if (csv != null && csv.Count >= 3)
            {
                var key = csv[PredefinedEquipmentXMLIndexes.NAME] + "_" + csv[PredefinedEquipmentXMLIndexes.TIER];
                var armor = ArmorParamTable.Instance.Table[key];
                armor.Sprites = EquipmentSpritesTable.Instance.Table[key];
                return armor;
            }
            else
                return null;
        }

        private List<string> GetEquipmentCSV(List<List<string>> potentials)
        {
            double tally = 0;
            double chance = RNG.Instance.NextDouble();
            for(int i = 0; i < potentials.Count; i++)
            {
                tally += Double.Parse(potentials[i][PredefinedEquipmentXMLIndexes.CHANCE]);
                if (tally >= chance)
                    return potentials[i];
            }
            return null;
        }

        private ArmorParams GetHelm(PreCharParams arg)
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

        private MountParams GetMount(PreCharParams arg)
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

        private WeaponParams GetWeapon(PreCharParams arg, Dictionary<string, List<List<string>>> table)
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
