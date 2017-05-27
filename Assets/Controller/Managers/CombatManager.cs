using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Controller.Map;
using Generics;
using Generics.Hex;
using Model.Abilities;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Managers
{
    public class CombatManager
    {
        private List<TileController> _curTiles;
        private List<GenericCharacterController> _characters;
        private CombatMap _map;
        private List<GenericCharacterController> _order;

        public bool PlayersTurn = true;

        public GenericCharacterController CurrActing { get; set; }

        public CombatManager(CombatMap m)
        {
            this._curTiles = new List<TileController>();
            this._map = m;
            this._order = new List<GenericCharacterController>();
        }

        public void InitParties(List<GenericCharacterController> c)
        {
            this._characters = c;
            this.InitCharacterTurns();
        }

        public List<TileController> GetAttackTiles(AttackSelectedEvent e)
        {
            this.ResetTileControllerFlags();
            var proto = WeaponAbilityTable.Instance.Table[e.Type];
            int distMod = 0;
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
            var hexTiles = this._map.GetAoETiles(this.CurrActing.CurrentTile.Model, proto.Range + distMod);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexTiles)
            {
                tileControllers.Add(hex.Parent);
                TileControllerFlags.SetPotentialAttackFlagTrue(hex.Parent.Flags);
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

        public void ProcessNextTurn()
        {
            this._order.RemoveAt(0);
            if (this._order.Count > 0)
            {
                var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
            }
            else
            {
                this.InitCharacterTurns();
            }
        }

        private void InitCharacterTurns()
        {
            foreach (var character in this._characters)
            {
                this._order.Add(character);
                character.Model.CurrentAP = character.Model.SecondaryStats.MaxAP;
            }
            this._order.Sort((x, y) => y.Model.SecondaryStats.Initiative.CompareTo(x.Model.SecondaryStats.Initiative));
            if (this._order != null && this._order.Count > 0)
            {
                var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
            }
        }

        private void ResetTileControllerFlags()
        {
            foreach (var tile in this._curTiles) { TileControllerFlags.SetAllFlagsFalse(tile.Flags); }
        }
    }
}
