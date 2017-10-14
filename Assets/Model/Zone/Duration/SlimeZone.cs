using Assets.Controller.Character;

namespace Assets.Model.Zone.Duration
{
    public class SlimeZone : ADurationZone
    {
        public SlimeZone() : base(EZone.Slime_Zone) { }

        public override void ProcessEnterZone(CChar target)
        {
            base.ProcessEnterZone(target);

        }
    }
}
