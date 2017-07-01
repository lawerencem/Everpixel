﻿using Assets.Generics;
using Controller.Managers;
using Model.Abilities;
using System.Collections.Generic;

namespace View.Events
{
    public class PopulateWpnBtnsEvent : GUIEvent
    {
        public List<Pair<GenericAbility, bool>> Abilities;

        public PopulateWpnBtnsEvent(List<Pair<GenericAbility, bool>> abs, GUIEventManager parent) : 
            base(GUIEventEnum.PopulateWpnBtns, parent)
        {
            this.Abilities = abs;
            this._parent.RegisterEvent(this);
        }
    }
}
