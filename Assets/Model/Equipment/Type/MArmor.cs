using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Enum;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Type
{
    public class MArmor : MEquipment
    {
        public double APReduce { get; set; }
        public EArmorType ArmorType { get; set; }
        public double BlockReduce { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduction { get; set; }
        public double DodgeMod { get; set; }
        public double FatigueCost { get; set; }
        public double InitativeReduce { get; set; }
        public string Name { get; set; }
        public double ParryReduce { get; set; }
        public double StaminaReduce { get; set; }

        public MArmor() : base(EEquipmentType.Worn)
        {
            
        }

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.AP, this.APReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Block, this.BlockReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Dodge, this.DodgeMod));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Initiative, this.InitativeReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Parry, this.ParryReduce));
            toReturn.Add(new IndefSecondaryStatModifier(ESecondaryStat.Stamina, this.StaminaReduce));

            return toReturn;
        }
    }
}
