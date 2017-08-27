using System.Collections.Generic;
using Template.Script;
using UnityEngine;

namespace Assets.Controller.Manager.GUI
{
    public class GUIManager
    {
        private bool _guiLocked = false;
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
            //this._hoverModal = new HoverModal();

            //this._abilityModal.Init();
            //this._hoverModal.Init();

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
        public void SetInteractionLocked(bool locked) { this._interactionLocked = locked; }

        private void Init()
        {
            var banner = GameObject.FindGameObjectWithTag(GameObjectTags.BANNER);
            this.AddComponent(GameObjectTags.BANNER, banner);
            this.SetComponentActive(GameObjectTags.BANNER, false);
        }
    }
}
