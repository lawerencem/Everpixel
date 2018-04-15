using Assets.Controller.Character;
using Assets.Model.Effect;
using Assets.Template.CB;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneData
    {
        public MEffect Effect { get; set; }
        public GameObject Handle { get; set; }
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
        public EZone Type { get { return this._type; } }

        public AZone(EZone type) { this._type = type; }
        public virtual void ProcessEnterZone(CChar target, Callback cb) { }
        public virtual void ProcessExitZone(CChar target, Callback cb) { }
        public virtual void ProcessTurnInZone(CChar target, Callback cb) { }
    }
}
