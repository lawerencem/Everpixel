using Assets.Controller.Character;
using Assets.Model.Combat;
using System.Collections.Generic;

namespace Assets.Model.Action
{
    public class Attack : AAction
    {
        private List<Hit> _hits;

        public List<Hit> GetHits() { return this._hits; }

        public Attack(CharController origin) : base(origin)
        {
            this._hits = new List<Hit>();
        }

        public void AddHit(Hit hit)
        {
            hit.AddCallback(this.CallbackHandler);
            this._hits.Add(hit);
        }

        public override void CallbackHandler(object o)
        {
            base.CallbackHandler(o);
        }

        public override void DoCallbacks()
        {
            base.DoCallbacks();
        }
    }
}
