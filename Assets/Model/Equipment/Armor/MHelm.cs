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

