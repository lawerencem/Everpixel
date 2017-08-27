using Assets.Controller.Character;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Tile;
using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Character.Enum;
using Assets.Model.Event.Combat;
using System.Collections.Generic;

namespace Assets.Controller.Manager.Combat
{
    public class CombatManager
    {
        private CombatManagerData _data;

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

        public EAbility GetCurrentAbility() { return this._data.CurrentAbility; }
        public CharController GetCurrentlyActing() { return this._data.CurrentlyActing; }
        public bool GetLWeapon() { return this._data.LWeapon; }
        public List<TileController> GetPotentialTgtTiles() { return this._data.PotentialTgtTiles; }

        public void SetCurrentAbility(EAbility a) { this._data.CurrentAbility = a; }
        public void SetCurrentlyActing(CharController c) { this._data.CurrentlyActing = c; }
        public void SetLWeapon(bool lWeapon) { this._data.LWeapon = lWeapon; }
        public void SetPotentialTgtTiles(List<TileController> t) { this._data.PotentialTgtTiles = t; }

        public CombatManager()
        {
            this._data = new CombatManagerData();
        }

        public void Init(MMapController map)
        {
            this._data.Map = map;
            this._data.LParties = map.GetLParties();
            this._data.RParties = map.GetRParties();
            foreach(var party in this._data.LParties)
            {
                this._data.Characters.AddRange(party.GetChars());
            }
            foreach (var party in this._data.RParties)
            {
                this._data.Characters.AddRange(party.GetChars());
            }
            if (this._data.Characters.Count > 0)
            {
                this._data.Characters.Sort(
                    (x, y) =>
                    y.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)
                    .CompareTo(x.Model.GetCurrentStats().GetStatValue(ESecondaryStat.Initiative)));
            }
            foreach (var character in this._data.Characters)
            {
                this._data.InitiativeOrder.Add(character);

                // TODO: Try reading .xml values, or set to max...
                character.Model.SetCurrentHP(character.Model.GetCurrentStats().GetSecondaryStats().MaxHP);
                character.Model.SetCurrentAP(character.Model.GetCurrentStats().GetSecondaryStats().MaxAP);
                character.Model.SetCurrentMorale(character.Model.GetCurrentStats().GetSecondaryStats().Morale);
                character.Model.SetCurrentStam(character.Model.GetCurrentStats().GetSecondaryStats().Stamina);
            }
                
            this.ProcessTakingAction();
        }

        public bool IsValidActionClick(TileController t)
        {
            if (t.Current != null && t.Current.GetType().Equals(typeof(CharController)))
            {
                var ability = AbilityTable.Instance.Table[this._data.CurrentAbility];
                var target = t.Current as CharController;
                var tile = this._data.PotentialTgtTiles.Find(x => x.Equals(t));
                if (tile != null)
                {
                    if (this._data.CurrentlyActing.Model.LParty == target.Model.LParty)
                    {
                        if (!ability.Data.Hostile)
                            return true;
                    }
                    else
                    {
                        if (ability.Data.Hostile)
                            return true;
                    }
                }
            }
            return false;
        }

        public void ProcessEndTurn()
        {
            this._data.CurrentlyActing.Model.ProcessEndOfTurn();
            this._data.InitiativeOrder.Remove(this._data.CurrentlyActing);
            if (this._data.InitiativeOrder.Count > 0)
            {
                this.ProcessTakingAction();
            }
            else
            {
                var e = new EvNewRound();
                e.TryProcess();
            }
        }

        public void ProcessNewRound()
        {
            this._data.InitiativeOrder.Clear();
            foreach (var character in this._data.Characters)
                this._data.InitiativeOrder.Add(character);
            this.ProcessTakingAction();
        }

        private void ProcessTakingAction()
        {
            if (this._data.InitiativeOrder.Count > 0)
            {
                var data = new EvTakingActionData();
                data.Target = this._data.InitiativeOrder[0];
                var acting = new EvTakingAction(data);
                acting.TryProcess();
            }
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
