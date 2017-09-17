using Assets.Model.Character.Param;

namespace Assets.Model.Character.Builder
{
    public class SecondaryStatReferee
    {
        private static readonly double ActionPoints_Agi = 0.6;
        private static readonly double ActionPoints_Int = 0.4;
        private static readonly double BaseHPAndStamina = 50;
        private static readonly double Block_Agi = 0.4;
        private static readonly double Block_Might = 0.4;
        private static readonly double Block_Per = 0.2;
        private static readonly double Concentration_Con = 0.4;
        private static readonly double Concentration_Res = 0.6;
        private static readonly double CriticalChance_Agi = 0.5;
        private static readonly double CriticalChance_Might = 0.5;
        private static readonly double CriticalMultiplier_Might = 0.5;
        private static readonly double CriticalMultiplier_Per = 0.5;
        private static readonly double Dodge_Agi = 0.4;
        private static readonly double Dodge_Per = 0.6;
        private static readonly double Fortitude_Con = 0.6;
        private static readonly double Fortitude_Res = 0.4;
        private static readonly double MaxHP_Con = 0.8;
        private static readonly double MaxHP_Res = 0.2;
        private static readonly double MeleeSkill_Agi = 0.2;
        private static readonly double MeleeSkill_Int = 0.2;
        private static readonly double MeleeSkill_Might = 0.6;
        private static readonly double Morale_Con = 0.3;
        private static readonly double Morale_Int = 0.2;
        private static readonly double Morale_Res = 0.5;
        private static readonly double Initiative_Agi = 0.4;
        private static readonly double Initiative_Con = 0.4;
        private static readonly double Initiative_Per = 0.2;
        private static readonly double Parry_Agi = 0.4;
        private static readonly double Parry_Int = 0.4;
        private static readonly double Parry_Per = 0.2;
        private static readonly double Power_Might = 0.8;
        private static readonly double Power_Res = 0.2;
        private static readonly double RangedSkill_Agi = 0.2;
        private static readonly double RangedSkill_Per = 0.8;
        private static readonly double Reflexes_Agi = 0.6;
        private static readonly double Reflexes_Per = 0.4;
        private static readonly double SpellDur_Con = 0.2;
        private static readonly double SpellDur_Int = 0.2;
        private static readonly double SpellDur_Res = 0.6;
        private static readonly double SpellPen_Int = 0.6;
        private static readonly double SpellPen_Res = 0.4;
        private static readonly double Stamina_Con = 0.6;
        private static readonly double Stamina_Might = 0.2;
        private static readonly double Stamina_Res = 0.2;
        private static readonly double Will_Int = 0.2;
        private static readonly double Will_Res = 0.8;

        public static int DetermineActionPoints(PrimaryStats p) { return (int)(((ActionPoints_Agi * p.Agility) + (ActionPoints_Int * p.Intelligence)) / 200) + 12; }
        public static int DetermineBlock(PrimaryStats p) { return (int) ((Block_Agi * p.Agility) + (Block_Might * p.Might) + (Block_Per * p.Perception) / 2); }
        public static int DetermineConcentration(PrimaryStats p) { return (int) ((Concentration_Con * p.Constitution) + (Concentration_Res * p.Resolve) / 2); }
        public static int DetermineCriticalChance(PrimaryStats p) { return (int) ((CriticalChance_Agi * p.Agility) + (CriticalChance_Might * p.Might) / 2); }
        public static int DetermineCriticalMultiplier(PrimaryStats p) { return (int) ( (CriticalMultiplier_Might * p.Might) + (CriticalMultiplier_Per * p.Perception) / 2); }
        public static int DeterminePower(PrimaryStats p) { return (int) ( (Power_Might * p.Might) + (Power_Res* p.Resolve) / 2); }
        public static int DetermineDodge(PrimaryStats p) { return (int) ( (Dodge_Agi * p.Agility) + (Dodge_Per * p.Resolve) / 2); }
        public static int DetermineFortitude(PrimaryStats p) { return (int) ( (Fortitude_Con * p.Constitution) + (Fortitude_Res * p.Resolve) / 2); }
        public static int DetermineInitiative(PrimaryStats p) { return (int) ( (Initiative_Agi * p.Agility) + (Initiative_Con * p.Constitution) + (Initiative_Per * p.Perception) / 2); }
        public static int DetermineMaxHP(PrimaryStats p) { return (int) ( (((MaxHP_Con * p.Constitution) + (MaxHP_Res * p.Resolve)) * 0.25) + BaseHPAndStamina); }
        public static int DetermineMeleeSkill(PrimaryStats p) { return (int) ( (MeleeSkill_Agi * p.Agility) + (MeleeSkill_Int * p.Intelligence) + (MeleeSkill_Might + p.Might) / 2); }
        public static int DetermineMoraleSkill(PrimaryStats p) { return (int) (((Morale_Con * p.Constitution) + (Morale_Int * p.Intelligence) + (Morale_Res + p.Resolve)) * 0.25); }
        public static int DetermineParry(PrimaryStats p) { return (int) ( (Parry_Agi * p.Agility) + (Parry_Int * p.Intelligence) + (Parry_Per * p.Perception) / 2); }
        public static int DetermineRangedSkill(PrimaryStats p) { return (int) ( (RangedSkill_Agi * p.Agility) + (RangedSkill_Per * p.Perception) / 2); }
        public static int DetermineReflexe(PrimaryStats p) { return (int) ( (Reflexes_Agi * p.Agility) + (Reflexes_Per * p.Perception) / 2); }
        public static int DetermineSavingThrowModifier(PrimaryStats p) { return (int) ( (SpellPen_Int * p.Intelligence) + (SpellPen_Res * p.Resolve) / 2); }
        public static int DetermineSpellDuration(PrimaryStats p) { return (int)((SpellDur_Con * p.Constitution) + (SpellDur_Int * p.Intelligence) + (SpellDur_Res * p.Resolve) / 2); }
        public static int DetermineStamina(PrimaryStats p) { return (int) ( (((Stamina_Con * p.Constitution) + (Stamina_Might * p.Might) + (Stamina_Res * p.Resolve)) * 0.25) + BaseHPAndStamina); }
        public static int DetermineWill(PrimaryStats p) { return (int) ( (Will_Int * p.Intelligence) + (Will_Res * p.Resolve) / 2); }
    }
}
