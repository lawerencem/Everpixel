
using Assets.Model.Ability.Enum;
using Assets.Model.Character;

namespace Assets.Model.Perk
{
    public class MPerk : APerk
    {
        public double AoE { get; set; }
        public double Dur { get; set; }
        public MChar Parent { get; set; }
        public EResistType Resist { get; set; }
        public double Val { get; set; }
        public double ValPerPower { get; set; }

        public MPerk(EPerk type)
        {
            this._type = type;
        }

        public virtual void Init(MChar parent)
        {
            var proto = PerkTable.Instance.Table[this.Type];
            this.AoE = proto.AoE;
            this.Dur = proto.Dur;
            this.Parent = parent;
            this.Resist = EResistType.None;
            this.Val = proto.Val;
            this.ValPerPower = proto.ValPerPower;
        }
    }
}
