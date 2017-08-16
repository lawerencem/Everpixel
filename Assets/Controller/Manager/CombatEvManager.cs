using Template.Event;

namespace Assets.Model.Event.Combat
{
    public class CombatEvManager : AEventManager<MCombatEv>
    {
        private static CombatEvManager _instance;
        public static CombatEvManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CombatEvManager();
                return _instance;
            }
        }

        public override void RegisterEvent(MCombatEv e)
        {
            this._events.Add(e);
            this._events.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            e.AddCallback(this.RemoveEvent);
            this.TryProcessEvent(e);
        }

        public void RemoveEvent(object o)
        {
            if (o.GetType().Equals(typeof(MCombatEv)))
            {
                var e = o as MCombatEv;
                this._events.Remove(e);
            }
        }

        public void Update()
        {
            foreach (var e in this._events)
            {
                this.TryProcessEvent(e);
            }
        }

        protected override void TryProcessEvent(MCombatEv e)
        {
            e.TryProcess();
        }
    }
}
