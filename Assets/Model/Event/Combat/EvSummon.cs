using Assets.Controller.Character;
using Assets.Controller.GUI.Combat;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat.Loader;
using Assets.Controller.Map.Tile;
using Assets.Data.Character.Table;
using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Factory;
using Assets.Model.Character.Summon;
using Assets.Model.Party;
using Assets.Template.Script;
using Assets.Template.Util;
using Assets.View.Particle;

namespace Assets.Model.Event.Combat
{
    public class EvSummonData
    {
        public int Duration { get; set; }
        public bool LParty { get; set; }
        public SummonModData ModData { get; set; }
        public string ParticlePath { get; set; }
        public MParty Party { get; set; }
        public CTile TargetTile { get; set; }
        public string ToSummon { get; set; }
    }

    public class EvSummon : MEvCombat
    {
        private EvSummonData _data;

        public EvSummon(EvSummonData data) : base(ECombatEv.Summon)
        {
            this._data = data;
        }

        public override void TryProcess()
        {
            base.TryProcess();
            if (this.IsInitialized())
                this.Process();
        }

        private bool IsInitialized()
        {
            if (this._data == null ||
                this._data.Party == null ||
                this._data.TargetTile == null ||
                this._data.ToSummon == "")
            {
                return false;
            }
            else
                return true;
        }

        private void Process()
        {
            if (PredefinedCharTable.Instance.Table.ContainsKey(this._data.ToSummon))
            {
                var preCharParams = PredefinedCharTable.Instance.Table[this._data.ToSummon];
                var factory = new CharacterFactory();
                var summon = factory.CreateNewCharacter(preCharParams);
                summon.SetLParty(this._data.LParty);
                var controller = new CChar();
                var proxy = new PChar(summon);
                controller.SetProxy(proxy);
                this._data.Party.AddChar(controller);
                CombatManager.Instance.ProcessSummon(controller);
                this._data.TargetTile = this._data.TargetTile.GetNearestEmptyTile();
                CharLoader.Instance.LoadSummon(controller, this._data.TargetTile);
                proxy.SetPointsToMax(ESecondaryStat.AP);
                proxy.SetPointsToMax(ESecondaryStat.HP);
                proxy.SetPointsToMax(ESecondaryStat.Stamina);
                proxy.SetPointsToMax(ESecondaryStat.Morale);
                this.ProcessParticles(controller);
            }
        }

        private void ProcessParticles(CChar c)
        {
            if (this._data.ParticlePath != null && !this._data.ParticlePath.Equals(""))
            {
                var path = StringUtil.PathBuilder(
                    CombatGUIParams.EFFECTS_PATH,
                    this._data.ParticlePath,
                    CombatGUIParams.PARTICLES_EXTENSION);
                var controller = new ParticleController();
                var particles = controller.CreateParticle(path);
                var script = particles.AddComponent<SDestroyByLifetime>();
                script.Init(particles, CombatGUIParams.PARTICLE_DUR);
                controller.AttachParticle(c.GameHandle, particles);
            }
        }
    }
}
