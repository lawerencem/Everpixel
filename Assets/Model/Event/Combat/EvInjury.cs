using Assets.Controller.Character;
using Assets.Model.Combat.Hit;
using Assets.Model.Injury;

namespace Assets.Model.Event.Combat
{
    public class EvApplyInjuryData
    {
        public MHit Hit { get; set; }
        public MInjury Injury { get; set; }
        public CChar Target { get; set; }
    }

    public class EvInjury : MEvCombat
    {
        private EvApplyInjuryData _data;
        public EvInjury(EvApplyInjuryData d) : base(ECombatEv.ApplyInjury)
        {
            this._data = d;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
                this.Process();
        }

        private void Process()
        {
            var proxy = this._data.Target.Proxy;
            var injuries = proxy.GetEffects().GetInjuries();
            var exists = injuries.Find(x => x.Type == this._data.Injury.Type);
            if (exists == null)
            {
                proxy.AddInjury(this._data.Injury);
            }
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Target == null)
                return false;
            return true;
        }
    }
}
