using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Biomes;
using System.Collections.Generic;
using UnityEngine;

namespace View.GUI
{
    public class GUISpriteLoader : AbstractSingleton<GUISpriteLoader>
    {
        private const string EXTENSION = "_Spritesheet";
        private const string ICON_PATH = "Sprites/Icons/";


        public GUISpriteLoader() { }

        public Sprite GetWpnAbilityBtnImg(WeaponAbilitiesEnum ability)
        {
            var path = StringUtil.PathBuilder(ICON_PATH, "Icon", EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = this.GetWpnAbilityIndex(ability);
            return stuff[index] as Sprite;
        }

        private int GetWpnAbilityIndex(WeaponAbilitiesEnum ability)
        {
            switch (ability)
            {
                case (WeaponAbilitiesEnum.Aim): { return 1; }
                case (WeaponAbilitiesEnum.Break_Armor): { return 2; }
                case (WeaponAbilitiesEnum.Break_Shield): { return 3; }
                case (WeaponAbilitiesEnum.Chop): { return 4; }
                case (WeaponAbilitiesEnum.Crush): { return 5; }
                case (WeaponAbilitiesEnum.Double_Strike): { return 6; }
                case (WeaponAbilitiesEnum.Fire): { return 7; }
                case (WeaponAbilitiesEnum.Gash): { return 8; }
                case (WeaponAbilitiesEnum.Great_Strike): { return 9; }
                case (WeaponAbilitiesEnum.Maim): { return 10; }
                case (WeaponAbilitiesEnum.Pierce): { return 11; }
                case (WeaponAbilitiesEnum.Pull): { return 12; }
                case (WeaponAbilitiesEnum.Riposte): { return 13; }
                case (WeaponAbilitiesEnum.Scatter): { return 14; }
                case (WeaponAbilitiesEnum.Shield_Wall): { return 15; }
                case (WeaponAbilitiesEnum.Shove): { return 16; }
                case (WeaponAbilitiesEnum.Slash): { return 17; }
                case (WeaponAbilitiesEnum.Spear_Wall): { return 18; }
                case (WeaponAbilitiesEnum.Stab): { return 19; }
                case (WeaponAbilitiesEnum.Stun): { return 20; }
                case (WeaponAbilitiesEnum.Triple_Strike): { return 21; }
                case (WeaponAbilitiesEnum.Wide_Slash): { return 22; }
                case (WeaponAbilitiesEnum.Wrap): { return 23; }
                default: { return 1; }
            }
        }
    }
}
