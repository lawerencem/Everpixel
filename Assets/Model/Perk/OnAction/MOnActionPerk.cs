﻿using Assets.Model.Combat;

namespace Assets.Model.Perk.OnAction
{
    public class MOnActionPerk : MPerk
    {
        public MOnActionPerk(EPerk type) : base(type)
        {

        }

        public virtual void TryProcessAction(Hit hit)
        {

        }
    }
}