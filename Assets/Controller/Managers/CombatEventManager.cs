using Assets.Controller.Managers;
using Controller.Managers.Map;
using Generics;
using Generics.Scripts;
using Model.Events;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;

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
                case (CombatEventEnum.HexSelectedForMove): { HandleHexSelectedForMoveEvent(e as HexSelectedForMoveEvent); } break;
                case (CombatEventEnum.MapDoneLoading): { HandleMapDoneLoadingEvent(e as MapDoneLoadingEvent); } break;
                case (CombatEventEnum.PathTraversed): { HandlePathTraversedEvent(e as PathTraversedEvent); } break;
                case (CombatEventEnum.ShowPotentialPath): { HandleShowPotentialPathEvent(e as ShowPotentialPathEvent); } break;
                case (CombatEventEnum.TakingAction): { HandleTakingActionEvent(e as TakingActionEvent); } break;
                case (CombatEventEnum.TileDoubleClick): { HandleTileDoubleClickEvent(e as TileDoubleClickEvent); } break;
                case (CombatEventEnum.TraversePath): { HandleTraversePathEvent(e as TraversePathEvent); } break;
                case (CombatEventEnum.TraverseTile): { HandleTraverseTileEvent(e as TraverseTileEvent); } break;
            }
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
                    var bob = e.Character.Handle.AddComponent<BobbingScript>();
                    bob.Init(0.001f, 0.05f, e.Character.Handle);
                    this._mapGUIController.SetActingBoxToController(e.Character);
                }
                else
                {
                    this._combatManager.ProcessNextTurn();
                }
                this._mapGUIController.ClearPotentialPathView();
                this._events.RemoveAll(x => x.Type == CombatEventEnum.ShowPotentialPath);
            }
        }

        private void HandleShowPotentialPathEvent(ShowPotentialPathEvent e)
        {
            var path = this._combatManager.GetPathTileControllers(e);
            this._mapGUIController.DecoratePath(path);
        }

        private void HandleMapDoneLoadingEvent(MapDoneLoadingEvent e)
        {
            this._events.Remove(e);
            var mapController = new CombatMapGuiController();
            this._combatManager = new CombatManager(e.Map);
            this._combatManager.InitEnemyParty(e.Controllers);
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

        private void HandleTraversePathEvent(TraversePathEvent e)
        {
            this._events.Remove(e);
            var next = e.Path.GetNextTile(e.Character.CurrentTile);
            var traverseTileEvent = new TraverseTileEvent(this, e.Path, e.Character.CurrentTile, next);
            var bob = e.Character.GetComponent<BobbingScript>();
            if (bob != null) { GameObject.Destroy(bob); }
        }

        private void HandleTraverseTileEvent(TraverseTileEvent e)
        {
            this._events.Remove(e);
            var script = e.Character.Handle.AddComponent<TileMoveScript>();
            script.Init(e.Character, e.Path, e.Source, e.Next);
        }

    }
}
