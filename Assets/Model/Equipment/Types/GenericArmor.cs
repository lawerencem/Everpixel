using Characters.Params;
using Model.Abilities;
using Model.Events;
using Model.Slot;
using System.Collections.Generic;

namespace Model.Equipment
{
    public class GenericArmor : GenericEquipment
    {
        public double APReduce { get; set; }
        public double BlockReduce { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduction { get; set; }
        public double DodgeMod { get; set; }
        public double FatigueCost { get; set; }
        public double InitativeReduce { get; set; }
        public string Name { get; set; }
        public double ParryReduce { get; set; }
        public double StaminaReduce { get; set; }


        public GenericArmor() : base(SlotEnum.Torso, EquipmentTypeEnum.Worn)
        {
            
        }

        public List<IndefSecondaryStatModifier> GetStatModifiers()
        {
            var toReturn = new List<IndefSecondaryStatModifier>();

            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.AP, this.APReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Block, this.BlockReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Dodge, this.DodgeMod));
            //toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.fatigue, this.APReduce)); TODO
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Initiative, this.InitativeReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Parry, this.ParryReduce));
            toReturn.Add(new IndefSecondaryStatModifier(Characters.SecondaryStatsEnum.Stamina, this.StaminaReduce));

            return toReturn;
        }
    }
}
