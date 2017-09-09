using Assets.Data.Character.Table;
using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Param;
using Assets.Model.Mount;
using Assets.Template.Other;
using Assets.Template.Util;
using UnityEngine;

namespace Assets.View.Character
{
    public class CharSpriteLoader : ASingleton<CharSpriteLoader>
    {
        private const string CHARACTER_PATH = "Sprites/Characters/";
        private const string CRITTER_PATH = "Sprites/Critters/";
        private const int CRITTER_INDEX = 0;
        private const int DEAD_EYES = 8;
        private const string EFFECT_PATH = "Sprites/Effects/";
        private const int FLINCH_EYES = 7;
        private const string EQUIPMENT_PATH = "Sprites/Equipment/";
        private const string EXTENSION = "_Spritesheet";
        private const string LYCANTHROPE_PATH = "Sprites/Characters/Lycanthropes/";
        private const string SHIELD_PATH = "Sprites/Effects/";

        public CharSpriteLoader() { }

        public Sprite[] GetArmorSprites(ArmorParams a)
        {
            var path = StringUtil.PathBuilder(EQUIPMENT_PATH, a.Type.ToString(), EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetCritterSprites(CharParams c)
        {
            var path = StringUtil.PathBuilder(CRITTER_PATH, c.Name, EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetCritterSprites(string c)
        {
            var path = StringUtil.PathBuilder(CRITTER_PATH, c, EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetCharacterSprites(CharParams c)
        {
            var path = StringUtil.PathBuilder(CHARACTER_PATH, c.Race.ToString(), EXTENSION);
            return GetSprites(path);
        }

        public Sprite GetEffectSprite(string path)
        {
            var effectPath = StringUtil.PathBuilder(EFFECT_PATH, path);
            var sprites = GetSprites(effectPath);
            return sprites[0];
        }

        public Sprite GetHumanoidDeadEyes(ERace race)
        {
            var path = StringUtil.PathBuilder(CHARACTER_PATH, race.ToString(), EXTENSION);
            var sprites = GetSprites(path);
            var index = RaceParamsTable.Instance.Table[race].Sprites.Dead;
            return sprites[index[0]];
        }

        public Sprite GetHumanoidFlinchEyes(ERace race)
        {
            var path = StringUtil.PathBuilder(CHARACTER_PATH, race.ToString(), EXTENSION);
            var sprites = GetSprites(path);
            var index = RaceParamsTable.Instance.Table[race].Sprites.Flinch;
            return sprites[index[0]];
        }

        public Sprite[] GetMountSprites(MountParams m)
        {
            var path = StringUtil.PathBuilder(CRITTER_PATH, m.Type.ToString(), EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetLycanthropeSprites(string typeStr)
        {
            var path = StringUtil.PathBuilder(LYCANTHROPE_PATH, typeStr, EXTENSION);
            return GetSprites(path);
        }

        public Sprite GetShieldSprite()
        {
            var path = StringUtil.PathBuilder(SHIELD_PATH, "Shield");
            var sprites = GetSprites(path);
            return sprites[0];
        }

        public Sprite[] GetWeaponSprites(WeaponParams w)
        {
            var path = StringUtil.PathBuilder(EQUIPMENT_PATH, w.Type.ToString() + "_", w.Skill.ToString(), EXTENSION);
            return GetSprites(path);
        }

        private Sprite[] GetSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[stuff.Length - 1];
                for (int itr = 1; itr < stuff.Length; itr++)
                {
                    sprites[itr - 1] = stuff.GetValue(itr) as Sprite;
                }
                return sprites;
            }
            else if (stuff.Length == 1)
            {
                var sprites = new Sprite[1];
                sprites[0] = stuff[0] as Sprite;
                return sprites;
            }
            else
                return null;
        }
    }
}
