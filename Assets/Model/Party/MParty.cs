using Assets.Controller.Character;
using System.Collections.Generic;

namespace Assets.Model.Party
{
    public class MParty
    {
        private List<CharController> _chars;

        public List<CharController> GetChars() { return this._chars; }

        public MParty()
        {
            this._chars = new List<CharController>();
        }

        public void AddChar(CharController c)
        {
            this._chars.Add(c);
            c.Proxy.SetParentParty(this);
        }

        public void RemoveChar(CharController c)
        {
            this._chars.Remove(c);
            c.Proxy.SetParentParty(null);
        }
    }
}
