using Assets.Controller.Character;
using Assets.Model.Character.Enum;

namespace Assets.Model.Event.Combat
{
    public class EvStaminaModData
    {
        public CChar Char { get; set; }
        public bool ToDisplay { get; set; }
        public bool IsHeal { get; set; }
        public int Qty { get; set; }
    }

    public class EvStaminaMod : MEvCombat
    {
        private EvStaminaModData _data;

        public EvStaminaMod() : base(ECombatEv.StaminaMod) { }
        public EvStaminaMod(EvStaminaModData d) : base(ECombatEv.StaminaMod) { this._data = d; }

        public void SetData(EvStaminaModData data) { this._data = data; }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
            {
                this._data.Char.Proxy.ModifyPoints(ESecondaryStat.Stamina, this._data.Qty, this._data.IsHeal);
                if (this._data.ToDisplay)
                {
                    // TODO: Display stamina mod
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
