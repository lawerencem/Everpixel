using Controller.Characters;
using Controller.Map;
using Model.Abilities;
using Model.Characters;

namespace Model.Combat
{
    public class HitInfo
    {
        private Callback _callBack;
        public delegate void Callback();

        public GenericAbility Ability { get; set; }
        public AttackEventFlags Flags { get; set; }
        public bool FXProcessed { get; set; }
        public bool IsFinished { get; set; }
        public bool IsHeal { get; set; }
        public int Dmg { get; set; }
        public GenericCharacterController Source { get; set; }
        public GenericCharacterController Target { get; set; }
        public TileController TargetTile { get; set; }

        public HitInfo(GenericCharacterController s, TileController t, GenericAbility a, Callback callback = null)
        {
            this.IsFinished = false;
            this.Source = s;
            if (t.Model.Current != null && t.Model.Current.GetType().Equals(typeof(GenericCharacterController)))
                this.Target = t.Model.Current as GenericCharacterController;
            this.TargetTile = t;
            this.Ability = a;
            this._callBack = callback;
            this.Flags = new AttackEventFlags();
        }

        public void Done()
        {
            if (this._callBack != null)
            {
                this.IsFinished = true;
                this._callBack();
            }
        }
    }
}
