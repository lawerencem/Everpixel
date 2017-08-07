//using Model.Abilities;
//using Model.Events.Combat;
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using View.Events;
//using View.GUI;
//using View.Scripts;

//namespace Assets.Controller.Managers
//{
//    public class GUIEventManager
//    {
//        private Dictionary<string, GameObject> _btns;
//        private List<GUIEvent> _events;

//        public GUIEventManager()
//        {
//            this._events = new List<GUIEvent>();
//            this._btns = new Dictionary<string, GameObject>();
//            for (int i = 0; i < 7; i++)
//            {
//                var tag = "WpnBtnTag" + i;
//                var btn = GameObject.FindGameObjectWithTag(tag);
//                this._btns.Add(tag, btn);
//            }
//        }

//        private static GUIEventManager _instance;
//        public static GUIEventManager Instance
//        {
//            get
//            {
//                if (_instance == null)
//                    _instance = new GUIEventManager();
//                return _instance;
//            }
//        }

//        public void Update()
//        {
//            foreach (var e in this._events) { this.TryProcessEvent(e); }
//        }

//        public void RegisterEvent(GUIEvent e)
//        {
//            this._events.Add(e);
//            this.TryProcessEvent(e);
//        }

//        private void TryProcessEvent(GUIEvent e)
//        {
//            switch (e.Type)
//            {
//                case (GUIEventEnum.EndTurn): { this.HandleEndTurnEvent(e as GUIEndTurnEvent); } break;
//                case (GUIEventEnum.PopulateWpnBtns): { this.HandlePopulateWpnBtnsEvent(e as PopulateWpnBtnsEvent); } break;
//                case (GUIEventEnum.WpnBtnClick): { this.HandleWpnBtnClickEvent(e as WpnBtnClickEvent); } break;
//            }
//        }

//        private void HandleEndTurnEvent(GUIEndTurnEvent e)
//        {
//            this._events.Remove(e);
//            var end = new EndTurnEvent(CombatEventManager.Instance);
//        }

//        private void HandlePopulateWpnBtnsEvent(PopulateWpnBtnsEvent e)
//        {
//            this._events.Remove(e);
//            for (int i = 0; i < 7; i++)
//            {
//                var tag = "WpnBtnTag" + i;
//                if (e.Abilities != null && i < e.Abilities.Count)
//                {
//                    this._btns[tag].SetActive(true);
//                    var script = this._btns[tag].GetComponent<WpnBtnClick>();
//                    script.SetAbility(e.Abilities[i].X.Type, e.Abilities[i].Y);
//                }
//                else
//                {
//                    this._btns[tag].SetActive(false);
//                }   
//            }
//        }
        
//        private void HandleWpnBtnClickEvent(WpnBtnClickEvent e)
//        {
//            this._events.Remove(e);
//            var attackSelEvent = new AttackSelectedEvent(CombatEventManager.Instance,e.RWeapon , e.AbilityType );
//        }
//    }
//}
