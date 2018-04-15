using Assets.Controller.Character;
using Assets.Controller.Equipment.Weapon;
using Assets.Controller.Manager.Combat;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
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
    }

    public class SpearWallZone : ADurationZone
    {
        private ZoneSpearWallData _spearWallData;

        public SpearWallZone() : base(EZone.Spear_Wall_Zone) { }

        public override void ProcessEnterZone(CChar target, Callback cb)
        {
            base.ProcessEnterZone(target, cb);
            if (this._spearWallData.Source != null)
            {
                if (target.Proxy.LParty != this._spearWallData.Source.Proxy.LParty)
                {
                    cb(this);
                    var data = new EvPerformAbilityData();
                    data.Ability = EAbility.Pierce;
                    data.LWeapon = this._spearWallData.LWeapon;
                    data.ParentWeapon = this._spearWallData.ParentWeapon;
                    data.Source = this._spearWallData.Source;
                    data.Target = target.Tile;
                    data.WpnAbility = true;
                    var e = new EvPerformAbility(data);
                    e.AddCallback(this.HandleSpeared);
                    e.TryProcess();
                }
            }
        }

        public void HandleSpeared(object o)
        {
            var action = o as MAction;
            foreach (var hit in action.Data.Hits)
            {
                if (!FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Block) &&
                    !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Dodge) &&
                    !FHit.HasFlag(hit.Data.Flags.CurFlags, FHit.Flags.Parry))
                {
                    var tgt = hit.Data.Target.Current as CChar;
                    var tgtTile = hit.Data.Source.Tile.Model.GetPushTile(tgt.Tile.Model);
                    if (tgtTile != null)
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
                        var random = hit.Data.Source.Tile.Model.GetRandomNearbyTile(1) as MTile;
                        if (random != null)
                        {
                            var data = new EvTileMoveData();
                            data.Char = tgt;
                            data.Cost = 0;
                            data.StamCost = 0;
                            data.Source = tgt.Tile;
                            data.Target = random.Controller;
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
    }
}
