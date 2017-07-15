﻿using Controller.Managers;
using Generics;
using Model.Characters;
using Model.Combat;
using Model.Events.Combat;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.GUI
{
    public class HoverModal : MonoBehaviour
    {
        private const string DMG_MODAL = "ModalDmgPrediction";
        private const string MODAL = "ModalTag";
        
        private const string MODAL_ARMOR_SLIDER = "ModalArmorSliderTag";        
        private const string MODAL_HEADER = "ModalHeaderTag";
        private const string MODAL_HELM_SLIDER = "ModalHelmSliderTag";
        private const string MODAL_HP_SLIDER = "ModalHPSliderTag";
        private const string MODAL_L_WEAPON_SLIDER = "ModalLWeaponSliderTag";
        private const string MODAL_MORALE_SLIDER = "ModalMoraleSliderTag";
        private const string MODAL_R_WEAPON_SLIDER = "ModalRWeaponSliderTag";
        private const string MODAL_STAM_SLIDER = "ModalStamSliderTag";

        private const string MODAL_BLOCK_TEXT = "BlockTextTag";
        private const string MODAL_CRIT_TEXT = "CritTextTag";
        private const string MODAL_DAMAGE_TEXT = "DmgTextTag";
        private const string MODAL_DODGE_TEXT = "DodgeTextTag";
        private const string MODAL_PARRY_TEXT = "ParryTextTag";

        private GameObject _dmgPredictionModal;
        private GameObject _modal;

        private Slider _armorSlider;
        private Slider _helmSlider;
        private Slider _hpSlider;
        private Slider _lWeaponSlider;
        private Slider _moraleSlider;
        private Slider _rWeaponSlider;
        private Slider _stamSlider;

        private Text _blockText;
        private Text _critText;
        private Text _dmgText;
        private Text _dodgeText;
        private Text _parryText;

        public void Init()
        {
            this._modal = GameObject.FindGameObjectWithTag(MODAL);
            this._dmgPredictionModal = GameObject.FindGameObjectWithTag(DMG_MODAL);

            var armor = GameObject.FindGameObjectWithTag(MODAL_ARMOR_SLIDER);
            var helm = GameObject.FindGameObjectWithTag(MODAL_HELM_SLIDER);
            var hp = GameObject.FindGameObjectWithTag(MODAL_HP_SLIDER);
            var lWeapon = GameObject.FindGameObjectWithTag(MODAL_L_WEAPON_SLIDER);
            var morale = GameObject.FindGameObjectWithTag(MODAL_MORALE_SLIDER);
            var rWeapon = GameObject.FindGameObjectWithTag(MODAL_R_WEAPON_SLIDER);
            var stam = GameObject.FindGameObjectWithTag(MODAL_STAM_SLIDER);

            var blockText = GameObject.FindGameObjectWithTag(MODAL_BLOCK_TEXT);
            var critText = GameObject.FindGameObjectWithTag(MODAL_CRIT_TEXT);
            var dmgText = GameObject.FindGameObjectWithTag(MODAL_DAMAGE_TEXT);
            var dodgeText = GameObject.FindGameObjectWithTag(MODAL_DODGE_TEXT);
            var parryText = GameObject.FindGameObjectWithTag(MODAL_PARRY_TEXT);

            this._armorSlider = armor.GetComponent<Slider>();
            this._helmSlider = helm.GetComponent<Slider>();
            this._hpSlider = hp.GetComponent<Slider>();
            this._lWeaponSlider = lWeapon.GetComponent<Slider>();
            this._moraleSlider = morale.GetComponent<Slider>();
            this._rWeaponSlider = rWeapon.GetComponent<Slider>();
            this._stamSlider = stam.GetComponent<Slider>();

            this._blockText = blockText.GetComponent<Text>();
            this._critText = critText.GetComponent<Text>();
            this._dmgText = dmgText.GetComponent<Text>();
            this._dodgeText = dodgeText.GetComponent<Text>();
            this._parryText = parryText.GetComponent<Text>();

        }

        public void SetModalActive()
        {
            if (!this._modal.activeSelf)
                this._modal.SetActive(true);
        }

        public void SetDamageModalInactive()
        {
            this._dmgPredictionModal.SetActive(false);
        }

        public void SetModalDamageValues(PredictActionEvent e)
        {
            if (CombatEventManager.Instance.GetCurrentAbility() != null)
            {
                var hit = e.Container.Hits.Find(x => x.Target.Equals(e.Container.Target.Model.Current));
                if (hit != null)
                    this.SetModalDamageValuesHelper(hit);
            }
        }

        public void SetModalInactive()
        {
            this._modal.SetActive(false);
        }

        public void SetModalHeaderText(string toSet)
        {
            this.SetModalActive();
            var header = GameObject.FindGameObjectWithTag(MODAL_HEADER);
            var text = header.GetComponent<Text>();
            text.text = toSet;
        }

        public void SetModalLocation(Vector3 pos)
        {
            this.SetModalActive();
            this._modal.transform.position = pos;
        }

        public void SetModalStatValues(GenericCharacter c)
        {
            if (c.Armor != null)
            {
                this._armorSlider.maxValue = c.Armor.MaxDurability;
                this._armorSlider.value = c.Armor.Durability;
            }
            else
                this._armorSlider.maxValue = 0;
            if (c.Helm != null)
            {
                this._helmSlider.maxValue = c.Helm.MaxDurability;
                this._helmSlider.value = c.Helm.Durability;
            }
            else
                this._helmSlider.maxValue = 0;
            if (c.LWeapon != null)
            {
                this._lWeaponSlider.maxValue = c.LWeapon.MaxDurability;
                this._lWeaponSlider.value = c.LWeapon.Durability;
            }
            else
                this._lWeaponSlider.maxValue = 0;
            if (c.RWeapon != null)
            {
                this._rWeaponSlider.maxValue = c.RWeapon.MaxDurability;
                this._rWeaponSlider.value = c.RWeapon.Durability;
            }
            else
                this._rWeaponSlider.maxValue = 0;

            this._hpSlider.maxValue = (int)c.GetCurrentStatValue(SecondaryStatsEnum.HP);
            this._hpSlider.value = c.CurrentHP;
            this._moraleSlider.maxValue = (int)c.GetCurrentStatValue(SecondaryStatsEnum.Morale);
            this._moraleSlider.value = c.CurrentMorale;
            this._stamSlider.maxValue = (int)c.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
            this._stamSlider.value = c.CurrentStamina;
        }

        private void SetModalDamageValuesHelper(HitInfo hit)
        {
            this._dmgPredictionModal.SetActive(true);
            this._blockText.text = Math.Truncate(hit.Chances.Block * 100).ToString() + " %";
            this._critText.text = Math.Truncate(hit.Chances.Crit * 100).ToString() + " %";
            this._dodgeText.text = Math.Truncate(hit.Chances.Dodge * 100).ToString() + " %";
            if (hit.IsHeal)
                this._dmgText.text = "+ " + Math.Truncate(hit.Chances.Damage).ToString();
            else
                this._dmgText.text = Math.Truncate(hit.Chances.Damage).ToString();
            this._parryText.text = Math.Truncate(hit.Chances.Parry * 100).ToString() + " %";
        }
    }
}