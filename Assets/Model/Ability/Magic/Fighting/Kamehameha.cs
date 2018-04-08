using Assets.Controller.Map.Tile;
using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;
using System.Collections.Generic;

namespace Assets.Model.Ability.Magic.Fighting
{
    public class Kamehameha : MAbility
    {
        public Kamehameha() : base(EAbility.Kamehameha) { }

        public override List<CTile> GetAoETiles(AbilityArgs arg)
        {
            return base.GetTargetableRaycastTiles(arg);
        }

        public override void Predict(MHit hit)
        {
            base.ProcessRay(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessRay(hit);
        }
    }
}
