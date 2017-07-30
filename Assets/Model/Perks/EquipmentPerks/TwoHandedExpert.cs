using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Characters;
using Model.Equipment;

namespace Model.Perks
{
    public class TwoHandedExpert : GenericEquipmentPerk
    {
        public TwoHandedExpert() : base(PerkEnum.Two_Handed_Expert) { }

        public override void TryProcessAdd(GenericCharacter character, object equipment)
        {
            base.TryProcessAdd(character, equipment);
            if (equipment.GetType().Equals(typeof(GenericWeapon)))
            {
                var wpn = equipment as GenericWeapon;
                if (wpn.WpnType == WeaponTypeEnum.Two_Handed)
                {
                    var mightMod = new IndefPrimaryStatModifier(PrimaryStatsEnum.Might, this.Val);
                    var gearMods = character.Mods.IndefPStatGearMods;
                    var wpnMods = gearMods.Find(x => x.X.Equals(wpn));
                    if (wpnMods.X != null)
                    {
                        wpnMods.Y.Add(mightMod);
                    }
                    else
                    {
                        var list = new List<IndefPrimaryStatModifier>() { mightMod };
                        var pair = new Pair<object, List<IndefPrimaryStatModifier>>(wpn, list);
                        character.Mods.AddMod(pair);
                    }
                }
            }
        }
    }
}
