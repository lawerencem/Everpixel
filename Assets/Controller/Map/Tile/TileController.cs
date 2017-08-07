using UnityEngine;

namespace Controller.Map
{
    public class TileController : MonoBehaviour
    {

    }
}
//        private BoxCollider2D _collider;
//        private bool _doubleClick = false;
//        private double _clickDelta = 0.5;
//        private DateTime _clickTime;

//        public List<TileController> Adjacent { get; set; }
//        public List<CharController> DeadCharacters { get; set; }
//        public TileControllerFlags Flags { get; set; }
//        public GameObject Handle { get; set; }
//        public MTile Model { get; set; }
//        public HexTileView View { get; set; }
//        public List<AZone> Zones { get; set; }

//        public TileController()
//        {
//            this.Adjacent = new List<TileController>();
//            this.DeadCharacters = new List<CharController>();
//            this.Flags = new TileControllerFlags();
//            this.Model = new MTile();
//            this.Zones = new List<AZone>();
//        }

//        public void Update()
//        {
//            if (_doubleClick && (DateTime.Now > (this._clickTime.AddSeconds(this._clickDelta))))
//                this._doubleClick = false;       
//        }

//        public void AddZone(AZone zone) { this.Zones.Add(zone); }
//        public void RemoveZone(AZone zone) { this.Zones.Remove(zone); }

//        public TileController GetNearestEmptyTile()
//        {
//            if (this.Model.Current == null)
//                return this;
//            else
//            {
//                var openSet = new List<TileController>() { this };
//                var closed = new List<TileController>();
//                while (openSet.Count > 0)
//                {
//                    var tile = openSet.ElementAt(0);
//                    var probed = closed.Find(x => x == tile);
//                    if (probed == null)
//                    {
//                        if (tile.Model.Current == null)
//                            return tile;
//                        else
//                            openSet.Add(tile);
//                    }
//                    foreach (var neighbor in tile.Adjacent)
//                        openSet.Add(neighbor);
//                    closed.Add(tile);
//                    openSet.RemoveAt(0);
//                }
//            }
//            return null;
//        }

//        public void Init(GameObject o)
//        {
//            this.Handle = o;
//            this._collider = this.Handle.AddComponent<BoxCollider2D>();
//        }

//        public void OnMouseDown()
//        {
//            if (TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
//            {
//                var perform = new EvPerformAction(CombatEventManager.Instance, this, CombatEventManager.Instance.ActionPerformedCallback);
//            }
//            else if (this.Model.Current == null)
//            {
//                var e = new HexSelectedForMoveEvent(this, CombatEventManager.Instance);

//                if (this._doubleClick)
//                {
//                    var doubleClick = new EvTileDoubleClick(CombatEventManager.Instance, this);
//                }
//                else
//                {
//                    this._clickTime = System.DateTime.Now;
//                    this._doubleClick = true;
//                }
//            }
//        }

//        public void OnMouseEnter()
//        {
//            this.HandleHover();
//            this.HandleAoE();
//        }

//        public void OnMouseExit()
//        {
//            CMapGUIController.Instance.SetHoverModalInactive();
//        }

//        public void SetModel(MTile t)
//        {
//            this.Model = t;
//            this.Model.Parent = this;
//        }

//        public void SetView(HexTileView v)
//        {
//            this.View = v;
//        }

//        private void HandleAoE()
//        {
//            var curChar = CombatEventManager.Instance.GetCurrentCharacter();
//            var curAbility = CombatEventManager.Instance.GetCurrentAbility();
//            var aoe = new List<TileController>();
//            if (curAbility != null && TileControllerFlags.HasFlag(this.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
//            {
//                if (!curAbility.isRayCast())
//                {
//                    var hexes = this.Model.GetAoETiles((int)curAbility.AoE);
//                    foreach (var hex in hexes)
//                        aoe.Add(hex.Parent);
//                }
//                else
//                {
//                    var hexes = this.Model.GetRaycastTiles(curChar.CurrentTile.Model, curAbility.Range);
//                    foreach (var hex in hexes)
//                        aoe.Add(hex.Parent);
//                }
//            }
//            var hover = new EvTileHover(CombatEventManager.Instance, this, aoe);
//        }

//        private void HandleHover()
//        {
//            this.HandleHoverTargetStats();
//            this.HandleHoverTargetDamage();
//        }

//        private void HandleHoverTargetDamage()
//        {
//            if (this.Model.Current != null &&
//                this.Model.Current.GetType() == typeof(CharController) &&
//                CombatEventManager.Instance.GetCurrentAbility() != null)
//            {
//                var predict = new EvPredictAction(CombatEventManager.Instance);
                
//                predict.Container.Ability = CombatEventManager.Instance.GetCurrentAbility();
//                predict.Container.Source = CombatEventManager.Instance.GetCurrentCharacter();
//                predict.Container.Target = this;
//                var targets = predict.Container.Ability.GetAoETiles(
//                    predict.Container.Source.CurrentTile, 
//                    predict.Container.Target, 
//                    predict.Container.Ability.Range);

//                foreach (var target in targets)
//                {
//                    var hit = new Hit(predict.Container.Source, predict.Container.Target, predict.Container.Ability);
//                    predict.Container.Hits.Add(hit);
//                }

//                predict.Process();
//                CMapGUIController.Instance.SetHoverModalDamageValues(predict);
//            }
//            else
//                CMapGUIController.Instance.SetDmgModalInactive();
//        }

//        private void HandleHoverTargetStats()
//        {
//            if (this.Model.Current != null && this.Model.Current.GetType() == typeof(CharController))
//            {
//                var fov = Camera.main.fieldOfView;
//                var character = this.Model.Current as CharController;
//                var position = character.Handle.transform.position;
//                position.x += (float)(fov * 0.025);
//                position.y += (float)(fov * 0.025);
//                CMapGUIController.Instance.SetHoverModalHeaderText(character.View.Name);
//                CMapGUIController.Instance.SetHoverModalLocation(position);
//                var controller = this.Model.Current as CharController;
//                CMapGUIController.Instance.SetHoverModalStatValues(controller.Model);
//            }
//        }
//    }
//}
