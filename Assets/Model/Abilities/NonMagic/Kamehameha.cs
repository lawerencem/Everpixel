using System.Collections.Generic;
using Controller.Map;
using Model.Combat;
using Model.Events.Combat;

namespace Model.Abilities
{
    public class Kamehameha : GenericAbility
    {
        public Kamehameha() : base(AbilitiesEnum.Kamehameha)
        {
            this.CastType = AbilityCastTypeEnum.LOS_Cast;
            this.MagicType = Magic.MagicTypeEnum.Fighting;
        }

        public override List<TileController> GetAoETiles(PerformActionEvent e)
        {
            return base.GetLOSTiles(e);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessLoS(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
