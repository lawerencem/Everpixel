using Assets.Controller.Map.Tile;
using Assets.Model.Map;
using Assets.View;
using Assets.View.Map;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Map.Combat
{
    public class VMapController
    {
        private const float DEFAULT_ALPHA = 0.5f;

        private List<GameObject> _familyTileDeco = new List<GameObject>();

        private static VMapController _instance;
        public static VMapController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new VMapController();
                return _instance;
            }
        }
        
        public VMapController()
        {
            this._familyTileDeco = new List<GameObject>();
        }

        public void DecoratePath(Path p)
        {
            foreach (var old in this._familyTileDeco)
            {
                GameObject.Destroy(old);
            }

            if (p != null)
            {
                var sprite = MapBridge.Instance.GetTileHighlightSprite();
                foreach (var t in p.GetTiles())
                {
                    DecorateTileFamily(t.Controller, sprite);
                }
            }
        }

        private GameObject DecorateTile(TileController t, Sprite sprite, float alpha = DEFAULT_ALPHA)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;
            renderer.transform.position = t.View.Center;
            renderer.sortingLayerName = Layers.MAP_GUI_LAYER;
            tView.name = "Tile Deco";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            return tView;
        }

        private void DecorateTileFamily(TileController tile, Sprite deco)
        {
            var tView = this.DecorateTile(tile, deco, ViewParams.TILE_DECO_ALPHA);
            this._familyTileDeco.Add(tView);
        }
    }
}
//        private List<GameObject> _aoeTiles = new List<GameObject>();
//        private List<GameObject> _boxImages = new List<GameObject>();
//        private GameObject _singleTile;

//        private AbilitiesModal _abilityModal;
//        private GameObject _banner;        
//        private HoverModal _hoverModal;

//        private CMapGUIControllerHit _hitHelper = new CMapGUIControllerHit();
//        private CMapGUIControllerParticle _particleHelper = new CMapGUIControllerParticle();
//        private CMapGUIControllerShapeshift _shapeshiftHelper = new CMapGUIControllerShapeshift();

//        public void ActivateFatalityBanner()
//        {
//            this._banner.SetActive(true);
//            var script = this._banner.AddComponent<DeactivateByLifetime>();
//            script.Init(this._banner, 4);
//        }

//        public void ApplyInjuryGraphics(ApplyInjuryEvent e)
//        {
//            this._particleHelper.ApplyInjuryParticle(e);
//            this._hitHelper.ProcessInjury(e);
//        }

//        public CMapGUIController()
//        {
//            this._abilityModal = new AbilitiesModal();
//            this._hoverModal = new HoverModal();

//            this._abilityModal.Init();
//            this._hoverModal.Init();

//            var banner = GameObject.FindGameObjectWithTag("BannerTag");
//            this._banner = banner;
//            this._banner.SetActive(false);
//        }

//        public void ClearAoETiles()
//        {
//            foreach (var old in this._aoeTiles) { GameObject.Destroy(old); }
//            this._aoeTiles.Clear();
//        }

//        public void ClearDecoratedTiles()
//        {
//            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }     
//            this._decorateTileFamily.Clear();
//        }

//        public void DecorateAoETiles(List<TileController> t)
//        {
//            var sprite = MapBridge.Instance.GetTileHighlightSprite();
//            foreach (var tile in t)
//                this.DecorateAoETile(tile, sprite);
//        }

//        public void DecorateHover(TileController t)
//        {
//            if (this._singleTile != null && this._singleTile != t)
//                GameObject.Destroy(this._singleTile);

//            if (TileControllerFlags.HasFlag(t.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
//            {
//                var sprite = MapBridge.Instance.GetHostileHoverSprite();
//                DecorateSingleTile(t, sprite);
//            }
//        }

//        public void DecoratePotentialAttackTiles(List<TileController> tiles)
//        {
//            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }

//            if (tiles != null)
//            {
//                var sprite = MapBridge.Instance.GetPotentialAttackLocSprite();
//                foreach(var t in tiles)
//                {
//                    DecorateFamilyOfTiles(t, sprite);
//                }
//            }
//        }

