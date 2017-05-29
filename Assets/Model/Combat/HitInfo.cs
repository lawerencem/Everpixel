using Controller.Characters;
using Model.Abilities;
using Model.Characters;

namespace Model.Combat
{
    public class HitInfo
    {
        public GenericAbility Ability { get; set; }
        public AttackEventFlags Flags { get; set; }
        public int Dmg { get; set; }
        public GenericCharacterController Source { get; set; }
        public GenericCharacterController Target { get; set; }

        public HitInfo(GenericCharacterController s, GenericCharacterController t, GenericAbility a)
        {
            this.Source = s;
            this.Target = t;
            this.Ability = a;
            this.Flags = new AttackEventFlags();
        }
    }
}
