using Assets.Model.Ability;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class CharAbilities
    {
        private List<MAbility> _activeAbilities;
        private List<MAbility> _wpnAbilities;

        public List<MAbility> GetNonWpnAbilities() { return this._activeAbilities; }
        public List<MAbility> GetWpnAbilities() { return this._wpnAbilities; }

        public CharAbilities()
        {
            this._activeAbilities = new List<MAbility>();
            this._wpnAbilities = new List<MAbility>();
        }

        public void AddAbility(MAbility a)
        {
            if (a.WpnAbility)
            {
                var exists = this._wpnAbilities.Find(x => x.Type == a.Type);
                if (exists == null)
                    this._wpnAbilities.Add(a);
            }
            else
            {
                var exists = this._activeAbilities.Find(x => x.Type == a.Type);
                if (exists == null)
                    this._activeAbilities.Add(a);
            }
        }
    }
}
