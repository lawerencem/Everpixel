using Assets.Model.Ability;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharAbilities<T>
    {
        private AChar<T> _parent;

        private List<MAbility> _activeAbilities;
        private List<MAbility> _defaultAbilities;

        public List<MAbility> GetNonWpnAbilities() { return this._activeAbilities; }
        public List<MAbility> GetWpnAbilities() { return this._defaultAbilities; }

        public CharAbilities(AChar<T> parent)
        {
            this._parent = parent;
            this._activeAbilities = new List<MAbility>();
            this._defaultAbilities = new List<MAbility>();
        }
    }
}
