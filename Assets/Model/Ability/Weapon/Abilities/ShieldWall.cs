using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.View.Equipment;

namespace Assets.Model.Weapon.Abilities
{
    public class ShieldWall : MAbility
    {
        public ShieldWall() : base(EAbility.Shield_Wall) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {

        }

        public override void Process(MHit hit)
        {
            FActionStatus.SetShieldWallingTrue(hit.Data.Action.Data.Source.Proxy.GetActionFlags());
        }

        public override void DisplayFX(MAction action)
        {
            var util = new VWeaponUtil();
            util.DoShieldWall(action);

            foreach (var hit in action.Data.Hits)
                hit.CallbackHandler(null);
        }
    }
}
