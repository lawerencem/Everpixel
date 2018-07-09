using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Model.Action;
using Assets.Model.Character.Enum;
using Assets.Model.Combat.Hit;
using Assets.View.Equipment;

namespace Assets.Model.Weapon.Abilities
{
    public class Riposte : MAbility
    {
        public Riposte() : base(EAbility.Riposte) { this._wpnAbility = true; }

        public override void Predict(MHit hit)
        {

        }

        public override void Process(MHit hit)
        {
            FActionStatus.SetRipostingTrue(hit.Data.Action.Data.Source.Proxy.GetActionFlags());
        }

        public override void DisplayFX(MAction action)
        {
            var util = new VWeaponUtil();
            util.DoRiposte(action);

            foreach (var hit in action.Data.Hits)
                hit.CallbackHandler(null);
        }
    }
}
