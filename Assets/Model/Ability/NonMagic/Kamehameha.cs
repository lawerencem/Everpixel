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
            this.CastType = CastTypeEnum.Raycast;
            this.MagicType = Magic.MagicTypeEnum.Fighting;
        }

        public override List<TileController> GetAoETiles(TileController source, TileController target, int range)
        {
            return base.GetRaycastTiles(source, target, range);
        }

        public override void PredictAbility(HitInfo hit)
        {
            base.PredictBullet(hit);
        }

        public override void ProcessAbility(PerformActionEvent e, HitInfo hit)
        {
            base.ProcessAbility(e, hit);
            base.ProcessLoS(hit);
        }

        public override bool IsValidActionEvent(PerformActionEvent e)
        {
            return true;
        }
    }
}
