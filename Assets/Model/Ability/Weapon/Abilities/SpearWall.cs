using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
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

        }

        public override void Process(MHit hit)
        {
            base.ProcessHitMelee(hit);
            FActionStatus.SetSpearWallingTrue(hit.Data.Action.Data.Source.Proxy.GetActionFlags());
        }

        public override void DisplayFX(MAction action)
        {
            var util = new VWeaponUtil();
            util.DoSpearWallFX(action);

            foreach (var hit in action.Data.Hits)
                hit.CallbackHandler(null);
        }
    }
}
