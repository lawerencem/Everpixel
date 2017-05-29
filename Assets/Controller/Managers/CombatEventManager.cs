using Assets.Controller.Managers;
using Assets.Generics;
using Model.Abilities;
using Controller.Characters;
using Controller.Managers.Map;
using Generics;
using Generics.Scripts;
using Model.Abilities;
using Model.Events;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;
using View.Events;
using Model.Combat;

namespace Controller.Managers
{
    public class CombatEventManager
    {
        private CombatManager _combatManager;
        private CombatMapGuiController _mapGUIController;

        public CombatEventManager()
        {
            this._events = new List<CombatEvent>();
            this._mapGUIController = new CombatMapGuiController();
        }

        private List<CombatEvent> _events;

        private static CombatEventManager _instance;
        public static CombatEventManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CombatEventManager();
                return _instance;
            }
        }

        public void Update()
        {
            foreach (var e in this._events) { this.TryProcessEvent(e); }
        }

        public void RegisterEvent(CombatEvent e)
        {
            this._events.Add(e);
            this.TryProcessEvent(e);
        }

        private void TryProcessEvent(CombatEvent e)
        {
            switch(e.Type)
            {
                case (CombatEventEnum.ActionCofirmed): { HandleAttackConfirmedEvent(e as ActionConfirmedEvent); } break;
                case (CombatEventEnum.AttackSelected): { HandleAttackSelectedEvent(e as AttackSelectedEvent); } break;
                case (CombatEventEnum.DisplayHitStats): { HandleDisplayHitStatsEvent(e as DisplayHitStatsEvent); } break;
                case (CombatEventEnum.EndTurn): { HandleEndTurnEvent(e as EndTurnEvent); } break;
                case (CombatEventEnum.HexSelectedForMove): { HandleHexSelectedForMoveEvent(e as HexSelectedForMoveEvent); } break;
                case (CombatEventEnum.MapDoneLoading): { HandleMapDoneLoadingEvent(e as MapDoneLoadingEvent); } break;
                case (CombatEventEnum.PathTraversed): { HandlePathTraversedEvent(e as PathTraversedEvent); } break;
                case (CombatEventEnum.PerformActionEvent): { HandlePerformActionEvent(e as PerformActionEvent); } break;
                case (CombatEventEnum.ShowPotentialPath): { HandleShowPotentialPathEvent(e as ShowPotentialPathEvent); } break;
                case (CombatEventEnum.TakingAction): { HandleTakingActionEvent(e as TakingActionEvent); } break;
                case (CombatEventEnum.TileDoubleClick): { HandleTileDoubleClickEvent(e as TileDoubleClickEvent); } break;
                case (CombatEventEnum.TileHoverDeco): { HandleTileHoverDecoEvent(e as TileHoverDecoEvent); } break;
                case (CombatEventEnum.TraversePath): { HandleTraversePathEvent(e as TraversePathEvent); } break;
                case (CombatEventEnum.TraverseTile): { HandleTraverseTileEvent(e as TraverseTileEvent); } break;
            }
        }

        private void HandleAttackConfirmedEvent(ActionConfirmedEvent e)
        {
            this._events.Remove(e);
            var action = new PerformActionEvent(this, this._combatManager.CurrActing.CurrentTile, e.Target, this._combatManager.CurAbility);
        }

        private void HandleAttackSelectedEvent(AttackSelectedEvent e)
        {
            this._events.Remove(e);
            var potentialTiles = this._combatManager.GetAttackTiles(e);
            this._mapGUIController.DecoratePotentialAttackTiles(potentialTiles);
            var ability = GenericAbilityTable.Instance.Table[e.Type];
            this._combatManager.CurAbility = ability;
        }

        private void HandleDisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            this._events.Remove(e);
            this._mapGUIController.DisplayHitStatsEvent(e);
        }

        private void HandleEndTurnEvent(EndTurnEvent e)
        {
            this._events.Remove(e);
            this._mapGUIController.ClearDecoratedTiles();
            var cur = this._combatManager.CurrActing.Handle;
            var bob = cur.GetComponent<BobbingScript>();
            if (bob != null) { GameObject.Destroy(bob); }
            this._combatManager.ProcessNextTurn();
            this.PopulateBtnsHelper();
        }

        private void HandleHexSelectedForMoveEvent(HexSelectedForMoveEvent e)
        {
            this._events.Remove(e);
            if (this._combatManager != null && this._combatManager.PlayersTurn)
            {
                var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Selected, this);
            }
        }

        private void HandlePathTraversedEvent(PathTraversedEvent e)
        {
            this._events.Remove(e);
            {
                if (e.Character.Model.CurrentAP > 0)
                {
                    this._mapGUIController.SetActingBoxToController(e.Character);
                    var bob = e.Character.Handle.AddComponent<BobbingScript>();
                    bob.Init(0.001f, 0.05f, e.Character.Handle);
                }
                else
                {
                    var end = new EndTurnEvent(this);
                }
                this._mapGUIController.ClearDecoratedTiles();
                this._events.RemoveAll(x => x.Type == CombatEventEnum.ShowPotentialPath);
            }
        }

        private void HandlePerformActionEvent(PerformActionEvent e)
        {
            this._events.Remove(e);
            if (e.Source.Model.Current.GetType() == typeof(GenericCharacterController) &&
                e.Target.Model.Current.GetType() == typeof(GenericCharacterController))
            {
                var source = e.Source.Model.Current as GenericCharacterController;
                var target = e.Target.Model.Current as GenericCharacterController;
                var hit = new HitInfo(source, target, e.Action);
                e.Action.ProcessAbility(hit);
            }
        }

        private void HandleShowPotentialPathEvent(ShowPotentialPathEvent e)
        {
            this._events.Remove(e);
            var path = this._combatManager.GetPathTileControllers(e);
            this._mapGUIController.DecoratePath(path);
        }

        private void HandleMapDoneLoadingEvent(MapDoneLoadingEvent e)
        {
            this._events.Remove(e);
            var mapController = new CombatMapGuiController();
            this._combatManager = new CombatManager(e.Map);
            this._combatManager.InitParties(e.Controllers);
            this.PopulateBtnsHelper();
        }

        private void HandleTakingActionEvent(TakingActionEvent e)
        {
            this._events.Remove(e);
            this._combatManager.CurrActing = e.Controller;
            this._mapGUIController.SetActingBoxToController(e.Controller);
            var bob = e.Controller.Handle.AddComponent<BobbingScript>();
            bob.Init(0.001f, 0.05f, e.Controller.Handle);
        }

        private void HandleTileDoubleClickEvent(TileDoubleClickEvent e)
        {
            this._events.Remove(e);
            if (this._combatManager.PlayersTurn)
            {
                var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Tile, this);
                var pathTileControllers = this._combatManager.GetPathTileControllers(pathEvent);
                this._mapGUIController.DecoratePath(pathTileControllers);
                var path = this._combatManager.GetPath(this._combatManager.CurrActing.CurrentTile, e.Tile);
                var traversePathEvent = new TraversePathEvent(this, this._combatManager.CurrActing, path);
            }
        }

        private void HandleTileHoverDecoEvent(TileHoverDecoEvent e)
        {
            this._events.Remove(e);
            this._mapGUIController.DecorateHover(e.Tile);
        }

        private void HandleTraversePathEvent(TraversePathEvent e)
        {
            this._events.Remove(e);
            var bob = e.Character.GetComponent<BobbingScript>();
            if (bob != null) { GameObject.Destroy(bob); }
            var next = e.Path.GetNextTile(e.Character.CurrentTile);
            var traverseTileEvent = new TraverseTileEvent(this, e.Path, e.Character.CurrentTile, next);
        }

        private void HandleTraverseTileEvent(TraverseTileEvent e)
        {
            this._events.Remove(e);
            var script = e.Character.Handle.AddComponent<TileMoveScript>();
            script.Init(e.Character, e.Path, e.Source, e.Next);
        }

        private void PopulateBtnsHelper()
        {
            var curr = this._combatManager.CurrActing.Model;
            var abs = new List<Pair<WeaponAbility, bool>>();
            if (curr.LWeapon != null)
                foreach(var ab in curr.LWeapon.Abilities)
                    abs.Add(new Pair<WeaponAbility, bool>(ab, false));
            if (curr.RWeapon != null)
                foreach (var ab in curr.RWeapon.Abilities)
                    abs.Add(new Pair<WeaponAbility, bool>(ab, true));

            var populateBtns = new PopulateWpnBtnsEvent(abs, GUIEventManager.Instance);
        }

    }
}
