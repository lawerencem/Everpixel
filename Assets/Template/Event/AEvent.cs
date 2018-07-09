using Assets.Template.CB;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Template.Event
{
    public abstract class AEvent
    {
        protected List<Pair<Callback, int>> _callbackPriorityPairs;
        protected int _priority = 0;

        public int Priority { get { return this._priority; } }

        public AEvent()
        {
            this._callbackPriorityPairs = new List<Pair<Callback, int>>();
        }

        public void AddCallback(Callback callback, int priority = 0)
        {
            if (this._callbackPriorityPairs == null)
                this._callbackPriorityPairs = new List<Pair<Callback, int>>();
            this._callbackPriorityPairs.Add(new Pair<Callback, int>(callback, priority));
        }

        public void DoCallbacks()
        {
            if (this._callbackPriorityPairs != null)
            {
                this._callbackPriorityPairs.Sort((x, y) => x.Y.CompareTo(y.Y));
                foreach (var pair in this._callbackPriorityPairs)
                    pair.X(this);
            }
        }

        public abstract void TryProcess();
        public abstract void Register();

        public void SetCallback(Callback callback, int priority = 0)
        {
            this._callbackPriorityPairs = new List<Pair<Callback, int>>();
            this._callbackPriorityPairs.Add(new Pair<Callback, int>(callback, priority));
        }

        public void SetPriority(int p) { this._priority = p; }
    }
}