//        public void DisplayActionEvent(DisplayActionEvent e)
//        {
//            switch(e.EventController.Action.CastType)
//            {
//                case (CastTypeEnum.Bullet): { this._hitHelper.ProcessBulletFX(e); } break;
//                case (CastTypeEnum.Raycast): { this._hitHelper.ProcessBulletFX(e); } break;
//                case (CastTypeEnum.Melee): { this._hitHelper.ProcessMeleeHitFX(e); } break;
//                case (CastTypeEnum.No_Collision_Bullet): { this._hitHelper.ProcessBulletFX(e); } break;
//                case (CastTypeEnum.Zone): { this._hitHelper.ProcessZoneFX(e); } break;
//                default: { e.AttackFXDone(); } break;
//            }
//        }

//        public void DisplayBuff(BuffEvent e)
//        {
//            this._hitHelper.DisplayText("+ " + e.BuffStr, e.ToBuff.Handle, CMapGUIControllerParams.BLUE);
//        }

//        public void DisplayCast(CastingEvent e)
//        {
//            this._hitHelper.DisplayText(e.SpellName, e.Caster.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
//        }

//        public void DisplayDebuff(DebuffEvent e)
//        {
//            this._hitHelper.DisplayText("Debuff", e.ToDebuff.Handle, CMapGUIControllerParams.RED);
//        }

//        public void DisplayGenericEffect(GenericEffectEvent e)
//        {
//            var display = e.Effect.Type.ToString().Replace("_", " ");
//            this._hitHelper.DisplayText(display, e.Target.Handle, CMapGUIControllerParams.WHITE);
//        }

//        public void DisplayHitStatsEvent(DisplayHitStatsEvent e)
//        {
//            switch(e.Hit.Ability.CastType)
//            {
//                case (CastTypeEnum.Bullet): { this.ProcessDefenderGraphics(e); } break;
//                case (CastTypeEnum.Raycast): { this.ProcessDefenderGraphics(e); } break;
//                case (CastTypeEnum.Melee): { this.ProcessDefenderGraphics(e); } break;
//                case (CastTypeEnum.No_Collision_Bullet): { this.ProcessDefenderGraphics(e); } break;
//                case (CastTypeEnum.Shapeshift): { this._shapeshiftHelper.ProcessShapeshiftFX(e); } break;
//                case (CastTypeEnum.Song): { this._particleHelper.HandleSongParticle(e); } break;
//                case (CastTypeEnum.Summon): { this.ProcessSummonFX(e); } break;
//                case (CastTypeEnum.Zone): { e.Done(); } break;
//            }
//        }

//        public void DisplayText(string toDisplay, GameObject toShow, Color color, float yOffset = 0, float dur = 3)
//        {
//            this._hitHelper.DisplayText(toDisplay, toShow, color, yOffset, dur);
//        }

//        public void ProcessCharacterKilled(CharacterKilledEvent e)
//        {
//            this._hitHelper.ProcessCharacterKilled(e.Killed);
//        }

//        private void ProcessDefenderGraphics(DisplayHitStatsEvent e)
//        {
//            if (e.Hit.Target != null)
//            {
//                this.TryProcessShieldFX(e);
//                if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Dodge))
//                    this._hitHelper.ProcessDodge(e);
//                else if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Parry))
//                    this._hitHelper.ProcessParry(e);
//                else if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Block))
//                    this._hitHelper.ProcessBlock(e);
//                else
//                    this._hitHelper.ProcessNormalHit(e);
//                if (FHit.HasFlag(e.Hit.Flags.CurFlags, FHit.Flags.Resist))
//                    this._hitHelper.ProcessResist(e);
//                this._hitHelper.ProcessSplatterOnHitEvent(e);
//            }
//            e.Done();
//        }

//        public void ProcessNewTurn()
//        {
//            this._abilityModal.ResetModal();
//        }

//        public void ProcessSummonFX(DisplayHitStatsEvent e)
//        {
//            this._hitHelper.ProcessSummon(e);
//        }

//        public void SetAbilityModalActive()
//        {
//            this._abilityModal.SetModalActive();
//        }

//        public void SetAbilityModalInactive()
//        {
//            this._abilityModal.SetModalInactive();
//        }

