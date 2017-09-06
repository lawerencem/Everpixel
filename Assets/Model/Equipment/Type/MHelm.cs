using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Type
{
    public class MHelm : MEquipment
    {
        public double APMod { get; set; }
        public EArmorType ArmorType { get; set; }
        public double BlockMod { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageMod { get; set; }
        public double DodgeMod { get; set; }
        public double FatigueMod { get; set; }
        public double InitativeMod { get; set; }
        public string Name { get; set; }
        public double ParryMod { get; set; }
        public double StaminaMod { get; set; }

        public MHelm() : base(EEquipmentType.Worn)
        {

        }

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.AP, this.APMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Block, this.BlockMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Dodge, this.DodgeMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Initiative, this.InitativeMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Parry, this.ParryMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Stamina, this.StaminaMod));

            return toReturn;
        }
    }
}

