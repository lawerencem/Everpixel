using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Effect;
using Assets.Template.CB;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneData
    {
        public MEffect Effect { get; set; }
        public bool DependsOnSourceChar { get; set; }
        public int Dur { get; set; }
        public GameObject Handle { get; set; }
        public CTile Parent { get; set; }
        public EResistType Resist { get; set; }
        public double ResistBase { get; set; }
        public CChar Source { get; set; }
        public List<int> SpriteIndexes { get; set; }
        public string SpritesPath { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public ZoneData()
        {
            this.SpriteIndexes = new List<int>();
        }
    }

    public abstract class AZone
    {
        private EZone _type;

        protected ZoneData _data;

        public EZone Type { get { return this._type; } }

        public AZone(EZone type)
        {
            this._data = new ZoneData();
            this._type = type;
        }

        public virtual void ProcessEnterZone(CChar target, Callback cb) { }
        public virtual void ProcessExitZone(CChar target, bool doAttackOfOpportunity, Callback cb) { }
        public virtual void ProcessTurnInZone(CChar target, Callback cb) { }

        public void RemoveFromParentAndSource()
        {
            if (this._data.DependsOnSourceChar)
            {
                this._data.Parent.RemoveZone(this);
                this._data.Source.Proxy.RemoveZone(this);
            }
        }

        public ZoneData GetData()
        {
            return this._data;
        }

        public void HandleSourceDeath()
        {
            if (this._data.DependsOnSourceChar)
                this.RemoveFromParentAndSource();
        }

        public void ProcessTurn()
        {
            this._data.Dur--;
        }

        public void SetData(ZoneData data)
        {
            this._data = data;
            if (this._data.Parent != null)
                this._data.Parent.AddZone(this);
            if (this._data.Source != null)
                this._data.Source.Proxy.AddZone(this);
        }

        public void SetParent(CTile tile)
        {
            this._data.Parent = tile;
        }
    }
}
