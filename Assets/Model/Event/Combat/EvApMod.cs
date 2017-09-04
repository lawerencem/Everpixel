using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Character.Enum;

namespace Assets.Model.Event.Combat
{
    public class EvAPModData
    {
        public CharController Char { get; set; }
        public bool ToDisplay { get; set; }
        public bool IsHeal { get; set; }
        public int Qty { get; set; }
    }

    public class EvAPMod : MEvCombat
    {
        private EvAPModData _data;

        public EvAPMod() : base(ECombatEv.APMod) { }
        public EvAPMod(EvAPModData d) : base(ECombatEv.APMod) { this._data = d; }

        public void SetData(EvAPModData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
            {
                this._data.Char.Proxy.ModifyPoints(ESecondaryStat.AP, this._data.Qty, this._data.IsHeal);
                if (this._data.ToDisplay)
                {
                    // TODO: Display ap mod
                }
            }
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Char == null)
                return false;
            return true;
        }
    }
}
