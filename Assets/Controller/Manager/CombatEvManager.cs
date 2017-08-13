using Assets.Model.Event.Combat;
using System;
using System.Collections.Generic;
using Template.Event;

namespace Assets.Controller.Manager
{
    public class CombatEvManager : AEventManager<GCombatEv>
    {
        private static CombatEvManager _instance;
        public static CombatEvManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CombatEvManager();
                return _instance;
            }
        }

        public CombatEvManager()
        {
            this._events = new List<GCombatEv>();
        }

        public override void RegisterEvent(GCombatEv t)
        {
            throw new NotImplementedException();
        }

        public override void TryProcessEvent(GCombatEv t)
        {
            throw new NotImplementedException();
        }
    }
}
//    {
//        private const float PER_FRAME = 0.0025f;
//        private const float PER_FRAME_DIST = 0.075f;

//        private bool _guiLock = false;
//        private bool _interactionLock = false;

//        private CombatManager _combatManager;
//        private CombatMapLoader _mapLoader;
//        private List<MCombatEv> _events;

//        private EvPerformAction _currentAction;
//        private List<TileController> _currentActionTiles;

//        public GameObject CameraManager;

//        public CombatEventManager()
//        {
//            this._events = new List<MCombatEv>();
//            this.CameraManager = new GameObject();
//            this.CameraManager.AddComponent<CameraManager>();
//        }



//        public void Update()
//        {
//            foreach (var e in this._events) { this.TryProcessEvent(e); }
//        }

//        public MAbility GetCurrentAbility()
//        {
//            return this._combatManager.CurAbility;
//        }

//        public CharController GetCurrentCharacter()
//        {
//            return this._combatManager.CurrActing;
//        }

//        public CharController GetRandomCharacter()
//        {
//            return ListUtil<CharController>.GetRandomListElement(this._combatManager.Characters);
//        }

//        public bool GetGUILock() { return this._guiLock; }
//        public bool GetInteractionLock() { return this._interactionLock; }

//        public void LockGUI() { this._guiLock = true; }
//        public void LockInteraction() { this._interactionLock = true; }

//        public void UnlockGUI() { this._guiLock = false; }
//        public void UnlockInteraction() { this._interactionLock = false; }

//        public void ActionPerformedCallback()
//        {
//            this.UnlockInteraction();
//            this.UnlockGUI();
//            CMapGUIController.Instance.ClearDecoratedTiles();
//            if (this._currentAction != null)
//            {
//                CMapGUIController.Instance.SetActingBoxToController(this._currentAction.Container.Source);
//                foreach (var hit in this._currentAction.Container.Hits)
//                {
//                    foreach (var perk in hit.Source.Model.Perks.PostHitPerks)
//                        perk.TryProcessAction(hit);
//                    if (hit.Target != null)
//                        foreach (var perk in hit.Target.Model.Perks.PostHitPerks)
//                            perk.TryProcessAction(hit);
//                    var tileHit = new EvTileHit(this, hit);
//                }
//                if (this._currentAction.Container.CastFinished)
//                    this._combatManager.ProcessNextTurn();
//            }
//            this._currentAction = null;
//        }

