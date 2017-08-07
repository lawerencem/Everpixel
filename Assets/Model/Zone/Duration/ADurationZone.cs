using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public abstract class ADurationZone : AZone
    {
        protected int _duration;

        public int Duration { get { return this._duration; } }
        public GameObject Handle { get; set; }

        public ADurationZone(ZoneArgsCont arg) : base(arg)
        {
            this._duration = arg.Dur;
            this.Handle = arg.Handle;
        }

        public void ProcessTurn()
        {
            this._duration--;
        }
    }
}
