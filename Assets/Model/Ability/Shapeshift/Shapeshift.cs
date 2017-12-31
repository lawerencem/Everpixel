using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability.Shapeshift
{
    public class Shapeshift : MAbility
    {
        private static readonly int HUMANOID_ATTACK_HEAD = 3;
        private static readonly int HUMANOID_ATTACK_TORSO = 4;
        private static readonly int HUMANOID_HEAD = 0;
        private static readonly int HUMANOID_HEAD_DEAD = 2;
        private static readonly int HUMANOID_HEAD_FLINCH = 1;
        private static readonly int HUMANOID_TORSO = 4;

        public ShapeshiftInfo Info { get; set; }

        public Shapeshift(EAbility type) : base(type)
        {
            this.Info = new ShapeshiftInfo();
        }

        protected void SetStandardHumanoidInfo()
        {
            this.Info.CharAttackHead = Shapeshift.HUMANOID_ATTACK_HEAD;
            this.Info.CharAttackTorso = Shapeshift.HUMANOID_ATTACK_TORSO;
            this.Info.CharHead = Shapeshift.HUMANOID_HEAD;
            this.Info.CharHeadDead = Shapeshift.HUMANOID_HEAD_DEAD;
            this.Info.CharHeadFlinch = Shapeshift.HUMANOID_HEAD_FLINCH;
            this.Info.CharTorso = Shapeshift.HUMANOID_TORSO;
        }
    }
}
