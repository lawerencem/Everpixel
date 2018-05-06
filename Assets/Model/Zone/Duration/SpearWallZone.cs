using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Logic.Calculator;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Template.Script;
using Assets.View;

namespace Assets.Model.Zone.Duration
{
    public class ZoneSpearWallData : ZoneData
    {
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public CTile ParentTile { get; set; }
    }

    public class SpearWallZone : AZone
    {
        private MAction _action;
        private ZoneSpearWallData _spearWallData;

        public bool FirstSpearWallHit { get; set; }
        public bool SpearWallHit { get; set; }

        public SpearWallZone() : base(EZone.Spear_Wall_Zone)
        {
            this.FirstSpearWallHit = false;
            this.SpearWallHit = false;
        }

        public override void ProcessEnterZone(TileMoveData moveData)
        {
            this.FirstSpearWallHit = false;
            this.SpearWallHit = false;
            base.ProcessEnterZone(moveData);
            if (this._spearWallData.Source != null)
            {
                if (moveData.Target.Proxy.LParty != this._spearWallData.Source.Proxy.LParty)
                {
                    moveData.Callback(this);
                    var data = new ActionData();
                    data.Ability = EAbility.Pierce;
                    data.LWeapon = this._spearWallData.LWeapon;
                    data.ParentWeapon = this._spearWallData.ParentWeapon;
                    data.Source = this._spearWallData.Source;
                    data.Target = moveData.Target.Tile;
                    data.WpnAbility = true;
                    this._action = new MAction(data);
                    this._action.AddCallback(moveData.ParentEvent.TryDone);
                    moveData.ParentEvent.AddChildAction(this._action);
                    var staminaCalc = new StaminaCalculator();
                    var cost = staminaCalc.Process(this._action);
                    if (this._action.Data.ParentWeapon.View.SpearWalling &&
                        cost <= this._action.Data.Source.Proxy.GetPoints(ESecondaryStat.Stamina))
                    {
                        this._action.TryProcessNoDisplay();
                        foreach (var hit in this._action.Data.Hits)
                        {
                            if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block) &&
                                !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                                !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                            {
                                this.SpearWallHit = true;
                            }
                        }
                        this.HandleSpeared(null);
                    }
                }
            }
        }

        public void SetSpearWallZoneData(ZoneSpearWallData data)
        {
            this._spearWallData = data;
            this._data.DependsOnSourceChar = true;
            this._data.Source = data.Source;
            this._data.Source.Proxy.AddZone(this);
        }

        private void AddBob(object o)
        {
            var e = o as EvTileMove;
            if (e.GetData().Char != null && e.GetData().Char == CombatManager.Instance.GetCurrentlyActing())
            {
                var bob = e.GetData().Char.GameHandle.AddComponent<SBob>();
                bob.Init(ViewParams.BOB_PER_FRAME, ViewParams.BOB_PER_FRAME_DIST, e.GetData().Char.GameHandle);
            }
        }

        private void DoJolt(bool alreadySpearWalled)
        {
            if (!alreadySpearWalled && this.SpearWallHit)
            {
                foreach (var hit in this._action.Data.Hits)
                    hit.AddCallback(this.DoSpearWall);
            }
            this._action.DisplayAction();
        }

        private void DoSpearWall(object o)
        {
            var hit = o as MHit;
            var tgt = hit.Data.Target.Current as CChar;
            if (tgt != null)
            {
                var tgtTile = hit.Data.Source.Tile.Model.GetPushTile(tgt.Tile.Model);
                if (tgtTile != null && tgtTile.GetCurrentOccupant() == null)
                {
                    var data = new EvTileMoveData();
                    data.Char = tgt;
                    data.Cost = 0;
                    data.DoAttackOfOpportunity = false;
                    data.StamCost = 0;
                    data.Source = tgt.Tile;
                    data.Target = tgtTile.Controller;
                    var bob = data.Char.GameHandle.GetComponent<SBob>();
                    if (bob != null)
                        bob.Reset();
                    var e = new EvTileMove(data);
                    e.AddCallback(this.AddBob);
                    e.TryProcess();
                    this.TryUndoSpearwall(null);
                }
                else
                {
                    var controller = hit.Data.Source.Tile;
                    var random = controller.GetNearestEmptyTile();
                    if (random != null)
                    {
                        var data = new EvTileMoveData();
                        data.Char = tgt;
                        data.Cost = 0;
                        data.StamCost = 0;
                        data.Source = tgt.Tile;
                        data.Target = random;
                        var bob = data.Char.GameHandle.GetComponent<SBob>();
                        if (bob != null)
                            bob.Reset();
                        var e = new EvTileMove(data);
                        e.AddCallback(this.AddBob);
                        e.TryProcess();
                        this.TryUndoSpearwall(null);
                    }
                }
            }
        }

        private void HandleSpeared(object o)
        {
            bool alreadySpearwalled = false;
            foreach (var zone in this._spearWallData.Parent.GetZones())
            {
                if (zone.GetType().Equals(typeof(SpearWallZone)))
                {
                    var spearWallZone = zone as SpearWallZone;
                    if (spearWallZone.SpearWallHit && spearWallZone.FirstSpearWallHit)
                        alreadySpearwalled = true;
                } 
            }
            if (!alreadySpearwalled)
                this.FirstSpearWallHit = true;
            foreach (var hit in this._action.Data.Hits)
                this.DoJolt(alreadySpearwalled);
        }

        private void TryUndoSpearwall(object o)
        {
            var staminaCalc = new StaminaCalculator();
            var cost = staminaCalc.Process(this._action);
            if (cost >= this._action.Data.Source.Proxy.GetPoints(ESecondaryStat.Stamina))
            {
                var data = new EvUndoSpearwallData();
                data.Action = this._action;
                data.Char = this._action.Data.Source;
                var e = new EvUndoSpearwall(data);
                e.TryProcess();
            }
        }
    }
}
