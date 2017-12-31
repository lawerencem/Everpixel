
using Assets.Data.Perk.Table;
using Assets.Model.Ability.Enum;
using Assets.Model.Character;
using Assets.Model.Character.Param;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Perk
{
    public class MPerk : APerk
    {
        public double AoE { get; set; }
        public double Dur { get; set; }
        public EResistType Resist { get; set; }
        public double Val { get; set; }
        public double ValPerPower { get; set; }

        public MPerk(EPerk type)
        {
            this._type = type;
        }

        public virtual void AddToParent(CharPerks parentContainer)
        {
            
        }

        public void Init()
        {
            try
            {
                var proto = PerkTable.Instance.Table[this.Type];
                this.AoE = proto.AoE;
                this.Dur = proto.Dur;
                this.Resist = EResistType.None;
                this.Val = proto.Val;
                this.ValPerPower = proto.ValPerPower;
            }
            catch(KeyNotFoundException e)
            {
                Debug.LogError(e);
            }
        }
    }
}
