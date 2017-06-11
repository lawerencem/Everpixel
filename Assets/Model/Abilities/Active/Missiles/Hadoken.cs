using Model.Combat;

namespace Model.Abilities
{
    public class Hadoken : GenericActiveAbility
    {
        public Hadoken() : base(ActiveAbilitiesEnum.Hadoken) { }

        public override void ProcessAbility(HitInfo hit)
        {

        }
    }
}
