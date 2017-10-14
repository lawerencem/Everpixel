using Assets.Model.Character.Builder;

namespace Assets.Model.Character.Param
{
    public class SStats
    {
        public int MaxAP { get; set; }
        public double Block { get; set; }
        public double Concentration { get; set; }
        public double CriticalChance { get; set; }
        public double CriticalMultiplier { get; set; }
        public double DamageIgnore { get; set; }
        public double DamageReduce { get; set; }
        public double DodgeSkill { get; set; }
        public double Fortitude { get; set; }
        public double Initiative { get; set; }
        public int MaxHP { get; set; }
        public double MeleeSkill { get; set; }
        public int Morale { get; set; }
        public double ParrySkill { get; set; }
        public double Power { get; set; }
        public double RangedSkill { get; set; }
        public double Reflex { get; set; }
        public double SpellDuration { get; set; }
        public double SpellPenetration { get; set; }
        public int Stamina { get; set; }
        public double Will { get; set; }

        public SStats() { }
        public SStats(PStats p)
        {
            this.MaxAP = SecondaryStatReferee.DetermineActionPoints(p);
            this.Block = SecondaryStatReferee.DetermineBlock(p);
            this.Concentration = SecondaryStatReferee.DetermineConcentration(p);
            this.CriticalChance = SecondaryStatReferee.DetermineCriticalChance(p);
            this.CriticalMultiplier = SecondaryStatReferee.DetermineCriticalMultiplier(p);
            this.DamageIgnore = 0;
            this.DamageReduce = 1;
            this.DodgeSkill = SecondaryStatReferee.DetermineDodge(p);
            this.Fortitude = SecondaryStatReferee.DetermineFortitude(p);
            this.Initiative = SecondaryStatReferee.DetermineInitiative(p);
            this.MaxHP = SecondaryStatReferee.DetermineMaxHP(p);
            this.MeleeSkill = SecondaryStatReferee.DetermineMeleeSkill(p);
            this.Morale = SecondaryStatReferee.DetermineMoraleSkill(p);
            this.ParrySkill = SecondaryStatReferee.DetermineParry(p);
            this.Power = SecondaryStatReferee.DeterminePower(p);
            this.RangedSkill = SecondaryStatReferee.DetermineRangedSkill(p);
            this.Reflex = SecondaryStatReferee.DetermineReflexe(p);
            this.SpellDuration = SecondaryStatReferee.DetermineSpellDuration(p);
            this.SpellPenetration = SecondaryStatReferee.DetermineSavingThrowModifier(p);
            this.Stamina = SecondaryStatReferee.DetermineStamina(p);
            this.Will = SecondaryStatReferee.DetermineWill(p);
        }
    }
}

