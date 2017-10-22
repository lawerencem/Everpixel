using Assets.Model.Ability;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharAbilities<T>
    {
        private List<MAbility> _activeAbilities;
        private List<MAbility> _defaultAbilities;

        public List<MAbility> GetNonWpnAbilities() { return this._activeAbilities; }
        public List<MAbility> GetWpnAbilities() { return this._defaultAbilities; }

        public CharAbilities()
        {
            this._activeAbilities = new List<MAbility>();
            this._defaultAbilities = new List<MAbility>();
        }
    }
}
