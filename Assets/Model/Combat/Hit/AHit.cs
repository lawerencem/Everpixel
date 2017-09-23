using Assets.Controller.Character;
using Assets.Model.Character.Enum;
using Assets.Template.CB;
using Assets.View;
using System.Collections.Generic;

namespace Assets.Model.Combat.Hit
{
    public class AHit : ICallback, ICallbackHandler
    {
        private bool _done;
        protected List<Callback> _callbacks;
        protected HitData _data;
        protected List<HitDisplayData> _displays;

        public HitData Data { get { return this._data; } }
        public bool Done { get { return this._done; } }

        public AHit(HitData d)
        {
            this._callbacks = new List<Callback>();
            this._data = d;
            this._displays = new List<HitDisplayData>();
        }

        public void AddCallback(Callback callback)
        {
            this._callbacks.Add(callback);
        }

        public void AddDataDisplay(HitDisplayData d)
        {
            this._displays.Add(d);
        }

        public void CallbackHandler(object o)
        {
            this._done = true;
            this.CleanDisplayData();
            this.DisplayData(this);
        }

        public void DoCallbacks()
        {
            foreach (var callback in this._callbacks)
                callback(this);
        }

        public void SetCallback(Callback callback)
        {
            this._callbacks = new List<Callback>() { callback };
        }

        private void CleanDisplayData()
        {
            this._displays.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        }

        private void DisplayData(object o)
        {
            if (this._displays.Count > 0)
            {
                int index = this._displays.Count - 1;
                this._displays[index].AddCallback(this.DisplayData);
                this._displays[index].Display();
                this._displays.RemoveAt(index);
            }
            else
                this.DoCallbacks();
        }
    }
}
