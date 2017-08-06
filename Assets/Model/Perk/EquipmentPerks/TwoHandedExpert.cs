using System.Collections.Generic;
using Assets.Generics;
using Characters.Params;
using Model.Characters;
using Model.Equipment;

namespace Model.Perks
{
    public class TwoHandedExpert : MEquipmentPerk
    {
        public TwoHandedExpert() : base(EPerk.Two_Handed_Expert) { }

        public override void TryProcessAdd(MChar character, object equipment)
        {
            base.TryProcessAdd(character, equipment);
            if (equipment.GetType().Equals(typeof(MWeapon)))
            {
                var wpn = equipment as MWeapon;
                if (wpn.WpnType == EWeaponType.Two_Handed)
                {
                    var mightMod = new IndefPrimaryStatMod(EPrimaryStat.Might, this.Val);
                    var gearMods = character.Mods.IndefPStatGearMods;
                    var wpnMods = gearMods.Find(x => x.X.Equals(wpn));
                    if (wpnMods.X != null)
                    {
                        wpnMods.Y.Add(mightMod);
                    }
                    else
                    {
                        var list = new List<IndefPrimaryStatMod>() { mightMod };
                        var pair = new Pair<object, List<IndefPrimaryStatMod>>(wpn, list);
                        character.Mods.AddMod(pair);
                    }
                }
            }
        }
    }
}
