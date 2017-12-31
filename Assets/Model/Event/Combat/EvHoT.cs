using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Model.Effect;
using Assets.Model.OTE;
using Assets.Model.OTE.HoT;
using Assets.Template.Util;
using Assets.View.Particle;

namespace Assets.Model.Event.Combat
{
    public class EvHoTData
    {
        public int Dmg { get; set; }
        public int Dur { get; set; }
        public bool HasDur { get; set; }
        public CChar Tgt { get; set; }
    }

    public class EvHoT : MEvCombat
    {
        private static readonly string PATH = "Effects/HoTParticles";

        private EvHoTData _data;

        public EvHoT(EvHoTData d) : base(ECombatEv.HoT)
        {
            this._data = d;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.VerifyAndPopulateData())
                this.Process();
        }

        private void ApplyHoTFX()
        {
            var exists = this._data.Tgt.Proxy.GetEffects().GetEffects().Find(x => x.Type == EEffect.HoT);
            if (exists == null)
            {
                var particles = ParticleController.Instance.CreateParticle(EvHoT.PATH);
                if (particles != null)
                    DecoUtil.AttachParticles(particles, this._data.Tgt.Handle);
            }
        }

        private void Process()
        {
            var hots = this._data.Tgt.Proxy.GetEffects().GetHots();
            if (hots.Count == 0)
                this.ApplyHoTFX();
            var data = new MOTEData();
            data.Dmg = this._data.Dmg;
            data.Dur = this._data.Dur;
            data.HasDur = this._data.HasDur;
            var hot = new MHoT(data);
            this._data.Tgt.Proxy.AddHoT(hot);
        }

        private bool VerifyAndPopulateData()
        {
            if (this._data == null)
                return false;
            if (this._data.Tgt == null)
                return false;
            return true;
        }
    }
}
