using Generics;
using Generics.Utilities;
using Model.Equipment;
using Model.Mounts;
using UnityEngine;

namespace View
{
    // TODO: duplicate class
    public class CharacterSpriteLoader : AbstractSingleton<CharacterSpriteLoader>
    {
        private const string CRITTER_PATH = "Sprites/Critters/";
        private const string EQUIPMENT_PATH = "Sprites/Equipment/";
        private const string EXTENSION = "_Spritesheet";

        public CharacterSpriteLoader() { }

        public Sprite[] GetArmorSprites(ArmorParams a)
        {
            var path = StringUtil.PathBuilder(EQUIPMENT_PATH, a.Type.ToString(),  EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetMountSprites(MountParams m)
        {
            var path = StringUtil.PathBuilder(CRITTER_PATH, m.Type.ToString(), EXTENSION);
            return GetSprites(path);
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
