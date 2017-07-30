using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Effects;

namespace Model.Events.Combat
{
    public class GenericEffectEvent : CombatEvent
    {
        public GenericEffect Effect { get; set; }
        public GenericCharacterController Target { get; set; }

        public GenericEffectEvent(CombatEventManager parent, GenericCharacterController target, GenericEffect effect) :
            base(CombatEventEnum.GenericEffect, parent)
        {
            this.Effect = effect;
            this.Target = target;
            this.Target.Model.AddEffect(this.Effect);
            this.RegisterEvent();
        }
    }
}
