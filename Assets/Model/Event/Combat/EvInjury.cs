using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Injury;
using Assets.View;

namespace Assets.Model.Event.Combat
{
    public class EvInjuryData
    {
        public MHit Hit { get; set; }
        public MInjury Injury { get; set; }
        public CChar Target { get; set; }
    }

    public class EvInjury : MEvCombat
    {
        private EvInjuryData _data;
        public EvInjury(EvInjuryData d) : base(ECombatEv.ApplyInjury)
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
            if (this._data.Hit.Data.Dmg < this._data.Target.Proxy.GetPoints(ESecondaryStat.HP))
            {
                var proxy = this._data.Target.Proxy;
                proxy.AddInjury(this._data.Injury);
                var data = new HitDisplayData();
                data.Color = CombatGUIParams.RED;
                data.Hit = this._data.Hit;
                data.Priority = ViewParams.INJURY_PRIORITY;
                data.Target = this._data.Target.Handle;
                data.Text = this._data.Injury.Type.ToString().Replace("_", " ");
                data.YOffset = CombatGUIParams.FLOAT_OFFSET;
                data.Hit.AddDataDisplay(data);
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
