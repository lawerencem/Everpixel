using Assets.Controller.Map.Tile;
using Assets.Model.AI.Role;
using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Zone;
using Assets.Model.Zone.Duration;
using Assets.Model.Zone.Perpetual;
using Assets.Template.Hex;
using Assets.View.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Character
{
    public class CChar : IHexOccupant
    {
        private GameObject _handle;
        private PChar _proxy;
        private MRole _role;
        private CTile _tile;
        private VChar _view;

        public GameObject GameHandle { get { return this._handle; } }
        public PChar Proxy { get { return this._proxy; } }
        public MRole Role { get { return this._role; } }
        public CTile Tile { get { return this._tile; } }
        public VChar View { get { return this._view; } }

        public void SetProxy(PChar p) { this._proxy = p; p.SetController(this); }
        public void SetView(VChar v) { this._view = v; }
        public void SetTile(CTile t) { this._tile = t; }

        public void SetCurrentHex(IHex hex)
        {
            this._proxy.GetModel().SetCurrentHex(hex);
        }

        public void ProcessEnterNewTile(CTile tile)
        {
            AZone spearWallProto = null;
            if (FActionStatus.HasFlag(this._proxy.GetActionFlags().CurFlags, FActionStatus.Flags.Spearwalling))
                spearWallProto = this._proxy.GetZones().Find(x => x.Type == EZone.Spear_Wall_Zone);
            var deleteZones = new List<AZone>();
            foreach (var zone in this.Proxy.GetZones())
                if (zone.GetData().DependsOnSourceChar && zone.GetData().Source.Equals(this))
                    deleteZones.Add(zone);
            foreach (var zone in deleteZones)
                zone.RemoveFromParentAndSource();
            foreach (var neighbor in tile.GetAdjacent())
            {
                if (spearWallProto != null)
                    this.ProcessSpearwallZone(neighbor, spearWallProto);
                this.ProcessAttackOfOpportunityZone(neighbor);
            }
            this.SetTile(tile);
        }

        public List<GameObject> Embedded = new List<GameObject>();
        public List<GameObject> Particles { get; set; }
        public Dictionary<string, GameObject> SubComponents = new Dictionary<string, GameObject>();

        public CChar()
        {
            this._handle = new GameObject("Character");
        }

        private void ProcessAttackOfOpportunityZone(CTile tile)
        {
            var data = new ZoneData();
            data.DependsOnSourceChar = true;
            data.Dur = 1;
            data.Parent = tile;
            data.Source = this;
            var zone = new AttackOfOpportunityZone();
            zone.SetData(data);
        }

        private void ProcessSpearwallZone(CTile tile, AZone spearWallProto)
        {
            var data = new ZoneSpearWallData();
            data.Dur = 1;
            data.Effect = spearWallProto.GetData().Effect;
            data.Handle = spearWallProto.GetData().Handle;
            data.Parent = tile;
            data.Source = spearWallProto.GetData().Source;
            var zone = new SpearWallZone();
            zone.SetSpearWallZoneData(data);
            tile.AddZone(zone);
        }
    }
}