//        private void TryProcessEvent(MCombatEv e)
//        {
//            switch(e.Type)
//            {
//                case (ECombatEv.ApplyInjury): { HandleApplyInjuryEvent(e as ApplyInjuryEvent); } break;
//                case (ECombatEv.AttackSelected): { HandleAttackSelectedEvent(e as AttackSelectedEvent); } break;
//                case (ECombatEv.Buff): { HandleBuffEvent(e as BuffEvent); } break;
//                case (ECombatEv.Casting): { HandleCastingEvent(e as CastingEvent); } break;
//                case (ECombatEv.CharacterKilled): { HandleCharacterKilledEvent(e as CharacterKilledEvent); } break;
//                case (ECombatEv.Debuff): { HandleDebuffEvent(e as DebuffEvent); } break;
//                case (ECombatEv.DisplayAction): { HandleDisplayActionEvent(e as DisplayActionEvent); } break;
//                case (ECombatEv.DisplayHitStats): { HandleDisplayHitStatsEvent(e as DisplayHitStatsEvent); } break;
//                case (ECombatEv.DoT): { HandleDoTEvent(e as EvDoT); } break;
//                case (ECombatEv.EndTurn): { HandleEndTurnEvent(e as EndTurnEvent); } break;
//                case (ECombatEv.GenericEffect): { HandleGenericEffect(e as GenericEffectEvent); } break;
//                case (ECombatEv.HexSelectedForMove): { HandleHexSelectedForMoveEvent(e as HexSelectedForMoveEvent); } break;
//                case (ECombatEv.HoT): { HandleHoTEvent(e as EvHoT); } break;
//                case (ECombatEv.MapDoneLoading): { HandleMapDoneLoadingEvent(e as EvCombatMapLoaded); } break;
//                case (ECombatEv.ModifyHP): { HandleModifyHPEvent(e as ModifyHPEvent); } break;
//                case (ECombatEv.ModifyStam): { HandleModifyStamEvent(e as ModifyStamEvent); } break;
//                case (ECombatEv.PathTraversed): { HandlePathTraversedEvent(e as EvPathTraversed); } break;
//                case (ECombatEv.PerformAction): { HandlePerformActionEvent(e as EvPerformAction); } break;
//                case (ECombatEv.PredictAction): { HandlePredictActionEvent(e as EvPredictAction); } break;
//                case (ECombatEv.Shapeshift): { HandleShapeshiftEvent(e as ShapeshiftEvent); } break;
//                case (ECombatEv.Shield): { HandleShieldEvent(e as EvShield); } break;
//                case (ECombatEv.ShowPotentialPath): { HandleShowPotentialPathEvent(e as ShowPotentialPathEvent); } break;
//                case (ECombatEv.Summon): { HandleSummonEvent(e as EvSummon); } break;
//                case (ECombatEv.TakingAction): { HandleTakingActionEvent(e as TakingActionEvent); } break;
//                case (ECombatEv.TileDoubleClick): { HandleTileDoubleClickEvent(e as EvTileDoubleClick); } break;
//                case (ECombatEv.TileHoverDeco): { HandleTileHoverDecoEvent(e as EvTileHover); } break;
//                case (ECombatEv.TileHitEvent): { HandleTileHitEvent(e as EvTileHit); } break;
//                case (ECombatEv.TraversePath): { HandleTraversePathEvent(e as EvTraversePath); } break;
//                case (ECombatEv.TraverseTile): { HandleTraverseTileEvent(e as EvTraverseTile); } break;
//            }
//        }

//        private void HandleApplyInjuryEvent(ApplyInjuryEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.ApplyInjuryGraphics(e);
//        }

//        private void HandleAttackSelectedEvent(AttackSelectedEvent e)
//        {
//            this._events.Remove(e);
//            this._currentActionTiles = new List<TileController>();
//            var ability = AbilityTable.Instance.Table[e.AttackType];
//            this._currentActionTiles = ability.GetTargetTiles(e, this._combatManager.CurrActing, this._combatManager);
//            this._combatManager.SetCurrentTargetTiles(this._currentActionTiles);
//            CMapGUIController.Instance.DecoratePotentialAttackTiles(this._currentActionTiles);
//            foreach (var tile in this._currentActionTiles)
//            {
//                TileControllerFlags.SetAwaitingActionFlagTrue(tile.Flags);
//            }

//            this._combatManager.CurAbility = ability;
//        }

//        private void HandleBuffEvent(BuffEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayBuff(e);
//        }

//        private void HandleCastingEvent(CastingEvent e)
//        {
//            this._events.Remove(e);
//            this._combatManager.AddCasting(e);
//            CMapGUIController.Instance.DisplayCast(e);
//            CMapGUIController.Instance.ClearDecoratedTiles();
//            var cur = this._combatManager.CurrActing.Handle;
//            var bob = cur.GetComponent<BobbingScript>();
//            if (bob != null) { bob.Reset(); }
//            this._combatManager.CurAbility = null;
//            this._combatManager.ProcessNextTurn();
//            this.PopulateBtnsHelper();
//        }

//        private void HandleDebuffEvent(DebuffEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayDebuff(e);
//        }

//        private void HandleCharacterKilledEvent(CharacterKilledEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.ProcessCharacterKilled(e);
//            this._combatManager.ProcessCharacterKilled(e.Killed);
//        }

