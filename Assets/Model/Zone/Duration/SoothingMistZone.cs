//using Assets.Model.Ability;
//using Assets.Model.Ability.Enum;
//using Assets.Model.Character.Enum;
//using Model.Events.Combat;

//namespace Assets.Model.Zone.Duration
//{
//    public class SoothingMistZone : ASpellZone
//    {
//        public SoothingMistZone(ZoneArgsCont arg) : base(arg) { }

//        public override void ProcessEnterZone(EvZoneEnter e)
//        {
//            var proto = AbilityTable.Instance.Table[EAbility.Soothing_Mist];
//            var heal = ((proto.Params.FlatDamage) + (proto.Params.DmgPerPower * this._caster.Model.GetCurrentStatValue(ESecondaryStat.Power)));
//            var modifyHP = new ModifyHPEvent(CombatEventManager.Instance, e.Character.Model, (int)heal, true);
//        }
//    }
//}