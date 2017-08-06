using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Controller.Managers;
using Model.Characters;
using Model.Events.Combat;

namespace Assets.Model.Zone.Duration
{
    public class SoothingMistZone : ASpellZone
    {
        public SoothingMistZone(ZoneArgsContainer arg) : base(arg) { }

        public override void ProcessEnterZone(ZoneEnterEvent e)
        {
            var proto = AbilityTable.Instance.Table[EAbility.Soothing_Mist];
            var heal = ((proto.Params.FlatDamage) + (proto.Params.DmgPerPower * this._caster.Model.GetCurrentStatValue(ESecondaryStat.Power)));
            var modifyHP = new ModifyHPEvent(CombatEventManager.Instance, e.Character.Model, (int)heal, true);
        }
    }
}