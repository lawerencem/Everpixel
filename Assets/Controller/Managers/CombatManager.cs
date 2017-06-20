using Assets.Generics;
using Controller.Characters;
using Controller.Managers;
using Controller.Map;
using Model.Abilities;
using Model.Characters;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;

namespace Assets.Controller.Managers
{
    public class CombatManager
    {
        private List<Pair<int, CastingEvent>> _castingOrder;
        private List<TileController> _curTiles;
        private List<GenericCharacterController> _lParty;
        private CombatMap _map;
        private List<GenericCharacterController> _order;
        private List<GenericCharacterController> _rParty;

        public List<GenericCharacterController> Characters;
        public GenericAbility CurAbility { get; set; }
        public GenericCharacterController CurrActing { get; set; }

        public CombatManager(CombatMap m)
        {
            this._castingOrder = new List<Pair<int, CastingEvent>>();
            this.Characters = new List<GenericCharacterController>();
            this._curTiles = new List<TileController>();
            this._map = m;
            this._order = new List<GenericCharacterController>();
        }

        public void AddCasting(CastingEvent e)
        {
            this.ResetTileControllerFlags();
            this._castingOrder.Add(new Pair<int, CastingEvent>(e.CastTime, e));
            this._castingOrder.Sort((x, y) => x.X.CompareTo(y.X));
            var remove = this._order.Find(x => x.Model == e.Caster.Model);
            if (remove != null)
                this._order.Remove(remove);
        }

        public void InitParties(List<GenericCharacterController> l, List<GenericCharacterController> r)
        {
            this._lParty = l;
            this._rParty = r;
            foreach (var c in this._rParty) { this.Characters.Add(c); }
            foreach (var c in this._lParty) { this.Characters.Add(c); }
            this.InitCharacterTurns();
        }

        public List<TileController> GetAttackTiles(AttackSelectedEvent e)
        {
            this.ResetTileControllerFlags();
            int distMod = 0;
            if (e.AttackType.GetType().Equals(typeof(WeaponAbilitiesEnum)))
                distMod += WeaponAbilityTable.Instance.Table[e.AttackType].Range;
            else if (e.AttackType.GetType().Equals(typeof(ActiveAbilitiesEnum)))
                distMod += ActiveAbilityTable.Instance.Table[e.AttackType].Range;
            
            if (e.RWeapon)
            {
                if (CurrActing.Model.RWeapon != null)
                    distMod += (int)CurrActing.Model.RWeapon.RangeMod;
            }
            else
            {
                if (CurrActing.Model.LWeapon != null)
                    distMod += (int)CurrActing.Model.LWeapon.RangeMod;
            }
            var hexTiles = this._map.GetAoETiles(this.CurrActing.CurrentTile.Model, distMod);
            var tileControllers = new List<TileController>();
            if (e.TileSelectable)
            {
                foreach (var hex in hexTiles)
                {
                    tileControllers.Add(hex.Parent);
                    TileControllerFlags.SetPotentialTileSelectFlagTrue(hex.Parent.Flags);
                }
            }
            else
            {
                foreach (var hex in hexTiles)
                {
                    tileControllers.Add(hex.Parent);
                    TileControllerFlags.SetPotentialAttackFlagTrue(hex.Parent.Flags);
                }
            }
            this._curTiles = tileControllers;
            return tileControllers;
        }

        public List<TileController> GetPathTileControllers(ShowPotentialPathEvent e)
        {
            this.ResetTileControllerFlags();
            var hexPath = this._map.GetPath(e.Character.CurrentTile.Model, e.Target.Model);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexPath.Tiles)
            {
                tileControllers.Add(hex.Parent);
            }
            this._curTiles.Clear();
            return tileControllers;
        }

        public Path GetPath(TileController s, TileController t)
        {
            return this._map.GetPath(s.Model, t.Model);
        }

        public void ProcessCharacterKilled(GenericCharacterController c)
        {
            this.Characters.Remove(c);
            if (this._lParty.Contains(c))
                this._lParty.Remove(c);
            else
                this._rParty.Remove(c);
            this._order.Remove(c);
        }

        public void ProcessNextTurn()
        {
            if (this._order.Count > 0)
                this._order.RemoveAt(0);
            if (this._order.Count > 0)
            {
                if (this._castingOrder.Count > 0)
                {
                    if (this._castingOrder[0].X + this._castingOrder[0].Y.Caster.Model.GetCurrentStatValue(SecondaryStatsEnum.Initiative) <
                        this._order[0].Model.GetCurrentStatValue(SecondaryStatsEnum.Initiative))
                    {
                        var cast = this._castingOrder[0];
                        this._castingOrder.RemoveAt(0);
                        cast.Y.DoneCasting();
                    }
                    else
                    {
                        var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
                    }
                }
                else
                {
                    var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
                }
            }
            else if (this._castingOrder.Count > 0)
            {
                var cast = this._castingOrder[0];
                this._castingOrder.RemoveAt(0);
                cast.Y.DoneCasting();
            }
            else
            {
                this.InitCharacterTurns();
                this.ProcessEndOfTurn();
            }
        }

        public bool TargetsOnSameTeam(GenericCharacterController s, GenericCharacterController t)
        {
            if (this._lParty.Contains(s) && this._lParty.Contains(t))
                return true;
            else if (this._rParty.Contains(s) && this._rParty.Contains(t))
                return true;
            else
                return false;
        }

        private void InitCharacterTurns()
        {
            foreach (var character in this.Characters)
            {
                this._order.Add(character);
                character.Model.CurrentAP = character.Model.GetCurrentStatValue(SecondaryStatsEnum.AP);
            }
            this._order.Sort((x, y) => y.Model.SecondaryStats.Initiative.CompareTo(x.Model.SecondaryStats.Initiative));
            if (this._order != null && this._order.Count > 0)
            {
                var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
            }
        }

        private void ProcessEndOfTurn()
        {
            foreach(var c in this.Characters)
            {
                c.Model.RestoreStamina();
            }
        }

        private void ResetTileControllerFlags()
        {
            foreach (var tile in this._curTiles) { TileControllerFlags.SetAllFlagsFalse(tile.Flags); }
        }
    }
}
