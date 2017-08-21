using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Template.Other;
using Template.Utility;
using UnityEngine;

namespace Assets.View.GUI
{
    public class GUISpriteLoader : ASingleton<GUISpriteLoader>
    {
        private const string EXTENSION = "_Spritesheet";
        private const string ICON_PATH = "Sprites/Icons/";

        public GUISpriteLoader() { }

        public Sprite GetWpnAbilityBtnImg(EAbility ability)
        {
            var path = StringUtil.PathBuilder(ICON_PATH, "Icon", EXTENSION);
            var stuff = Resources.LoadAll(path);
            int index = 0;
            if (AbilityTable.Instance.Table.ContainsKey(ability))
                index = AbilityTable.Instance.Table[ability].Params.Sprite;
            return stuff[index] as Sprite;
        }
    }
}
