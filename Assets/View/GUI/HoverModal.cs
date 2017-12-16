using Assets.Model.Action;
using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.Model.Equipment.Enum;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.View.GUI
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
        private const string RESIST_PARRY_TEXT = "ResistTextTag";

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
        private Text _resistText;

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
            var resistText = GameObject.FindGameObjectWithTag(RESIST_PARRY_TEXT);

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
            this._resistText = parryText.GetComponent<Text>();
        }

        public void SetModalActive()
        {
            if (!this._modal.activeSelf)
                this._modal.SetActive(true);
        }

        public void SetDamageModalActive(bool active)
        {
            this._dmgPredictionModal.SetActive(active);
        }

        public void SetModalDamageValues(MAction a)
        {
            var hit = a.Data.Hits.Find(x => x.Data.Target.Equals(a.Data.Target));
            if (hit != null)
                this.SetModalDamageValuesHelper(hit);
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

        public void SetModalStatValues(PChar c)
        {
            if (c.GetArmor() != null)
            {
                this._armorSlider.maxValue = (float)c.GetArmor().GetStat(EArmorStat.Durability);
                this._armorSlider.value = (float)c.GetArmor().GetStat(EArmorStat.Durability);
            }
            else
                this._armorSlider.maxValue = 0;
            if (c.GetHelm() != null)
            {
                this._helmSlider.maxValue = (float)c.GetHelm().GetStat(EArmorStat.Durability);
                this._helmSlider.value = (float)c.GetHelm().GetStat(EArmorStat.Durability);
            }
            else
                this._helmSlider.maxValue = 0;
            if (c.GetLWeapon() != null)
            {
                this._lWeaponSlider.maxValue = (float)c.GetLWeapon().GetStat(EWeaponStat.Max_Durability);
                this._lWeaponSlider.value = (float)c.GetLWeapon().GetStat(EWeaponStat.Max_Durability);
            }
            else
                this._lWeaponSlider.maxValue = 0;
            if (c.GetRWeapon() != null)
            {
                this._rWeaponSlider.maxValue = (float)c.GetRWeapon().GetStat(EWeaponStat.Max_Durability);
                this._rWeaponSlider.value = (float)c.GetRWeapon().GetStat(EWeaponStat.Max_Durability);
            }
            else
                this._rWeaponSlider.maxValue = 0;

            this._hpSlider.maxValue = (int)c.GetStat(ESecondaryStat.HP);
            this._hpSlider.value = (int)c.GetPoints(ESecondaryStat.HP);
            this._moraleSlider.maxValue = (int)c.GetStat(ESecondaryStat.Morale);
            this._moraleSlider.value = (int)c.GetPoints(ESecondaryStat.Morale);
            this._stamSlider.maxValue = (int)c.GetStat(ESecondaryStat.Stamina);
            this._stamSlider.value = (int)c.GetPoints(ESecondaryStat.Stamina);
        }

        private void SetModalDamageValuesHelper(MHit hit)
        {
            this._dmgPredictionModal.SetActive(true);
            this._blockText.text = ((int)(hit.Data.Chances.Block * 100)).ToString() + " %";
            this._critText.text = ((int)(hit.Data.Chances.Crit * 100)).ToString() + " %";
            this._dodgeText.text = ((int)(hit.Data.Chances.Dodge * 100)).ToString() + " %";
            if (hit.Data.IsHeal)
                this._dmgText.text = "+ " + Math.Truncate(hit.Data.Chances.Damage).ToString();
            else
                this._dmgText.text = ((int)(hit.Data.Chances.Damage)).ToString();
            this._parryText.text = ((int)(hit.Data.Chances.Parry * 100)).ToString() + " %";
        }
    }
}