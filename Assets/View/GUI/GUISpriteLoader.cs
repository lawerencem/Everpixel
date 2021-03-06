﻿using Assets.Model.Ability;
using Assets.Model.Ability.Enum;
using Assets.Template.Other;
using Assets.Template.Util;
using UnityEngine;

namespace Assets.View.GUI
{
    public class GUISpriteLoader : ASingleton<GUISpriteLoader>
    {
        private const string EXTENSION = "_Spritesheet";
        private const string ICON_PATH = "Sprites/Icons/";

        public GUISpriteLoader() { }

        public Sprite GetAbilityBtnImg(EAbility ability)
        {
            var path = StringUtil.PathBuilder(ICON_PATH, "Icon", EXTENSION);
            var stuff = Resources.LoadAll(path);
            int index = 1;
            if (AbilityTable.Instance.Table.ContainsKey(ability))
                index = AbilityTable.Instance.Table[ability].Data.IconSprite;
            return stuff[index + 1] as Sprite;
        }
    }
}
