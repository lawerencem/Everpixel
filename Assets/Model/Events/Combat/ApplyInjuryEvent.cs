using Controller.Characters;
using Controller.Managers;
using Model.Combat;
using Model.Injuries;

namespace Model.Events.Combat
{
    public class ApplyInjuryEvent : CombatEvent
    {
        public GenericInjury Injury { get; set; }
        public GenericCharacterController Target { get; set; }

        public ApplyInjuryEvent(CombatEventManager parent, HitInfo hit, GenericInjury injury) :
            base(CombatEventEnum.ApplyInjury, parent)
        {
            var exists = hit.Target.Model.Injuries.Find(x => x.Type == injury.Type);
            if (exists == null)
            {
                this.Injury = injury;
                this.Target = hit.Target;
                this.Target.Model.AddInjury(injury);
                this.RegisterEvent();
            }
        }
    }
}
