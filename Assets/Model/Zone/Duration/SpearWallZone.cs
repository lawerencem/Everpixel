using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Event.Combat;
using Assets.Model.Map.Tile;
using Assets.Template.CB;
using Assets.Template.Script;
using Assets.View;

namespace Assets.Model.Zone.Duration
{
    public class ZoneSpearWallData : DurationZoneData
    {
        public bool LWeapon { get; set; }
        public CWeapon ParentWeapon { get; set; }
        public CTile ParentTile { get; set; }
    }

    public class SpearWallZone : ADurationZone
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

        public override void ProcessEnterZone(CChar target, Callback callback)
        {
            this.FirstSpearWallHit = false;
            this.SpearWallHit = false;
            base.ProcessEnterZone(target, callback);
            if (this._spearWallData.Source != null)
            {
                if (target.Proxy.LParty != this._spearWallData.Source.Proxy.LParty)
                {
                    callback(this);
                    var data = new ActionData();
                    data.Ability = EAbility.Pierce;
                    data.LWeapon = this._spearWallData.LWeapon;
                    data.ParentWeapon = this._spearWallData.ParentWeapon;
                    data.Source = this._spearWallData.Source;
                    data.Target = target.Tile;
                    data.WpnAbility = true;
                    this._action = new MAction(data);
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

        public void SetSpearWallZoneData(ZoneSpearWallData data)
        {
            this._spearWallData = data;
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
                    data.StamCost = 0;
                    data.Source = tgt.Tile;
                    data.Target = tgtTile.Controller;
                    var bob = data.Char.GameHandle.GetComponent<SBob>();
                    if (bob != null)
                        bob.Reset();
                    var e = new EvTileMove(data);
                    e.AddCallback(this.AddBob);
                    e.TryProcess();
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
                    }
                }
            }
        }

        private void HandleSpeared(object o)
        {
            bool alreadySpearwalled = false;
            foreach (var zone in this._spearWallData.ParentTile.GetZones())
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
    }
}
