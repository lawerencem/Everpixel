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

        public Sprite GetWpnAbilityBtnImg(AbilitiesEnum ability)
        {
            var path = StringUtil.PathBuilder(ICON_PATH, "Icon", EXTENSION);
            var stuff = Resources.LoadAll(path);
            int index = 0;
            if (GenericAbilityTable.Instance.Table.ContainsKey(ability))
                index = GenericAbilityTable.Instance.Table[ability].Sprite;
            return stuff[index] as Sprite;
        }
    }
}
