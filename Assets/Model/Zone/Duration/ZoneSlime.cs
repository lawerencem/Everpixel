using Assets.Controller.Character;

namespace Assets.Model.Zone.Duration
{
    public class ZoneSlime : ADurationZone
    {
        public ZoneSlime() : base(EZone.Zone_Slime) { }

        public override void ProcessEnterZone(CChar target)
        {
            base.ProcessEnterZone(target);
        }
    }
}
