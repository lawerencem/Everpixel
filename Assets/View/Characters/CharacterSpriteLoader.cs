using Generics;
using Generics.Utilities;
using Model.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace View.Characters
{
    public class CharacterSpriteLoader : AbstractSingleton<CharacterSpriteLoader>
    {
        private const string CHARACTER_PATH = "Sprites/Characters/";
        private const string CRITTER_PATH = "Sprites/Critters/";
        private const int CRITTER_INDEX = 0;
        private const string EXTENSION = "_Spritesheet";

        public CharacterSpriteLoader() { }

        public Sprite[] GetCritterSprites(CharacterParams c)
        {
            var path = StringUtil.PathBuilder(CRITTER_PATH, c.Name, EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetCharacterSprites(CharacterParams c)
        {
            var path = StringUtil.PathBuilder(CHARACTER_PATH, c.Race.ToString(), EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetCharacterSprites(PredefinedCharacterParams c)
        {
            var path = StringUtil.PathBuilder(CHARACTER_PATH, c.Race.ToString(), EXTENSION);
            return GetSprites(path);
        }

        private Sprite[] GetCritterSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[1];
                sprites[0] = stuff[CRITTER_INDEX] as Sprite;
                return sprites;
            }
            else
                return null;
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
            else
                return null;
        }
    }
}
