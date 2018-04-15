using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Combat.Hit;
using Assets.View;
using Assets.View.Equipment;
using UnityEngine;

namespace Assets.Model.Weapon.Abilities
{
    public class SpearWall : MAbility
    {
        public SpearWall() : base(EAbility.Spear_Wall) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {
            base.PredictSingle(hit);
        }

        public override void Process(MHit hit)
        {
            base.ProcessSingle(hit);
        }

        public override void DisplayFX(MAction a)
        {
            var util = new VWeaponUtil();
            util.DoSpearWallFX(a);

            foreach (var hit in a.Data.Hits)
                hit.CallbackHandler(null);
        }
    }
}
