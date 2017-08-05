using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability
{
    public class Summon : Ability
    {
        public string toSummon = "";

        public Summon(EnumAbility type) : base(type)
        {

        }
    }
}
