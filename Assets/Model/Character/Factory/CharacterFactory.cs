using Assets.Model.Character.Builder;
using Assets.Model.Characters.Params;

namespace Assets.Model.Character.Factory
{
    public class CharacterFactory
    {
        private CharBuilder _charBuilder;

        public CharacterFactory()
        {
            this._charBuilder = new CharBuilder();
        }

        public MChar CreateNewCharacter(PreCharParams arg)
        {
            return this._charBuilder.Build(arg);
        }
    }
}