using Assets.Model.Ability.Enum;

namespace Assets.Model.Ability.Music
{
    public class MSong : MAbility
    {
        protected ESong _songType;
        public ESong SongType { get { return this._songType; } }

        public MSong(EAbility type) : base(type) { }
    }
}
