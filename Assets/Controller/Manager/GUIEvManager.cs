using Assets.View.Event;
using System.Collections.Generic;
using Template.Event;

namespace Assets.Controller.Managers
{
    public class GUIEvManager : AEventManager<MGuiEv>
    {
        //private Dictionary<string, GameObject> _btns;

        public GUIEvManager()
        {
            this._events = new List<MGuiEv>();
            //this._btns = new Dictionary<string, GameObject>();
            //for (int i = 0; i < 7; i++)
            //{
            //    var tag = "WpnBtnTag" + i;
            //    var btn = GameObject.FindGameObjectWithTag(tag);
            //    this._btns.Add(tag, btn);
            //}
        }

        private static GUIEvManager _instance;
        public static GUIEvManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GUIEvManager();
                return _instance;
            }
        }

        public void Update()
        {
            foreach (var e in this._events)
            {
                this.TryProcessEvent(e);
            }
        }

        public override void RegisterEvent(MGuiEv e)
        {
            this._events.Add(e);
            this._events.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            e.AddCallback(this.RemoveEvent);
            this.TryProcessEvent(e);
        }

        public void RemoveEvent(object o)
        {
            if (o.GetType().Equals(typeof(MGuiEv)))
            {
                var e = o as MGuiEv;
                this._events.Remove(e);
            }
        }

        protected override void TryProcessEvent(MGuiEv e)
        {
            e.TryProcess();
        }

        //private void HandleEndTurnEvent(GUIEndTurnEvent e)
        //{
        //    this._events.Remove(e);
        //    var end = new EndTurnEvent(CombatEventManager.Instance);
        //}

        //private void HandlePopulateWpnBtnsEvent(PopulateWpnBtnsEvent e)
        //{
        //    this._events.Remove(e);
        //    for (int i = 0; i < 7; i++)
        //    {
        //        var tag = "WpnBtnTag" + i;
        //        if (e.Abilities != null && i < e.Abilities.Count)
        //        {
        //            this._btns[tag].SetActive(true);
        //            var script = this._btns[tag].GetComponent<WpnBtnClick>();
        //            script.SetAbility(e.Abilities[i].X.Type, e.Abilities[i].Y);
        //        }
        //        else
        //        {
        //            this._btns[tag].SetActive(false);
        //        }
        //    }
        //}

        //private void HandleWpnBtnClickEvent(WpnBtnClickEvent e)
        //{
        //    this._events.Remove(e);
        //    var attackSelEvent = new AttackSelectedEvent(CombatEventManager.Instance, e.RWeapon, e.AbilityType);
        //}
    }
}
