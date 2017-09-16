using Assets.Controller.Character;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat.Loader;
using Assets.Controller.Map.Tile;
using Assets.Data.Character.Table;
using Assets.Model.Character;
using Assets.Model.Character.Factory;
using Assets.Model.Character.Summon;
using Assets.Model.Party;
using Assets.View.Builder;

namespace Assets.Model.Event.Combat
{
    public class EvSummonData
    {
        public int Duration { get; set; }
        public bool LParty { get; set; }
        public SummonModData ModData { get; set; }
        public string ParticlePath { get; set; }
        public MParty Party { get; set; }
        public TileController TargetTile { get; set; }
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
                var summon = CharacterFactory.Instance.CreateNewObject(preCharParams);
                summon.SetLParty(this._data.LParty);
                var controller = new CharController();
                var proxy = new PChar(summon);
                controller.SetProxy(proxy);
                this._data.Party.AddChar(controller);
                CombatManager.Instance.ProcessSummon(controller);
                this._data.TargetTile = this._data.TargetTile.GetNearestEmptyTile();
                CharLoader.Instance.LoadSummon(controller, this._data.TargetTile);
                this.ProcessParticles(controller);
            }
        }

        private void ProcessParticles(CharController c)
        {

        }
    }
}
