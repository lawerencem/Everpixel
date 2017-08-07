using Assets.Model.Ability.Enum;
using Assets.Model.Combat;
using Controller.Map;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Kamehameha : MAbility
    {
        public Kamehameha() : base(EAbility.Kamehameha) { }

        public override List<TileController> GetAoETiles(AbilityArgContainer arg)
        {
            return base.GetRaycastTiles(arg);
        }

        public override List<Hit> Predict(AbilityArgContainer arg)
        {
            return base.PredictBullet(arg);
        }

        public override List<Hit> Process(AbilityArgContainer arg)
        {
            var hits = base.Process(arg);
            foreach (var hit in hits)
            {
                base.ProcessHitLoS(hit);
            }
            return hits;
        }

        public override bool IsValidActionEvent(AbilityArgContainer arg)
        {
            return true;
        }
    }
}
