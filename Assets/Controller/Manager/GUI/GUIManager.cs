using Assets.Controller.Character;
using Assets.Model.Character;
using Assets.View.GUI;
using System.Collections.Generic;
using Template.Script;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Controller.Manager.GUI
{
    public class GUIManager
    {
        private bool _guiLocked = false;
        private HoverModal _hoverModal;
        private bool _interactionLocked = false;
        private Dictionary<string, GameObject> _guiComponents;

        private static GUIManager _instance;
        public static GUIManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GUIManager();
                return _instance;
            }
        }

        public GUIManager()
        {
            this._guiComponents = new Dictionary<string, GameObject>();

            //this._abilityModal = new AbilitiesModal();
            this._hoverModal = new HoverModal();

            //this._abilityModal.Init();
            this._hoverModal.Init();

            this.Init();
        }

        public void AddComponent(string tag, GameObject o) { this._guiComponents.Add(tag, o); }
        public void DeactivateComponentByLifetime(string tag, float life)
        {
            if (this._guiComponents.ContainsKey(tag))
            {
                var component = this._guiComponents[tag];
                var script = component.AddComponent<SDeactivateByLifetime>();
                script.Init(component, life);
            }
        }
        public void CallbackLockGUI(object o) { this._guiLocked = true; }
        public void CallbackLockInteraction(object o) { this._interactionLocked = true; }
        public void CallbackUnlockGUI(object o) { this._guiLocked = false; }
        public void CallbackUnlockInteraction(object o) { this._interactionLocked = false; }
        public GameObject GetComponent(string tag)
        {
            if (this._guiComponents.ContainsKey(tag))
                return this._guiComponents[tag];
            else
                return null;
        }
        public bool GetGUILocked() { return this._guiLocked; }
        public bool GetInteractionLocked() { return this._interactionLocked; }
        public void RemoveComponent(string tag) { this._guiComponents.Remove(tag); }
        public void SetComponentActive(string tag, bool active)
        {
            if (this._guiComponents.ContainsKey(tag))
                this._guiComponents[tag].SetActive(active);
        }
        public void SetGUILocked(bool locked) { this._guiLocked = locked; }
        public void SetHoverModalActive() { this._hoverModal.SetModalActive(); }
        //public void SetHoverModalDamageValues(EvPredictAction e) { this._hoverModal.SetModalDamageValues(e); }
        public void SetHoverModalInactive() { this._hoverModal.SetModalInactive(); }
        public void SetHoverModalHeaderText(string toSet) { this._hoverModal.SetModalHeaderText(toSet); }
        public void SetHoverModalLocation(Vector3 pos) { this._hoverModal.SetModalLocation(pos); }
        public void SetHoverModalStatValues(MChar c) { this._hoverModal.SetModalStatValues(c); }
        public void SetInteractionLocked(bool locked) { this._interactionLocked = locked; }

        private void Init()
        {
            var banner = GameObject.FindGameObjectWithTag(GameObjectTags.BANNER);
            this.AddComponent(GameObjectTags.BANNER, banner);
            this.SetComponentActive(GameObjectTags.BANNER, false);
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

        public void SetActingBoxToController(CharController c)
        {
            this.SetTagText(GameObjectTags.NAME, c.View.Name);
            this.SetTagText(GameObjectTags.AP, c.Model.GetCurrentAP() + " / " + ((int)c.Model.GetCurrentPoints().CurrentAP).ToString());
            this.SetTagText(GameObjectTags.HP, c.Model.GetCurrentHP() + " / " + ((int)c.Model.GetCurrentPoints().CurrentHP).ToString());
            this.SetTagText(GameObjectTags.STAM, c.Model.GetCurrentStamina() + " / " + ((int)c.Model.GetCurrentPoints().CurrentStamina).ToString());
            this.SetTagText(GameObjectTags.MORALE, c.Model.GetCurrentMorale() + " / " + ((int)c.Model.GetCurrentPoints().CurrentMorale).ToString());

            if (c.Model.GetEquipment().GetArmor() != null)
                this.SetTagText(GameObjectTags.ARMOR, c.Model.GetEquipment().GetArmor().Name);
            else
                this.SetTagText(GameObjectTags.ARMOR, "");
            if (c.Model.GetEquipment().GetHelm() != null)
                this.SetTagText(GameObjectTags.HELM, c.Model.GetEquipment().GetHelm().Name);
            else
                this.SetTagText(GameObjectTags.HELM, "");
            if (c.Model.GetEquipment().GetLWeapon() != null)
                this.SetTagText(GameObjectTags.L_WEAP, c.Model.GetEquipment().GetLWeapon().Name);
            else
                this.SetTagText(GameObjectTags.L_WEAP, "");
            if (c.Model.GetEquipment().GetRWeapon() != null)
                this.SetTagText(GameObjectTags.R_WEAP, c.Model.GetEquipment().GetRWeapon().Name);
            else
                this.SetTagText(GameObjectTags.R_WEAP, "");
        }
    }
}