//        private void HandleDisplayActionEvent(DisplayActionEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayActionEvent(e);
//        }

//        private void HandleDisplayHitStatsEvent(DisplayHitStatsEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayHitStatsEvent(e);
//        }

//        private void HandleDoTEvent(EvDoT e)
//        {
//            this._events.Remove(e);
//            var dotType = e.DoT.Type.ToString().Replace("_", " ");
//            CMapGUIController.Instance.DisplayText(dotType, e.ToDoT.Handle, CMapGUIControllerParams.RED);
//        }

//        private void HandleEndTurnEvent(EndTurnEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.ClearDecoratedTiles();
//            CMapGUIController.Instance.ProcessNewTurn();
//            var cur = this._combatManager.CurrActing.Handle;
//            this._combatManager.CurrActing.Model.ProcessEndOfTurn();
//            var bob = cur.GetComponent<BobbingScript>();
//            if (bob != null) { bob.Reset(); }
//            this._combatManager.CurAbility = null;
//            this._combatManager.ProcessNextTurn();
//            this.PopulateBtnsHelper();
//        }

//        private void HandleGenericEffect(GenericEffectEvent e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayGenericEffect(e);
//        }

//        private void HandleHexSelectedForMoveEvent(HexSelectedForMoveEvent e)
//        {
//            this._events.Remove(e);
//            if (this._combatManager != null)
//            {
//                var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Selected, this);
//            }
//        }

//        private void HandleHoTEvent(EvHoT e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayText("HoT", e.ToHoT.Handle, CMapGUIControllerParams.GREEN);
//        }

//        private void HandleMapDoneLoadingEvent(EvCombatMapLoaded e)
//        {
//            this._events.Remove(e);
//            this._combatManager = new CombatManager(e.Map);
//            this._combatManager.InitParties(e.LParty, e.RParty);
//            this._mapLoader = e.MapLoader;
//            this.PopulateBtnsHelper();
//        }

//        private void HandleModifyHPEvent(ModifyHPEvent e)
//        {
//            this._events.Remove(e);
//        }

//        private void HandleModifyStamEvent(ModifyStamEvent e)
//        {
//            this._events.Remove(e);
//        }

//        private void HandlePathTraversedEvent(EvPathTraversed e)
//        {
//            this._events.Remove(e);
//            {
//                if (e.Character.Model.GetCurrentHP() > 0)
//                {
//                    if (e.Character == this.GetCurrentCharacter())
//                    {
//                        CMapGUIController.Instance.SetActingBoxToController(e.Character);
//                        var bob = e.Character.Handle.AddComponent<BobbingScript>();
//                        bob.Init(PER_FRAME, PER_FRAME_DIST, e.Character.Handle);
//                    }
//                }
//                else
//                {
//                    var end = new EndTurnEvent(this);
//                }
//                CMapGUIController.Instance.ClearDecoratedTiles();
//                this._events.RemoveAll(x => x.Type == ECombatEv.ShowPotentialPath);
//            }
//        }

//        private void HandlePerformActionEvent(EvPerformAction e)
//        {
//            this._events.Remove(e);
//            this._combatManager.ResetTileControllerFlags();
//            this.HandlePerformActionEventHelper(e);
//        }

//        private void HandlePerformActionEventHelper(EvPerformAction e)
//        {
//            if (this._combatManager.CurAbility != null && this._combatManager.CurAbility != null)
//            {
//                e.Container.Action = this._combatManager.CurAbility;
//                e.Container.Source = this._combatManager.CurrActing;
//                this._combatManager.CurAbility = null;
//                if (e.ValidAction())
//                {
//                    this._currentAction = e;
//                    e.Perform();
//                }
//            }
//        }

//        private void HandlePredictActionEvent(EvPredictAction e)
//        {
//            this._events.Remove(e);
//        }

//        private void HandleShieldEvent(EvShield e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.DisplayText("Shield", e.ToShield.Handle, CMapGUIControllerParams.BLUE);
//        }

//        private void HandleShowPotentialPathEvent(ShowPotentialPathEvent e)
//        {
//            this._events.Remove(e);
//            var path = this._combatManager.GetPathTileControllers(e);
//            CMapGUIController.Instance.DecoratePath(path);
//        }

