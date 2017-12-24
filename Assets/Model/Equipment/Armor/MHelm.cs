using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Armor
{
    public class MHelm : MEquipment
    {
        private MArmorData _data;
        public MArmorData Data { get { return this._data; } }

        public MHelm() : base(EEquipmentType.Worn)
        {
            this._data = new MArmorData();
        }

        public List<StatMod> GetStatModifiers()
        {
            var toReturn = new List<StatMod>();

            var ap = this.GetModProto();
            var block = this.GetModProto();
            var dodge = this.GetModProto();
            var init = this.GetModProto();
            var parry = this.GetModProto();
            var stam = this.GetModProto();

            ap.Scalar = this.Data.APMod;
            block.Scalar = this.Data.BlockMod;
            dodge.Scalar = this.Data.DodgeMod;
            init.Scalar = this.Data.InitiativeMod;
            parry.Scalar = this.Data.ParryMod;
            stam.Scalar = this.Data.StaminaMod;

            ap.Stat = ESecondaryStat.AP;
            block.Stat = ESecondaryStat.Block;
            dodge.Stat = ESecondaryStat.Dodge;
            init.Stat = ESecondaryStat.Initiative;
            parry.Stat = ESecondaryStat.Parry;
            stam.Stat = ESecondaryStat.Stamina;

            toReturn.Add(new StatMod(ap));
            toReturn.Add(new StatMod(block));
            toReturn.Add(new StatMod(dodge));
            toReturn.Add(new StatMod(init));
            toReturn.Add(new StatMod(parry));
            toReturn.Add(new StatMod(stam));

            return toReturn;
        }
    }
}

