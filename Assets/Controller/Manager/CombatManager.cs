using Assets.Controller.Character;
using Assets.Controller.Map.Combat;
using Assets.Model.Character.Enum;
using Assets.Model.Event.Combat;
using Assets.Model.Party;
using System.Collections.Generic;

namespace Assets.Controller.Manager
{
    public class CombatManager
    {
        private List<CharController> _characters;
        private CharController _currentlyActing;
        private MapController _map;
        private List<MParty> _lParties;
        private List<MParty> _rParties;

        private static CombatManager _instance;
        public static CombatManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CombatManager();
                return _instance;
            }
        }

        public CombatManager()
        {
            this._characters = new List<CharController>();
            this._lParties = new List<MParty>();
            this._rParties = new List<MParty>();
        }

        public void Init(MapController map)
        {
            this._map = map;
            this._lParties = map.GetLParties();
            this._rParties = map.GetRParties();
            foreach(var party in this._lParties)
            {
                this._characters.AddRange(party.GetChars());
            }
            foreach (var party in this._rParties)
            {
                this._characters.AddRange(party.GetChars());
            }
            if (this._characters.Count > 0)
            {
                this._characters.Sort(
                    (x, y) =>
                    x.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)
                    .CompareTo(y.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)));
                var acting = new EvTakingAction();
                var data = new EvTakingActionData();
                data.Target = this._characters[0];
                acting.SetData(data);
                acting.TryProcess();
            }
            
        }

        public void SetCurrentlyActing(CharController c)
        {
            this._currentlyActing = c;
        }
    }
}
//        private List<Pair<int, CastingEvent>> _castingOrder;
//        private List<TileController> _curTiles;
//        private List<CharController> _lParty;
//        private List<CharController> _order;
//        private List<CharController> _rParty;

//        public List<CharController> Characters;
//        public MAbility CurAbility { get; set; }
//        public CharController CurrActing { get; set; }
//        public MMap Map;

//        public CombatManager(MMap m)
//        {
//            this._castingOrder = new List<Pair<int, CastingEvent>>();
//            this.Characters = new List<CharController>();
//            this._curTiles = new List<TileController>();
//            this.Map = m;
//            this._order = new List<CharController>();
//        }

//        public void AddCasting(CastingEvent e)
//        {
//            this._castingOrder.Add(new Pair<int, CastingEvent>(e.CastTime, e));
//            this._castingOrder.Sort((x, y) => x.X.CompareTo(y.X));
//            var remove = this._order.Find(x => x.Model == e.Caster.Model);
//            if (remove != null)
//                this._order.Remove(remove);
//        }

//        public void InitParties(List<CharController> l, List<CharController> r)
//        {
//            this._lParty = l;
//            this._rParty = r;
//            foreach (var c in this._rParty) { this.Characters.Add(c); }
//            foreach (var c in this._lParty) { this.Characters.Add(c); }
//            this.InitCharacterTurns();
//        }

//        public List<TileController> GetPathTileControllers(ShowPotentialPathEvent e)
//        {
//            var hexPath = this.Map.GetPath(e.Character.CurrentTile.Model, e.Target.Model);
//            var tileControllers = new List<TileController>();
//            foreach (var hex in hexPath.Tiles)
//            {
//                tileControllers.Add(hex.Parent);
//            }
//            this._curTiles.Clear();
//            return tileControllers;
//        }

//        public Path GetPath(TileController s, TileController t)
//        {
//            return this.Map.GetPath(s.Model, t.Model);
//        }

//        public void ProcessCharacterKilled(CharController c)
//        {
//            this.Characters.Remove(c);
//            if (this._lParty.Contains(c))
//                this._lParty.Remove(c);
//            else
//                this._rParty.Remove(c);
//            this._order.Remove(c);
//        }

//        public void ProcessNextTurn()
//        {
//            if (this._order.Count > 0)
//                this._order.RemoveAt(0);
//            if (this._order.Count > 0)
//            {
//                if (this._castingOrder.Count > 0)
//                {
//                    if (this._castingOrder[0].X + this._castingOrder[0].Y.Caster.Model.GetCurrentStatValue(ESecondaryStat.Initiative) <
//                        this._order[0].Model.GetCurrentStatValue(ESecondaryStat.Initiative))
//                    {
//                        var cast = this._castingOrder[0];
//                        this._castingOrder.RemoveAt(0);
//                        cast.Y.DoneCasting();
//                    }
//                    else
//                    {
//                        var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
//                    }
//                }
//                else
//                {
//                    var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
//                }
//            }
//            else if (this._castingOrder.Count > 0)
//            {
//                var cast = this._castingOrder[0];
//                this._castingOrder.RemoveAt(0);
//                cast.Y.DoneCasting();
//            }
//            else
//            {
//                this.InitCharacterTurns();
//                this.ProcessEndOfTurn();
//            }
//        }

//        public void ResetTileControllerFlags()
//        {
//            foreach (var tile in this._curTiles) { TileControllerFlags.SetAllFlagsFalse(tile.Flags); }
//        }

//        public void SetCurrentTargetTiles(List<TileController> tiles)
//        {
//            this._curTiles = tiles;
//        }

//        public bool TargetsOnSameTeam(CharController s, CharController t)
//        {
//            if (this._lParty.Contains(s) && this._lParty.Contains(t))
//                return true;
//            else if (this._rParty.Contains(s) && this._rParty.Contains(t))
//                return true;
//            else
//                return false;
//        }

//        private void InitCharacterTurns()
//        {
//            foreach (var character in this.Characters)
//            {
//                this._order.Add(character);
//                character.Model.SetCurrentAP((int)character.Model.GetCurrentStatValue(ESecondaryStat.AP));
//            }
//            this._order.Sort((x, y) => y.Model.SecondaryStats.Initiative.CompareTo(x.Model.SecondaryStats.Initiative));
//            if (this._order != null && this._order.Count > 0)
//            {
//                var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
//            }
//        }

//        private void ProcessEndOfTurn()
//        {
//            // TODO:
//        }
//    }
//}