//        public void SetActingBoxToController(CharController c)
//        {
//            this.SetTagText(CMapGUIControllerParams.NAME, c.View.Name);
//            this.SetTagText(CMapGUIControllerParams.AP, c.Model.GetCurrentAP() + " / " + ((int)c.Model.GetCurrentStatValue(ESecondaryStat.AP)).ToString());
//            this.SetTagText(CMapGUIControllerParams.HP, c.Model.GetCurrentHP() + " / " + ((int)c.Model.GetCurrentStatValue(ESecondaryStat.HP)).ToString());
//            this.SetTagText(CMapGUIControllerParams.STAM, c.Model.GetCurrentStamina() + " / " + ((int)c.Model.GetCurrentStatValue(ESecondaryStat.Stamina)).ToString());
//            this.SetTagText(CMapGUIControllerParams.MORALE, c.Model.GetCurrentMorale() + " / " + ((int)c.Model.GetCurrentStatValue(ESecondaryStat.Morale)).ToString());

//            if (c.Model.Armor != null)
//                this.SetTagText(CMapGUIControllerParams.ARMOR, c.Model.Armor.Name);
//            else
//                this.SetTagText(CMapGUIControllerParams.ARMOR, "");
//            if (c.Model.Helm != null)
//                this.SetTagText(CMapGUIControllerParams.HELM, c.Model.Helm.Name);
//            else
//                this.SetTagText(CMapGUIControllerParams.HELM, "");
//            if (c.Model.LWeapon != null)
//                this.SetTagText(CMapGUIControllerParams.L_WEAP, c.Model.LWeapon.Name);
//            else
//                this.SetTagText(CMapGUIControllerParams.L_WEAP, "");
//            if (c.Model.RWeapon != null)
//                this.SetTagText(CMapGUIControllerParams.R_WEAP, c.Model.RWeapon.Name);
//            else
//                this.SetTagText(CMapGUIControllerParams.R_WEAP, "");
//        }

//        public void SetDmgModalInactive()
//        {
//            this._hoverModal.SetDamageModalInactive();
//        }

//        public void SetHoverModalActive()
//        {
//            this._hoverModal.SetModalActive();
//        }

//        public void SetHoverModalDamageValues(EvPredictAction e)
//        {
//            this._hoverModal.SetModalDamageValues(e);
//        }

//        public void SetHoverModalInactive()
//        {
//            this._hoverModal.SetModalInactive();
//        }

//        public void SetHoverModalHeaderText(string toSet)
//        {
//            this._hoverModal.SetModalHeaderText(toSet);
//        }

//        public void SetHoverModalLocation(Vector3 pos)
//        {
//            this._hoverModal.SetModalLocation(pos);
//        }

//        public void SetHoverModalStatValues(MChar c)
//        {
//            this._hoverModal.SetModalStatValues(c);
//        }

//        private void DecorateAoETile(TileController tile, Sprite deco)
//        {
//            var tView = this.DecorateTile(tile, deco);
//            this._aoeTiles.Add(tView);
//        }

//        private void DecorateSingleTile(TileController t, Sprite deco, float alpha = 0.50f)
//        {
//            var tView = this.DecorateTile(t, deco, alpha);
//            this._singleTile = tView;
//        }
        
//        private void SetTagText(string tag, string toSet)
//        {
//            var tagged = GameObject.FindGameObjectWithTag(tag);
//            if (tagged != null)
//            {
//                var text = tagged.GetComponent<Text>();
//                text.text = toSet;
//            }
//        }

//        private void TryProcessShieldFX(DisplayHitStatsEvent e)
//        {
//            if (e.Hit.Target != null)
//            {
//                if (e.Hit.Target.Model.Shields.Count > 0)
//                {
//                    var shield = CharacterSpriteLoader.Instance.GetShieldSprite();
//                    var shieldView = new GameObject();
//                    var renderer = shieldView.AddComponent<SpriteRenderer>();
//                    renderer.sprite = shield;
//                    renderer.transform.position = e.Hit.Target.Handle.transform.position;
//                    renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
//                    shieldView.name = "Shield Sprite";
//                    var destroy = shieldView.AddComponent<DestroyByLifetime>();
//                    destroy.lifetime = 1f;
//                    if (!e.Hit.Target.LParty)
//                    {
//                        var position = shieldView.transform.position;
//                        position.x -= 0.15f;
//                        shieldView.transform.localRotation = Quaternion.Euler(0, 180, 0);
//                        shieldView.transform.position = position;
//                    }
//                    else
//                    {
//                        var position = shieldView.transform.position;
//                        position.x += 0.15f;
//                        shieldView.transform.position = position;
//                    }
//                    shieldView.transform.SetParent(e.Hit.Target.Handle.transform);
//                }
//            }
//        }
//    }
//}
