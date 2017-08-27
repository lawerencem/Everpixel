using Assets.Controller.Character;
using Assets.Model.Abiltiy.Logic;
using Assets.Model.Combat.Hit;
using Template.Utility;

namespace Assets.Model.Ability.Logic.Calculator
{
    public class DodgeCalculator : AAbilityCalculator
    {
        public override  void Predict(Hit hit)
        {
            var target = hit.Data.Target.Current as CharController;
            var targetArmor = target.Model.GetEquipment().GetArmor();
            var targetHelm = target.Model.GetEquipment().GetHelm();

            var acc = hit.Data.Source.Model.GetCurrentStats().GetSecondaryStats().MeleeSkill;

            var dodge = target.Model.GetCurrentStats().GetSecondaryStats().DodgeSkill;
            var dodgeChance = LogicParams.BASE_DODGE_CHANCE / hit.Data.Ability.Data.AccMod;

            if (targetArmor != null)
                dodgeChance *= targetArmor.DodgeMod;
            if (targetHelm != null)
                dodgeChance *= targetHelm.DodgeMod;

            acc *= hit.Data.Ability.Data.AccMod;

            hit.Data.Chances.Dodge = this.GetAttackVSDefenseSkillChance(acc, dodge, dodgeChance);
            hit.Data.Chances.Dodge *= hit.Data.Ability.Data.DodgeMod;
        }

        public override void Process(Hit hit)
        {
            this.Predict(hit);
            var roll = RNG.Instance.NextDouble();
            if (hit.Data.Chances.Dodge > roll)
                FHit.SetDodgeTrue(hit.Data.Flags);
        }
    }
}
