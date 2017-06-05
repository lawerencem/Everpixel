using Controller.Characters;
using Controller.Managers;
using Controller.Map;

namespace Model.Events.Combat
{
    public class ShowPotentialPathEvent : CombatEvent
    {
        public GenericCharacterController Character;
        public TileController Target;

        public ShowPotentialPathEvent(
            GenericCharacterController c, 
            TileController t,
            CombatEventManager parent) : 
            base(CombatEventEnum.ShowPotentialPath, parent)
        {
            if (!this._parent.GetInteractionLock() && !this._parent.GetGUILock())
            {
                this.Character = c;
                this.Target = t;
                this.RegisterEvent();
            }
        }
    }
}
