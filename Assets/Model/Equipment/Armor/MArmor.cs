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
        public double InitativeMod { get; set; }
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

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.AP, this.Data.APMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Block, this.Data.BlockMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Dodge, this.Data.DodgeMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Initiative, this.Data.InitativeMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Parry, this.Data.ParryMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Stamina, this.Data.StaminaMod));

            return toReturn;
        }
    }
}
