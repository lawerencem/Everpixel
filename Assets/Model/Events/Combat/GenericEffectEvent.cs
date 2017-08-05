using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Effects;

namespace Model.Events.Combat
{
    public class GenericEffectEvent : CombatEvent
    {
        public Effect Effect { get; set; }
        public CharController Target { get; set; }

        public GenericEffectEvent(CombatEventManager parent, CharController target, Effect effect) :
            base(CombatEventEnum.GenericEffect, parent)
        {
            this.Effect = effect;
            this.Target = target;
            this.Target.Model.AddEffect(this.Effect);
            this.RegisterEvent();
        }
    }
}
