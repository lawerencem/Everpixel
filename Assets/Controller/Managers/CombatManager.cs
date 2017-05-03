using Controller.Characters;
using Controller.Managers;
using Controller.Managers.Map;
using Controller.Map;
using Generics;
using Generics.Hex;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;

namespace Assets.Controller.Managers
{
    public class CombatManager
    {
        private List<GenericCharacterController> _enemies;
        private CombatMap _map;
        private List<GenericCharacterController> _order;

        public bool PlayersTurn = true;

        public GenericCharacterController CurrActing { get; set; }

        public CombatManager(CombatMap m)
        {
            this._map = m;
            this._order = new List<GenericCharacterController>();
        }

        public void InitEnemyParty(List<GenericCharacterController> c)
        {
            this._enemies = c;
            this.InitCharacterTurns();
        }

        public List<TileController> GetPathTileControllers(ShowPotentialPathEvent e)
        {
            var hexPath = this._map.GetPath(e.Character.CurrentTile.Model, e.Target.Model);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexPath.Tiles) { tileControllers.Add(hex.Parent); }
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
            foreach (var enemy in this._enemies)
            {
                this._order.Add(enemy);
                enemy.Model.CurrentAP = enemy.Model.SecondaryStats.MaxAP;
            }
            this._order.Sort((x, y) => x.Model.SecondaryStats.Initiative.CompareTo(y.Model.SecondaryStats.Initiative));
            if (this._order != null && this._order.Count > 0)
            {
                var e = new TakingActionEvent(CombatEventManager.Instance, this._order[0]);
            }
        }
    }
}
