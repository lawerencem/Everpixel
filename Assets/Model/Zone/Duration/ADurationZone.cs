using Controller.Characters;
using Controller.Map;
using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public abstract class ADurationZone : AZone
    {
        protected int _duration;

        public int Duration { get { return this._duration; } }
        public GameObject Handle { get; set; }

        public ADurationZone(int dur, GameObject handle, TileController tile) : base(tile)
        {
            this._duration = dur;
            this.Handle = handle;
        }

        public void ProcessTurn()
        {
            this._duration--;
        }
    }
}
