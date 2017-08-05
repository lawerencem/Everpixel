namespace Assets.Model.Abiltiy.Logic
{
    public class LogicParams
    {
        public static readonly double BASE_BLOCK_CHANCE = 0.25;
        public static readonly double BASE_BODY_RATIO = BASE_HIT_RATIO - 1;
        public static readonly double BASE_CHANCE = 1.0;
        public static readonly double BASE_CRIT_CHANCE = 0.15;
        public static readonly double BASE_CRIT_SCALAR = 1.5;
        public static readonly double BASE_DODGE_CHANCE = 0.20;
        public static readonly double BASE_HEAD_CHANCE = 0.25;
        public static readonly double BASE_HIT_RATIO = 1 / BASE_HEAD_CHANCE;
        public static readonly double BASE_PARRY_CHANCE = 0.15;
        public static readonly double BASE_RESIST = 0.15;
        public static readonly double BASE_SCALAR = 1000;
        public static readonly double BASE_SKILL_SCALAR = 0.75;
    }
}
