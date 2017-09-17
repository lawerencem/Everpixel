using Assets.Controller.Character;
using Assets.Model.Action;
using Assets.Model.Character;
using Assets.Model.Character.Enum;
using Assets.Template.Script;
using Assets.View.GUI;
using System.Collections.Generic;
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
        public void SetComponentActiveForLifetime(string tag, bool active, float time)
        {
            if (this._guiComponents.ContainsKey(tag))
                this._guiComponents[tag].SetActive(active);
            var script = this._guiComponents[tag].AddComponent<SDeactivateByLifetime>();
            script.Init(this._guiComponents[tag], time);
        }
        public void SetGUILocked(bool locked) { this._guiLocked = locked; }
        public void SetHoverModalActive() { this._hoverModal.SetModalActive(); }
        public void SetHoverModalDamageActive(bool active) { this._hoverModal.SetDamageModalActive(active); }
        public void SetHoverModalDamageValues(MAction a) { this._hoverModal.SetModalDamageValues(a); }
        public void SetHoverModalInactive() { this._hoverModal.SetModalInactive(); }
        public void SetHoverModalHeaderText(string toSet) { this._hoverModal.SetModalHeaderText(toSet); }
        public void SetHoverModalLocation(Vector3 pos) { this._hoverModal.SetModalLocation(pos); }
        public void SetHoverModalStatValues(PChar c) { this._hoverModal.SetModalStatValues(c); }
        public void SetInteractionLocked(bool locked) { this._interactionLocked = locked; }

        private void Init()
        {
            var banner = GameObject.FindGameObjectWithTag(GameObjectTags.FATALITY_BANNER);
            this.AddComponent(GameObjectTags.FATALITY_BANNER, banner);
            this.SetComponentActive(GameObjectTags.FATALITY_BANNER, false);
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

        public void SetActingBoxToController(CChar c)
        {
            this.SetTagText(GameObjectTags.NAME, c.View.Name.Replace("_", " "));
            this.SetTagText(GameObjectTags.AP, ((int)c.Proxy.GetStat(ESecondaryStat.AP)) + " / " + ((int)c.Proxy.GetStat(ESecondaryStat.AP)).ToString());
            this.SetTagText(GameObjectTags.HP, ((int)c.Proxy.GetStat(ESecondaryStat.HP)) + " / " + ((int)c.Proxy.GetStat(ESecondaryStat.HP)).ToString());
            this.SetTagText(GameObjectTags.MORALE, ((int)c.Proxy.GetStat(ESecondaryStat.Morale)) + " / " + ((int)c.Proxy.GetStat(ESecondaryStat.Morale)).ToString());
            this.SetTagText(GameObjectTags.STAM, ((int)c.Proxy.GetStat(ESecondaryStat.Stamina)) + " / " + ((int)c.Proxy.GetStat(ESecondaryStat.Stamina)).ToString());

            if (c.Proxy.GetArmor() != null)
                this.SetTagText(GameObjectTags.ARMOR, c.Proxy.GetArmor().Model.Data.Name);
            else
                this.SetTagText(GameObjectTags.ARMOR, "");
            if (c.Proxy.GetHelm() != null)
                this.SetTagText(GameObjectTags.HELM, c.Proxy.GetHelm().Model.Data.Name);
            else
                this.SetTagText(GameObjectTags.HELM, "");
            if (c.Proxy.GetLWeapon() != null)
                this.SetTagText(GameObjectTags.L_WEAP, c.Proxy.GetLWeapon().Model.Data.Name);
            else
                this.SetTagText(GameObjectTags.L_WEAP, "");
            if (c.Proxy.GetRWeapon() != null)
                this.SetTagText(GameObjectTags.R_WEAP, c.Proxy.GetRWeapon().Model.Data.Name);
            else
                this.SetTagText(GameObjectTags.R_WEAP, "");
        }
    }
}
