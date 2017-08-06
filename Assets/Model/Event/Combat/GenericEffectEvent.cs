using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Effects;

namespace Model.Events.Combat
{
    public class GenericEffectEvent : CombatEvent
    {
        public MEffect Effect { get; set; }
        public CharController Target { get; set; }

        public GenericEffectEvent(CombatEventManager parent, CharController target, MEffect effect) :
            base(ECombatEv.GenericEffect, parent)
        {
            this.Effect = effect;
            this.Target = target;
            this.Target.Model.AddEffect(this.Effect);
            this.RegisterEvent();
        }
    }
}
