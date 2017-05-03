using System;
using Generics;

namespace Generics
{
    public class GenericXMLReader
    {
        protected string _path;

        public virtual void ReadFromFile()
        {
            throw new NotImplementedException();
        }

        public void SetPath(string s) { this._path = s; }
    }
}