//        private void HandleShapeshiftEvent(ShapeshiftEvent e)
//        {
//            this._events.Remove(e);
//            var shift = new DisplayHitStatsEvent(CombatEventManager.Instance, e.Hit, e.Hit.Done);
//        }

//        private void HandleSummonEvent(EvSummon e)
//        {
//            //this._events.Remove(e);
//            //var summonParams = SummonFactory.Instance.CreateNewObject(e);
//            //var attemptedTile = e.Hit.TargetTile;
//            //var tgtTile = attemptedTile.GetNearestEmptyTile();
//            //this._mapLoader.BuildAndPlaceCharacter(summonParams, ref this._combatManager.Characters, tgtTile, e.Hit.Source.LParty);
//            //e.Hit.Done();
//        }

//        private void HandleTakingActionEvent(TakingActionEvent e)
//        {
//            this._events.Remove(e);
//            this._combatManager.CurrActing = e.Controller;
//            CMapGUIController.Instance.SetActingBoxToController(e.Controller);
//            var bob = e.Controller.Handle.AddComponent<BobbingScript>();
//            bob.Init(PER_FRAME, PER_FRAME_DIST, e.Controller.Handle);
//            var script = this.CameraManager.GetComponent<CameraManager>();
//            script.InitScrollTo(e.Controller.Handle.transform.position);
//        }

//        private void HandleTileDoubleClickEvent(EvTileDoubleClick e)
//        {
//            this._events.Remove(e);
//            var pathEvent = new ShowPotentialPathEvent(this._combatManager.CurrActing, e.Tile, this);
//            var pathTileControllers = this._combatManager.GetPathTileControllers(pathEvent);
//            CMapGUIController.Instance.DecoratePath(pathTileControllers);
//            var path = this._combatManager.GetPath(this._combatManager.CurrActing.CurrentTile, e.Tile);
//            var traversePathEvent = new EvTraversePath(this, this._combatManager.CurrActing, path);
//        }

//        private void HandleTileHitEvent(EvTileHit e)
//        {
//            this._events.Remove(e);
//        }

//        private void HandleTileHoverDecoEvent(EvTileHover e)
//        {
//            this._events.Remove(e);
//            CMapGUIController.Instance.ClearAoETiles();
//            CMapGUIController.Instance.DecorateHover(e.Tile);
//            CMapGUIController.Instance.DecorateAoETiles(e.AoETiles);
//        }

//        private void HandleTraversePathEvent(EvTraversePath e)
//        {
//            this._events.Remove(e);
//            var bob = e.Character.GetComponent<BobbingScript>();
//            if (bob != null) { bob.Reset(); }
//            var next = e.Path.GetNextTile(e.Character.CurrentTile);
//            var traverseTileEvent = new EvTraverseTile(this, e.Path, e.Character.CurrentTile, next);
//        }

//        private void HandleTraverseTileEvent(EvTraverseTile e)
//        {
//            this._events.Remove(e);
//            var script = e.Character.Handle.AddComponent<TileMoveScript>();
//            script.Init(e.Character, e.Path, e.Source, e.Next);
//            CMapGUIController.Instance.SetActingBoxToController(e.Character);
//        }

//        private void PopulateBtnsHelper()
//        {
//            var curr = this._combatManager.CurrActing.Model;
//            var abs = new List<Pair<MAbility, bool>>();

//            if (curr.Type == ECharacterType.Humanoid)
//            {
//                if (curr.LWeapon != null)
//                    foreach (var ab in curr.LWeapon.Abilities)
//                        abs.Add(new Pair<MAbility, bool>(ab, false));
//                if (curr.RWeapon != null)
//                    foreach (var ab in curr.RWeapon.Abilities)
//                        abs.Add(new Pair<MAbility, bool>(ab, true));
//            }
//            else
//            {
//                foreach (var ab in curr.DefaultWpnAbilities)
//                    abs.Add(new Pair<MAbility, bool>(ab, true));
//            }
//            CMapGUIController.Instance.ProcessNewTurn();
//            var populateWpnBtns = new PopulateWpnBtnsEvent(abs, GUIEventManager.Instance);
//        }
//    }
//}
