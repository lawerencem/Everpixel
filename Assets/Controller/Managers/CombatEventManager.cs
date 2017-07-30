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
using Controller.Map;
using Model.Characters;
using Generics.Utilities;

namespace Controller.Managers
{
    public class CombatEventManager
    {
        private const float PER_FRAME = 0.0025f;
        private const float PER_FRAME_DIST = 0.075f;

        private bool _guiLock = false;
        private bool _interactionLock = false;

        private CombatManager _combatManager;
        private CombatMapLoader _mapLoader;
        private List<CombatEvent> _events;

        private PerformActionEvent _currentAction;
        private List<TileController> _currentActionTiles;

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

        public GenericAbility GetCurrentAbility()
        {
            return this._combatManager.CurAbility;
        }

        public GenericCharacterController GetCurrentCharacter()
        {
            return this._combatManager.CurrActing;
        }

        public GenericCharacterController GetRandomCharacter()
        {
            return ListUtil<GenericCharacterController>.GetRandomListElement(this._combatManager.Characters);
        }

        public bool GetGUILock() { return this._guiLock; }
        public bool GetInteractionLock() { return this._interactionLock; }

        public void LockGUI() { this._guiLock = true; }
        public void LockInteraction() { this._interactionLock = true; }

        public void UnlockGUI() { this._guiLock = false; }
        public void UnlockInteraction() { this._interactionLock = false; }

        public void ActionPerformedCallback()
        {
            this.UnlockInteraction();
            this.UnlockGUI();
            CMapGUIController.Instance.ClearDecoratedTiles();
            if (this._currentAction != null)
            {
                CMapGUIController.Instance.SetActingBoxToController(this._currentAction.Container.Source);
                foreach (var hit in this._currentAction.Container.Hits)
                {
                    foreach (var perk in hit.Source.Model.Perks.PostHitPerks)
                        perk.TryProcessAction(hit);
                    foreach (var perk in hit.Target.Model.Perks.PostHitPerks)
                        perk.TryProcessAction(hit);
                    var tileHit = new TileHitEvent(this, hit);
                }
                if (this._currentAction.Container.CastFinished)
                    this._combatManager.ProcessNextTurn();
            }
            this._currentAction = null;
        }

        private void TryProcessEvent(CombatEvent e)
        {
            switch(e.Type)
            {
                case (CombatEventEnum.ApplyInjury): { HandleApplyInjuryEvent(e as ApplyInjuryEvent); } break;
                case (CombatEventEnum.AttackSelected): { HandleAttackSelectedEvent(e as AttackSelectedEvent); } break;
                case (CombatEventEnum.Buff): { HandleBuffEvent(e as BuffEvent); } break;
                case (CombatEventEnum.Casting): { HandleCastingEvent(e as CastingEvent); } break;
                case (CombatEventEnum.CharacterKilled): { HandleCharacterKilledEvent(e as CharacterKilledEvent); } break;
                case (CombatEventEnum.Debuff): { HandleDebuffEvent(e as DebuffEvent); } break;
                case (CombatEventEnum.DisplayAction): { HandleDisplayActionEvent(e as DisplayActionEvent); } break;
                case (CombatEventEnum.DisplayHitStats): { HandleDisplayHitStatsEvent(e as DisplayHitStatsEvent); } break;
                case (CombatEventEnum.DoT): { HandleDoTEvent(e as DoTEvent); } break;
                case (CombatEventEnum.EndTurn): { HandleEndTurnEvent(e as EndTurnEvent); } break;
                case (CombatEventEnum.GenericEffect): { HandleGenericEffect(e as GenericEffectEvent); } break;
                case (CombatEventEnum.HexSelectedForMove): { HandleHexSelectedForMoveEvent(e as HexSelectedForMoveEvent); } break;
                case (CombatEventEnum.HoT): { HandleHoTEvent(e as HoTEvent); } break;
                case (CombatEventEnum.MapDoneLoading): { HandleMapDoneLoadingEvent(e as MapDoneLoadingEvent); } break;
                case (CombatEventEnum.ModifyHP): { HandleModifyHPEvent(e as ModifyHPEvent); } break;
                case (CombatEventEnum.ModifyStam): { HandleModifyStamEvent(e as ModifyStamEvent); } break;
                case (CombatEventEnum.PathTraversed): { HandlePathTraversedEvent(e as PathTraversedEvent); } break;
                case (CombatEventEnum.PerformAction): { HandlePerformActionEvent(e as PerformActionEvent); } break;
                case (CombatEventEnum.PredictAction): { HandlePredictActionEvent(e as PredictActionEvent); } break;
                case (CombatEventEnum.Shapeshift): { HandleShapeshiftEvent(e as ShapeshiftEvent); } break;
                case (CombatEventEnum.Shield): { HandleShieldEvent(e as ShieldEvent); } break;
                case (CombatEventEnum.ShowPotentialPath): { HandleShowPotentialPathEvent(e as ShowPotentialPathEvent); } break;
                case (CombatEventEnum.Summon): { HandleSummonEvent(e as SummonEvent); } break;
                case (CombatEventEnum.TakingAction): { HandleTakingActionEvent(e as TakingActionEvent); } break;
                case (CombatEventEnum.TileDoubleClick): { HandleTileDoubleClickEvent(e as TileDoubleClickEvent); } break;
                case (CombatEventEnum.TileHoverDeco): { HandleTileHoverDecoEvent(e as TileHoverDecoEvent); } break;
                case (CombatEventEnum.TileHitEvent): { HandleTileHitEvent(e as TileHitEvent); } break;
                case (CombatEventEnum.TraversePath): { HandleTraversePathEvent(e as TraversePathEvent); } break;
                case (CombatEventEnum.TraverseTile): { HandleTraverseTileEvent(e as TraverseTileEvent); } break;
            }
        }

