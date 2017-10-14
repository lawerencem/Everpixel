using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Armor
{
    public class MArmorData : MEquipmentData
    {
        public double APMod { get; set; }
        public EArmorType ArmorType { get; set; }
        public double BlockMod { get; set; }
        public double DamageMod { get; set; }
        public double DodgeMod { get; set; }
        public double FatigueMod { get; set; }
        public double FlatDamageIgnore { get; set; }
        public double InitiativeMod { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public double StaminaMod { get; set; }
    }

    public class MArmor : MEquipment
    {
        private MArmorData _data;
        public MArmorData Data { get { return this._data; } }

        public MArmor() : base(EEquipmentType.Worn)
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
