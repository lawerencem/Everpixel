using System;
using System.Collections.Generic;

namespace Assets.Template.XML
{
    public class XMLReader
    {
        protected List<string> _paths;

        public XMLReader()
        {
            this._paths = new List<string>();
        }

        public virtual void ReadFromFile()
        {
            throw new NotImplementedException();
        }

        public void AddPath(string s) { this._paths.Add(s); }
    }
}
