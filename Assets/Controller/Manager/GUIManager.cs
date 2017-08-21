namespace Assets.Controller.Manager
{
    public class GUIManager
    {
        private bool _guiLocked = false;
        private bool _interactionLocked = false;

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

        public void CallbackLockGUI(object o) { this._guiLocked = true; }
        public void CallbackLockInteraction(object o) { this._interactionLocked = true; }

        public void CallbackUnlockGUI(object o) { this._guiLocked = false; }
        public void CallbackUnlockInteraction(object o) { this._interactionLocked = false; }

        public bool GetGUILocked() { return this._guiLocked; }
        public bool GetInteractionLocked() { return this._interactionLocked; }

        public void SetGUILocked(bool locked) { this._guiLocked = locked; }
        public void SetInteractionLocked(bool locked) { this._interactionLocked = locked; }
    }
}