        private void HandleApplyInjuryEvent(ApplyInjuryEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ApplyInjuryGraphics(e);
        }

        private void HandleAttackSelectedEvent(AttackSelectedEvent e)
        {
            this._events.Remove(e);
            this._currentActionTiles = new List<TileController>();
            var ability = GenericAbilityTable.Instance.Table[e.AttackType];
            this._currentActionTiles = ability.GetTargetTiles(e, this._combatManager.CurrActing, this._combatManager);
            this._combatManager.SetCurrentTargetTiles(this._currentActionTiles);
            CMapGUIController.Instance.DecoratePotentialAttackTiles(this._currentActionTiles);
            foreach (var tile in this._currentActionTiles)
            {
                TileControllerFlags.SetAwaitingActionFlagTrue(tile.Flags);
            }

            this._combatManager.CurAbility = ability;
        }

        private void HandleBuffEvent(BuffEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayBuff(e);
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

        private void HandleDebuffEvent(DebuffEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayDebuff(e);
        }

        private void HandleCharacterKilledEvent(CharacterKilledEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ProcessCharacterKilled(e);
            this._combatManager.ProcessCharacterKilled(e.Killed);
        }

        private void HandleDisplayActionEvent(DisplayActionEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayActionEvent(e);
        }

        private void HandleDisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayHitStatsEvent(e);
        }

        private void HandleDoTEvent(DoTEvent e)
        {
            this._events.Remove(e);
            var dotType = e.DoT.Type.ToString().Replace("_", " ");
            CMapGUIController.Instance.DisplayText(dotType, e.ToDoT.Handle, CMapGUIControllerParams.RED);
        }

        private void HandleEndTurnEvent(EndTurnEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ClearDecoratedTiles();
            CMapGUIController.Instance.ProcessNewTurn();
            var cur = this._combatManager.CurrActing.Handle;
            this._combatManager.CurrActing.Model.ProcessEndOfTurn();
            var bob = cur.GetComponent<BobbingScript>();
            if (bob != null) { bob.Reset(); }
            this._combatManager.CurAbility = null;
            this._combatManager.ProcessNextTurn();
            this.PopulateBtnsHelper();
        }

        private void HandleGenericEffect(GenericEffectEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayGenericEffect(e);
        }

