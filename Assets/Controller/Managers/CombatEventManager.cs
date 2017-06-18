using Assets.Controller.Managers;
using Assets.Generics;
using Model.Abilities;
using Controller.Characters;
using Controller.Managers.Map;
using Generics.Scripts;
using Model.Events;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using View.Events;
using Model.Combat;
using Controller.Map;
using Model.Characters;

namespace Controller.Managers
{
    public class CombatEventManager
    {
        private const float PER_FRAME = 0.0025f;
        private const float PER_FRAME_DIST = 0.075f;

        private bool _guiLock = false;
        private bool _interactionLock = false;

        private CombatManager _combatManager;
        private PerformActionEvent _currentAction;
        private List<CombatEvent> _events;

        public GameObject CameraManager;

        public CombatEventManager()
        {
            this._events = new List<CombatEvent>();
            this.CameraManager = new GameObject();
            this.CameraManager.AddComponent<CameraManager>();
        }

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

        public GenericCharacterController GetCurrentCharacter()
        {
            return this._combatManager.CurrActing;
        }

        public bool GetGUILock() { return this._guiLock; }
        public bool GetInteractionLock() { return this._interactionLock; }

        public void LockGUI() { this._guiLock = true; }
        public void LockInteraction() { this._interactionLock = true; }

        public void UnlockGUI() { this._guiLock = false; }
        public void UnlockInteraction() { this._interactionLock = false; }

        private void ActionPerformedCallback()
        {
            this.UnlockInteraction();
            TileControllerFlags.SetPotentialAttackFlagFalse(this._currentAction.Info.Target.Flags);
            CMapGUIController.Instance.ClearDecoratedTiles();
            CMapGUIController.Instance.SetActingBoxToController(this._currentAction.SourceCharController);
            foreach(var hit in this._currentAction.Info.Hits)
            {
                var dmg = new DamageCharacterEvent(this, hit);
            }
            if (this._currentAction.Info.CastCallback)
                this._combatManager.ProcessNextTurn();
            this._currentAction = null;
        }

        private void TryProcessEvent(CombatEvent e)
        {
            switch(e.Type)
            {
                case (CombatEventEnum.ActionCofirmed): { HandleActionConfirmed(e as ActionConfirmedEvent); } break;
                case (CombatEventEnum.ApplyInjury): { HandleApplyInjuryEvent(e as ApplyInjuryEvent); } break;
                case (CombatEventEnum.AttackSelected): { HandleAttackSelectedEvent(e as AttackSelectedEvent); } break;
                case (CombatEventEnum.Casting): { HandleCastingEvent(e as CastingEvent); } break;
                case (CombatEventEnum.DamageCharacter): { HandleDamageCharacterEvent(e as DamageCharacterEvent); } break;
                case (CombatEventEnum.CharacterKilled): { HandleCharacterKilledEvent(e as CharacterKilledEvent); } break; 
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

        private void HandleActionConfirmed(ActionConfirmedEvent e)
        {
            this._events.Remove(e);
            var info = new PerformActionEventInfo();
            info.Action = this._combatManager.CurAbility;
            info.CombatManager = this._combatManager;
            info.Parent = this;
            info.Source = this._combatManager.CurrActing.CurrentTile;
            info.Target = e.Target;
            var action = new PerformActionEvent(info, this.ActionPerformedCallback);
        }

        private void HandleApplyInjuryEvent(ApplyInjuryEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ApplyInjuryGraphics(e);
        }

        private void HandleAttackSelectedEvent(AttackSelectedEvent e)
        {
            this._events.Remove(e);
            var potentialTiles = this._combatManager.GetAttackTiles(e);
            CMapGUIController.Instance.DecoratePotentialAttackTiles(potentialTiles);

            var ability = new GenericAbility();

            if (e.AttackType.GetType().Equals(typeof(WeaponAbilitiesEnum)))
                ability = GenericAbilityTable.Instance.Table[e.AttackType];
            else if (e.AttackType.GetType().Equals(typeof(ActiveAbilitiesEnum)))
                ability = ActiveAbilityTable.Instance.Table[e.AttackType];

            this._combatManager.CurAbility = ability;
        }

        private void HandleCastingEvent(CastingEvent e)
        {
            this._events.Remove(e);
            this._combatManager.AddCasting(e);
            CMapGUIController.Instance.DisplayCast(e);
            CMapGUIController.Instance.ClearDecoratedTiles();
            var cur = this._combatManager.CurrActing.Handle;
            var bob = cur.GetComponent<BobbingScript>();
            if (bob != null) { bob.Reset(); }
            this._combatManager.CurAbility = null;
            this._combatManager.ProcessNextTurn();
            this.PopulateBtnsHelper();
        }

        private void HandleCharacterKilledEvent(CharacterKilledEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ProcessCharacterKilled(e);
            this._combatManager.ProcessCharacterKilled(e.Killed);
        }

        private void HandleDamageCharacterEvent(DamageCharacterEvent e)
        {
            this._events.Remove(e);
        }

        private void HandleDisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayHitStatsEvent(e);
        }

        private void HandleEndTurnEvent(EndTurnEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ClearDecoratedTiles();
            var cur = this._combatManager.CurrActing.Handle;
            var bob = cur.GetComponent<BobbingScript>();
            if (bob != null) { bob.Reset(); }
            this._combatManager.CurAbility = null;
            this._combatManager.ProcessNextTurn();
            this.PopulateBtnsHelper();
        }

        private void HandleHexSelectedForMoveEvent(HexSelectedForMoveEvent e)
        {
            this._events.Remove(e);
            if (this._combatManager != null)
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
                    if (e.Character == this.GetCurrentCharacter())
                    {
                        CMapGUIController.Instance.SetActingBoxToController(e.Character);
                        var bob = e.Character.Handle.AddComponent<BobbingScript>();
                        bob.Init(PER_FRAME, PER_FRAME_DIST, e.Character.Handle);
                    }
                }
                else
                {
                    var end = new EndTurnEvent(this);
                }
                CMapGUIController.Instance.ClearDecoratedTiles();
                this._events.RemoveAll(x => x.Type == CombatEventEnum.ShowPotentialPath);
            }
        }

