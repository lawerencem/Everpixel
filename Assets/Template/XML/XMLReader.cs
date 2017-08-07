using System;

namespace Template.XML
{
    public class XMLReader
    {
        protected string _path;

        public virtual void ReadFromFile()
        {
            throw new NotImplementedException();
        }

        public void SetPath(string s) { this._path = s; }
    }
}