        private void HandleHexSelectedForMoveEvent(HexSelectedForMoveEvent e)
        {
            this._events.Remove(e);
            if (this._combatManager != null)
            {
                var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Selected, this);
            }
        }

        private void HandleHoTEvent(HoTEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayText("HoT", e.ToHoT.Handle, CMapGUIControllerParams.GREEN);
        }

        private void HandleMapDoneLoadingEvent(MapDoneLoadingEvent e)
        {
            this._events.Remove(e);
            this._combatManager = new CombatManager(e.Map);
            this._combatManager.InitParties(e.LParty, e.RParty);
            this._mapLoader = e.MapLoader;
            this.PopulateBtnsHelper();
        }

        private void HandleModifyHPEvent(ModifyHPEvent e)
        {
            this._events.Remove(e);
        }

        private void HandleModifyStamEvent(ModifyStamEvent e)
        {
            this._events.Remove(e);
        }

        private void HandlePathTraversedEvent(PathTraversedEvent e)
        {
            this._events.Remove(e);
            {
                if (e.Character.Model.GetCurrentHP() > 0)
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
            this._combatManager.ResetTileControllerFlags();
            this.HandlePerformActionEventHelper(e);
        }

        private void HandlePerformActionEventHelper(PerformActionEvent e)
        {
            if (this._combatManager.CurAbility != null && this._combatManager.CurAbility != null)
            {
                e.Container.Action = this._combatManager.CurAbility;
                e.Container.Source = this._combatManager.CurrActing;
                this._combatManager.CurAbility = null;
                if (e.ValidAction())
                {
                    this._currentAction = e;
                    e.Perform();
                }
            }
        }

        private void HandlePredictActionEvent(PredictActionEvent e)
        {
            this._events.Remove(e);
        }

        private void HandleShieldEvent(ShieldEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.DisplayText("Shield", e.ToShield.Handle, CMapGUIControllerParams.BLUE);
        }

        private void HandleShowPotentialPathEvent(ShowPotentialPathEvent e)
        {
            this._events.Remove(e);
            var path = this._combatManager.GetPathTileControllers(e);
            CMapGUIController.Instance.DecoratePath(path);
        }

        private void HandleShapeshiftEvent(ShapeshiftEvent e)
        {
            this._events.Remove(e);
            var shift = new DisplayHitStatsEvent(CombatEventManager.Instance, e.Hit, e.Hit.Done);
        }

        private void HandleSummonEvent(SummonEvent e)
        {
            this._events.Remove(e);
            var summonParams = SummonFactory.Instance.CreateNewObject(e);
            var attemptedTile = e.Hit.TargetTile;
            var tgtTile = attemptedTile.GetNearestEmptyTile();
            this._mapLoader.BuildAndPlaceCharacter(summonParams, ref this._combatManager.Characters, tgtTile, e.Hit.Source.LParty);
            e.Hit.Done();
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

        private void HandleTileHitEvent(TileHitEvent e)
        {
            this._events.Remove(e);
        }

        private void HandleTileHoverDecoEvent(TileHoverDecoEvent e)
        {
            this._events.Remove(e);
            CMapGUIController.Instance.ClearAoETiles();
            CMapGUIController.Instance.DecorateHover(e.Tile);
            CMapGUIController.Instance.DecorateAoETiles(e.AoETiles);
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
            var abs = new List<Pair<GenericAbility, bool>>();

            if (curr.Type == CharacterTypeEnum.Humanoid)
            {
                if (curr.LWeapon != null)
                    foreach (var ab in curr.LWeapon.Abilities)
                        abs.Add(new Pair<GenericAbility, bool>(ab, false));
                if (curr.RWeapon != null)
                    foreach (var ab in curr.RWeapon.Abilities)
                        abs.Add(new Pair<GenericAbility, bool>(ab, true));
            }
            else
            {
                foreach (var ab in curr.DefaultWpnAbilities)
                    abs.Add(new Pair<GenericAbility, bool>(ab, true));
            }
            CMapGUIController.Instance.ProcessNewTurn();
            var populateWpnBtns = new PopulateWpnBtnsEvent(abs, GUIEventManager.Instance);
        }
    }
}
