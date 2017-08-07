﻿using Assets.Controller.Character;
using Controller.Map;

namespace Assets.Model.Ability
{
    public class AbilityArgContainer
    {
        public int Range { get; set; }
        public bool RWeapon { get; set; }
        public CharController Source { get; set; }
        public TileController Target { get; set; }
    }
}