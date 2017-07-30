using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Characters;

namespace Model.Events.Combat
{
    public class BuffEvent : CombatEvent
    {
        public GenericCharacterController ToBuff { get; set; }

        public BuffEvent(CombatEventManager parent, FlatSecondaryStatModifier buff, GenericCharacterController toBuff) :
            base(CombatEventEnum.Buff, parent)
        {
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }

        public BuffEvent(CombatEventManager parent, SecondaryStatModifier buff, GenericCharacterController toBuff) :
            base(CombatEventEnum.Buff, parent)
        {
            this.ToBuff = toBuff;
            toBuff.Model.TryAddMod(buff);
            this.RegisterEvent();
        }
    }
}
