using Assets.Controller.Character;
using System.Collections.Generic;

namespace Assets.Model.Party
{
    public class MParty
    {
        private List<CharController> _chars;

        public MParty()
        {
            this._chars = new List<CharController>();
        }

        public List<CharController> GetChars() { return this._chars; }
    }
}
