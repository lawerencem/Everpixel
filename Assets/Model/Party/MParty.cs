using Assets.Controller.Character;
using System.Collections.Generic;

namespace Assets.Model.Party
{
    public class MParty
    {
        private List<CChar> _chars;

        public List<CChar> GetChars()
        {
            return this._chars;
        }

        public MParty()
        {
            this._chars = new List<CChar>();
        }

        public void AddChar(CChar c)
        {
            this._chars.Add(c);
            c.Proxy.SetParentParty(this);
        }

        public void RemoveChar(CChar c)
        {
            this._chars.Remove(c);
            c.Proxy.SetParentParty(null);
        }
    }
}
