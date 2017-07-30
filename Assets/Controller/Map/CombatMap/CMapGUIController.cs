using Controller.Characters;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;
using View.Characters;
using View.GUI;

namespace Controller.Managers.Map
{
    public class CMapGUIController
    {
        private List<GameObject> _aoeTiles = new List<GameObject>();
        private List<GameObject> _boxImages = new List<GameObject>();
        private List<GameObject> _decorateTileFamily = new List<GameObject>();
        private GameObject _singleTile;

        private AbilitiesModal _abilityModal;
        private GameObject _banner;        
        private HoverModal _hoverModal;

        private CMapGUIControllerHit _hitHelper = new CMapGUIControllerHit();
        private CMapGUIControllerParticle _particleHelper = new CMapGUIControllerParticle();
        private CMapGUIControllerShapeshift _shapeshiftHelper = new CMapGUIControllerShapeshift();

        private static CMapGUIController _instance;
        public static CMapGUIController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CMapGUIController();
                return _instance;
            }
        }

        public void ActivateFatalityBanner()
        {
            this._banner.SetActive(true);
            var script = this._banner.AddComponent<DeactivateByLifetime>();
            script.Init(this._banner, 4);
        }

        public void ApplyInjuryGraphics(ApplyInjuryEvent e)
        {
            this._particleHelper.ApplyInjuryParticle(e);
            this._hitHelper.ProcessInjury(e);
        }

        public CMapGUIController()
        {
            this._abilityModal = new AbilitiesModal();
            this._hoverModal = new HoverModal();

            this._abilityModal.Init();
            this._hoverModal.Init();

            var banner = GameObject.FindGameObjectWithTag("BannerTag");
            this._banner = banner;
            this._banner.SetActive(false);
        }

        public void ClearAoETiles()
        {
            foreach (var old in this._aoeTiles) { GameObject.Destroy(old); }
            this._aoeTiles.Clear();
        }

        public void ClearDecoratedTiles()
        {
            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }     
            this._decorateTileFamily.Clear();
        }

        public void DecorateAoETiles(List<TileController> t)
        {
            var sprite = MapBridge.Instance.GetTileHighlightSprite();
            foreach (var tile in t)
                this.DecorateAoETile(tile, sprite);
        }

        public void DecorateHover(TileController t)
        {
            if (this._singleTile != null && this._singleTile != t)
                GameObject.Destroy(this._singleTile);

            if (TileControllerFlags.HasFlag(t.Flags.CurFlags, TileControllerFlags.Flags.AwaitingAction))
            {
                var sprite = MapBridge.Instance.GetHostileHoverSprite();
                DecorateSingleTile(t, sprite);
            }
        }

        public void DecoratePath(List<TileController> p)
        {
            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }

            if (p != null)
            {
                var sprite = MapBridge.Instance.GetTileHighlightSprite();
                foreach (var tile in p)
                {
                    DecorateFamilyOfTiles(tile, sprite);
                }
            }
        }

        public void DecoratePotentialAttackTiles(List<TileController> tiles)
        {
            foreach (var old in this._decorateTileFamily) { GameObject.Destroy(old); }

            if (tiles != null)
            {
                var sprite = MapBridge.Instance.GetPotentialAttackLocSprite();
                foreach(var t in tiles)
                {
                    DecorateFamilyOfTiles(t, sprite);
                }
            }
        }

        public void DisplayActionEvent(DisplayActionEvent e)
        {
            switch(e.EventController.Action.CastType)
            {
                case (CastTypeEnum.Bullet): { this._hitHelper.ProcessBulletFX(e); } break;
                case (CastTypeEnum.Raycast): { this._hitHelper.ProcessBulletFX(e); } break;
                case (CastTypeEnum.Melee): { this._hitHelper.ProcessMeleeHitFX(e); } break;
                case (CastTypeEnum.No_Collision_Bullet): { this._hitHelper.ProcessBulletFX(e); } break;
                case (CastTypeEnum.Zone): { this._hitHelper.ProcessZoneFX(e); } break;
                default: { e.AttackFXDone(); } break;
            }
        }

        public void DisplayBuff(BuffEvent e)
        {
            this._hitHelper.DisplayText("Buff", e.ToBuff.Handle, CMapGUIControllerParams.BLUE);
        }

        public void DisplayCast(CastingEvent e)
        {
            this._hitHelper.DisplayText(e.SpellName, e.Caster.Handle, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
        }

        public void DisplayDebuff(DebuffEvent e)
        {
            this._hitHelper.DisplayText("Debuff", e.ToDebuff.Handle, CMapGUIControllerParams.RED);
        }

        public void DisplayGenericEffect(GenericEffectEvent e)
        {
            var display = e.Effect.Type.ToString().Replace("_", " ");
            this._hitHelper.DisplayText(display, e.Target.Handle, CMapGUIControllerParams.WHITE);
        }

        public void DisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            switch(e.Hit.Ability.CastType)
            {
                case (CastTypeEnum.Bullet): { this.ProcessDefenderGraphics(e); } break;
                case (CastTypeEnum.Raycast): { this.ProcessDefenderGraphics(e); } break;
                case (CastTypeEnum.Melee): { this.ProcessDefenderGraphics(e); } break;
                case (CastTypeEnum.No_Collision_Bullet): { this.ProcessDefenderGraphics(e); } break;
                case (CastTypeEnum.Shapeshift): { this._shapeshiftHelper.ProcessShapeshiftFX(e); } break;
                case (CastTypeEnum.Song): { this._particleHelper.HandleSongParticle(e); } break;
                case (CastTypeEnum.Summon): { this.ProcessSummonFX(e); } break;
                case (CastTypeEnum.Zone): { e.Done(); } break;
            }
        }

        public void DisplayText(string toDisplay, GameObject toShow, Color color, float yOffset = 0, float dur = 3)
        {
            this._hitHelper.DisplayText(toDisplay, toShow, color, yOffset, dur);
        }

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            this._hitHelper.ProcessCharacterKilled(e.Killed);
        }

        private void ProcessDefenderGraphics(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target != null)
            {
                this.TryProcessShieldFX(e);
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Dodge))
                    this._hitHelper.ProcessDodge(e);
                else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Parry))
                    this._hitHelper.ProcessParry(e);
                else if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Block))
                    this._hitHelper.ProcessBlock(e);
                else
                    this._hitHelper.ProcessNormalHit(e);
                if (AttackEventFlags.HasFlag(e.Hit.Flags.CurFlags, AttackEventFlags.Flags.Resist))
                    this._hitHelper.ProcessResist(e);
                this._hitHelper.ProcessSplatterOnHitEvent(e);
            }
            e.Done();
        }

        public void ProcessNewTurn()
        {
            this._abilityModal.ResetModal();
        }

        public void ProcessSummonFX(DisplayHitStatsEvent e)
        {
            this._hitHelper.ProcessSummon(e);
        }

        public void SetAbilityModalActive()
        {
            this._abilityModal.SetModalActive();
        }

        public void SetAbilityModalInactive()
        {
            this._abilityModal.SetModalInactive();
        }

        public void SetActingBoxToController(GenericCharacterController c)
        {
            this.SetTagText(CMapGUIControllerParams.NAME, c.View.Name);
            this.SetTagText(CMapGUIControllerParams.AP, c.Model.GetCurrentAP() + " / " + ((int)c.Model.GetCurrentStatValue(SecondaryStatsEnum.AP)).ToString());
            this.SetTagText(CMapGUIControllerParams.HP, c.Model.GetCurrentHP() + " / " + ((int)c.Model.GetCurrentStatValue(SecondaryStatsEnum.HP)).ToString());
            this.SetTagText(CMapGUIControllerParams.STAM, c.Model.GetCurrentStamina() + " / " + ((int)c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina)).ToString());
            this.SetTagText(CMapGUIControllerParams.MORALE, c.Model.GetCurrentMorale() + " / " + ((int)c.Model.GetCurrentStatValue(SecondaryStatsEnum.Morale)).ToString());

            if (c.Model.Armor != null)
                this.SetTagText(CMapGUIControllerParams.ARMOR, c.Model.Armor.Name);
            else
                this.SetTagText(CMapGUIControllerParams.ARMOR, "");
            if (c.Model.Helm != null)
                this.SetTagText(CMapGUIControllerParams.HELM, c.Model.Helm.Name);
            else
                this.SetTagText(CMapGUIControllerParams.HELM, "");
            if (c.Model.LWeapon != null)
                this.SetTagText(CMapGUIControllerParams.L_WEAP, c.Model.LWeapon.Name);
            else
                this.SetTagText(CMapGUIControllerParams.L_WEAP, "");
            if (c.Model.RWeapon != null)
                this.SetTagText(CMapGUIControllerParams.R_WEAP, c.Model.RWeapon.Name);
            else
                this.SetTagText(CMapGUIControllerParams.R_WEAP, "");
        }

        public void SetDmgModalInactive()
        {
            this._hoverModal.SetDamageModalInactive();
        }

        public void SetHoverModalActive()
        {
            this._hoverModal.SetModalActive();
        }

        public void SetHoverModalDamageValues(PredictActionEvent e)
        {
            this._hoverModal.SetModalDamageValues(e);
        }

        public void SetHoverModalInactive()
        {
            this._hoverModal.SetModalInactive();
        }

        public void SetHoverModalHeaderText(string toSet)
        {
            this._hoverModal.SetModalHeaderText(toSet);
        }

        public void SetHoverModalLocation(Vector3 pos)
        {
            this._hoverModal.SetModalLocation(pos);
        }

        public void SetHoverModalStatValues(GenericCharacter c)
        {
            this._hoverModal.SetModalStatValues(c);
        }

        private void DecorateAoETile(TileController tile, Sprite deco)
        {
            var tView = this.DecorateTile(tile, deco);
            this._aoeTiles.Add(tView);
        }

        private void DecorateFamilyOfTiles(TileController tile, Sprite deco)
        {
            var tView = this.DecorateTile(tile, deco, 0.75f);
            this._decorateTileFamily.Add(tView);
        }

        private void DecorateSingleTile(TileController t, Sprite deco, float alpha = 0.50f)
        {
            var tView = this.DecorateTile(t, deco, alpha);
            this._singleTile = tView;
        }

        private GameObject DecorateTile(TileController t, Sprite deco, float alpha = 0.50f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Decorated Tile";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            return tView;
        }
        
        private void SetTagText(string tag, string toSet)
        {
            var tagged = GameObject.FindGameObjectWithTag(tag);
            if (tagged != null)
            {
                var text = tagged.GetComponent<Text>();
                text.text = toSet;
            }
        }

        private void TryProcessShieldFX(DisplayHitStatsEvent e)
        {
            if (e.Hit.Target != null)
            {
                if (e.Hit.Target.Model.Shields.Count > 0)
                {
                    var shield = CharacterSpriteLoader.Instance.GetShieldSprite();
                    var shieldView = new GameObject();
                    var renderer = shieldView.AddComponent<SpriteRenderer>();
                    renderer.sprite = shield;
                    renderer.transform.position = e.Hit.Target.Handle.transform.position;
                    renderer.sortingLayerName = CMapGUIControllerParams.PARTICLES_LAYER;
                    shieldView.name = "Shield Sprite";
                    var destroy = shieldView.AddComponent<DestroyByLifetime>();
                    destroy.lifetime = 1f;
                    if (!e.Hit.Target.LParty)
                    {
                        var position = shieldView.transform.position;
                        position.x -= 0.15f;
                        shieldView.transform.localRotation = Quaternion.Euler(0, 180, 0);
                        shieldView.transform.position = position;
                    }
                    else
                    {
                        var position = shieldView.transform.position;
                        position.x += 0.15f;
                        shieldView.transform.position = position;
                    }
                    shieldView.transform.SetParent(e.Hit.Target.Handle.transform);
                }
            }
        }
    }
}
