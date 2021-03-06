﻿using Assets.Model.Map;
using Assets.Model.Party;
using System.Collections.Generic;

namespace Assets.Controller.Map.Combat
{
    public class CMap
    {
        private List<MParty> _lParties;
        private MMap _map;
        private List<MParty> _rParties;

        public List<MParty> GetLParties()
        {
            return this._lParties;
        }

        public MMap GetMap()
        {
            return this._map;
        }

        public List<MParty> GetRParties()
        {
            return this._rParties;
        }

        public void SetMap(MMap m)
        {
            this._map = m;
        }

        public CMap()
        {
            this._lParties = new List<MParty>();
            this._rParties = new List<MParty>();
        }
    }
}
