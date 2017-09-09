using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Perk.Equipment
{
    public class TwoHandedExpert : MEquipmentPerk
    {
        public TwoHandedExpert() : base(EPerk.Two_Handed_Expert) { }

        public override void TryProcessAdd(MChar character, object equipment)
        {
            //base.TryProcessAdd(character, equipment);
            //if (equipment.GetType().Equals(typeof(MWeapon)))
            //{
            //    var wpn = equipment as MWeapon;
            //    if (wpn.WpnType == EWeaponType.Two_Handed)
            //    {
            //        var mightMod = new IndefPrimaryStatMod(EPrimaryStat.Might, this.Val);
            //        var gearMods = character.GetMods().GetIndefPStatGearMods();
            //        var wpnMods = gearMods.Find(x => x.X.Equals(wpn));
            //        if (wpnMods.X != null)
            //        {
            //            wpnMods.Y.Add(mightMod);
            //        }
            //        else
            //        {
            //            var list = new List<IndefPrimaryStatMod>() { mightMod };
            //            var pair = new Pair<object, List<IndefPrimaryStatMod>>(wpn, list);
            //            character.GetMods().AddMod(pair);
            //        }
            //    }
            //}
        }
    }
}
