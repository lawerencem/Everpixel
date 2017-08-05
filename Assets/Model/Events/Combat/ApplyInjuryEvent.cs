using Controller.Characters;
using Controller.Managers;
using Model.Combat;
using Model.Injuries;

namespace Model.Events.Combat
{
    public class ApplyInjuryEvent : CombatEvent
    {
        public GenericInjury Injury { get; set; }
        public CharController Target { get; set; }

        public ApplyInjuryEvent(CombatEventManager parent, Hit hit, GenericInjury injury) :
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