        private void HandlePerformActionEvent(PerformActionEvent e)
        {
            this._events.Remove(e);
            this._combatManager.CurAbility = null;

            // TODO: Generate multiple hits as necessaryy
            if (e.Info.Action.CastTime > 0)
            {
                var cast = new CastingEvent(this, e);
            }
            else
            {
                var hit = new HitInfo(e.SourceCharController, e.TargetCharController, e.Info.Action, e.ChildHitDone);
                e.Info.Hits.Add(hit);
                this._currentAction = e;
                e.Info.Action.ProcessAbility(hit);
            }
        }

        private void HandleShowPotentialPathEvent(ShowPotentialPathEvent e)
        {
            this._events.Remove(e);
            var path = this._combatManager.GetPathTileControllers(e);
            CMapGUIController.Instance.DecoratePath(path);
        }

        private void HandleMapDoneLoadingEvent(MapDoneLoadingEvent e)
        {
            this._events.Remove(e);
            this._combatManager = new CombatManager(e.Map);
            this._combatManager.InitParties(e.LParty, e.RParty);
            this.PopulateBtnsHelper();
        }

        private void HandleTakingActionEvent(TakingActionEvent e)
        {
            this._events.Remove(e);
            this._combatManager.CurrActing = e.Controller;
            CMapGUIController.Instance.SetActingBoxToController(e.Controller);
            var bob = e.Controller.Handle.AddComponent<BobbingScript>();
            bob.Init(PER_FRAME, PER_FRAME_DIST, e.Controller.Handle);
            var script = this.CameraManager.GetComponent<CameraManager>();
            script.InitScrollTo(e.Controller.Handle.transform.position);
        }

        private void HandleTileDoubleClickEvent(TileDoubleClickEvent e)
        {
            this._events.Remove(e);
            var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Tile, this);
            var pathTileControllers = this._combatManager.GetPathTileControllers(pathEvent);
            CMapGUIController.Instance.DecoratePath(pathTileControllers);
            var path = this._combatManager.GetPath(this._combatManager.CurrActing.CurrentTile, e.Tile);
            var traversePathEvent = new TraversePathEvent(this, this._combatManager.CurrActing, path);
        }

        private void HandleTileHoverDecoEvent(TileHoverDecoEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DecorateHover(e.Tile);
        }

        private void HandleTraversePathEvent(TraversePathEvent e)
        {
            this._events.Remove(e);
            var bob = e.Character.GetComponent<BobbingScript>();
            if (bob != null) { bob.Reset(); }
            var next = e.Path.GetNextTile(e.Character.CurrentTile);
            var traverseTileEvent = new TraverseTileEvent(this, e.Path, e.Character.CurrentTile, next);
        }

        private void HandleTraverseTileEvent(TraverseTileEvent e)
        {
            this._events.Remove(e);
            var script = e.Character.Handle.AddComponent<TileMoveScript>();
            script.Init(e.Character, e.Path, e.Source, e.Next);
            CMapGUIController.Instance.SetActingBoxToController(e.Character);
        }

        private void PopulateBtnsHelper()
        {
            var curr = this._combatManager.CurrActing.Model;
            var abs = new List<Pair<WeaponAbility, bool>>();

            if (curr.Type == CharacterTypeEnum.Humanoid)
            {
                if (curr.LWeapon != null)
                    foreach (var ab in curr.LWeapon.Abilities)
                        abs.Add(new Pair<WeaponAbility, bool>(ab, false));
                if (curr.RWeapon != null)
                    foreach (var ab in curr.RWeapon.Abilities)
                        abs.Add(new Pair<WeaponAbility, bool>(ab, true));
            }
            else
            {
                foreach (var ab in curr.DefaultWpnAbilities)
                    abs.Add(new Pair<WeaponAbility, bool>(ab, true));
            }
            CMapGUIController.Instance.ProcessNewTurn();
            var populateWpnBtns = new PopulateWpnBtnsEvent(abs, GUIEventManager.Instance);
        }
    }
}
