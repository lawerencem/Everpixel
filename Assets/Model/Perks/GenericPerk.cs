namespace Model.Perks
{
    public class GenericPerk : AbstractPerk
    {
        public double AoE { get; set; }
        public double Dur { get; set; }
        public double Val { get; set; }
        public double ValPerPower { get; set; }

        public GenericPerk(PerkEnum type)
        {
            this._type = type;
        }

        public virtual void Init()
        {
            var proto = PerkTable.Instance.Table[this.Type];
            this.AoE = proto.AoE;
            this.Dur = proto.Dur;
            this.Val = proto.Val;
            this.ValPerPower = proto.ValPerPower;
        }
    }
}
