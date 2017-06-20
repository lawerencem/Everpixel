using Controller.Characters;
using Controller.Map;
using Generics.Scripts;
using Model.Abilities;
using Model.Characters;
using Model.Events.Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.Biomes;
using View.GUI;

namespace Controller.Managers.Map
{
    public class CMapGUIController
    {
        private List<GameObject> _boxImages = new List<GameObject>();
        private List<GameObject> _decorateTileFamily = new List<GameObject>();
        private GameObject _singleTile;

        private AbilitiesModal _abilityModal;
        private GameObject _banner;
        private CMapGUIControllerHit _hitHelper = new CMapGUIControllerHit();
        private HoverModal _hoverModal;
        private CMapGUIControllerParticle _particleHelper = new CMapGUIControllerParticle();

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

        public void ActivateBanner()
        {
            this._banner.SetActive(true);
            var script = this._banner.AddComponent<DeactivateByLifetime>();
            script.Init(this._banner, 4);
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

        public void ApplyInjuryGraphics(ApplyInjuryEvent e)
        {
            this._particleHelper.ApplyInjuryParticle(e);
            this._hitHelper.ProcessInjury(e);
        }

        public void ClearDecoratedTiles()
        {
            foreach (var old in this._decorateTileFamily)
                GameObject.Destroy(old);
            this._decorateTileFamily.Clear();
        }

        public void DecorateHover(TileController t)
        {
            if (this._singleTile != null && this._singleTile != t)
                GameObject.Destroy(this._singleTile);

            if (TileControllerFlags.HasFlag(t.Flags.CurFlags, TileControllerFlags.Flags.PotentialAttack))
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
                var sprite = MapBridge.Instance.GetMovePathSprite();
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

        public void DisplayCast(CastingEvent e)
        {
            this._hitHelper.DisplayText(e.SpellName, e.Caster.transform.position, CMapGUIControllerParams.WHITE, CMapGUIControllerParams.ATTACK_TEXT_OFFSET);
        }

        public void DisplayHitStatsEvent(DisplayHitStatsEvent e)
        {
            if (e.Hit.Ability.Type.GetType() == (typeof(WeaponAbilitiesEnum)))
            {
                this._hitHelper.ProcessMeleeHitGraphics(e);
            }
            else if (e.Hit.Ability.Type.GetType() == (typeof(ActiveAbilitiesEnum)))
            {
                var ability = e.Hit.Ability as GenericActiveAbility;
                switch(ability.CastType)
                {
                    case (AbilityCastTypeEnum.Bullet): { this._hitHelper.ProcessBulletGraphics(e); } break;
                    case (AbilityCastTypeEnum.No_Collision_Bullet): { this._hitHelper.ProcessBulletGraphics(e); } break;
                    case (AbilityCastTypeEnum.Summon): { this.ProcessSummon(e); } break;
                }
            }
        } 

        public void ProcessCharacterKilled(CharacterKilledEvent e)
        {
            this._hitHelper.ProcessCharacterKilled(e.Killed);
        }

        public void ProcessNewTurn()
        {
            this._abilityModal.ResetModal();
        }

        public void ProcessSummon(DisplayHitStatsEvent e)
        {
            // TODO: any graphics
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
            this.SetTagText(CMapGUIControllerParams.AP, c.Model.CurrentAP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.AP).ToString());
            this.SetTagText(CMapGUIControllerParams.HP, c.Model.CurrentHP + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.HP).ToString());
            this.SetTagText(CMapGUIControllerParams.STAM, c.Model.CurrentStamina + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Stamina).ToString());
            this.SetTagText(CMapGUIControllerParams.MORALE, c.Model.CurrentMorale + " / " + c.Model.GetCurrentStatValue(SecondaryStatsEnum.Morale).ToString());

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

        public void SetHoverModalActive()
        {
            this._hoverModal.SetModalActive();
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

        private void DecorateFamilyOfTiles(TileController tile, Sprite deco)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = tile.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = 0.50f;
            renderer.color = color;
            this._decorateTileFamily.Add(tView);
        }

        private void DecorateSingleTile(TileController t, Sprite deco, float alpha = 0.50f)
        {
            var tView = new GameObject();
            var renderer = tView.AddComponent<SpriteRenderer>();
            renderer.sprite = deco;
            renderer.transform.position = t.Model.Center;
            renderer.sortingLayerName = CMapGUIControllerParams.MAP_GUI_LAYER;
            tView.name = "Path Tile";
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
            this._singleTile = tView;
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
    }
}
