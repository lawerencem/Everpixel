using Assets.Model.Biome.Enum;
using Generics;
using Generics.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Map
{
    // TODO: Clean up
    public class MapSpriteLoader : AbstractSingleton<MapSpriteLoader>
    {
        private const int POTENTIAL_ATTACK_SPRITE_INDEX = 1;
        private const int HOSTILE_HOVER_SPRITE_INDEX = 2;
        private const int TILE_HIGHLIGHT_SPRITE_INDEX = 3;

        private readonly List<int> LEVEL_ONE_BLOOD_SPATTER = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
        private readonly List<int> LEVEL_TWO_BLOOD_SPATTER = new List<int>() { 8, 9, 10};
        private readonly List<int> LEVEL_FOUR_BLOOD_SPATTER = new List<int>() { 11, 12 };
        private readonly List<int> LEVEL_FIVE_BLOOD_SPATTER = new List<int>() { 13, 14 };

        private const string PATH = "Sprites/CombatMap/";
        private const string DECO_EXTENSION = "_Deco";
        private const string TILE_EXTENSION = "_Tiles";

        public MapSpriteLoader() { }

        public Sprite[] GetBiomeBackground(EBiome b)
        {
            var path = StringUtil.PathBuilder(PATH, b.ToString(), TILE_EXTENSION);
            return GetBackgroundSprites(path);
        }

        public Sprite[] GetBiomeBackgroundDeco(EBiome b)
        {
            var path = StringUtil.PathBuilder(PATH, b.ToString(), DECO_EXTENSION);
            return GetBackgroundSprites(path);
        }

        public Sprite GetHostileHoverSprite()
        {
            var path = StringUtil.PathBuilder(PATH, "Base", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            return stuff[HOSTILE_HOVER_SPRITE_INDEX] as Sprite;
        }

        public Sprite GetPotentialAttackLocSprite()
        {
            var path = StringUtil.PathBuilder(PATH, "Base", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            return stuff[POTENTIAL_ATTACK_SPRITE_INDEX] as Sprite;
        }

        public Sprite GetBloodSpatterLevelOne()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomListElement(this.LEVEL_ONE_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelTwo()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomListElement(this.LEVEL_TWO_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelFour()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomListElement(this.LEVEL_FOUR_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelFive()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomListElement(this.LEVEL_FIVE_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetTileHighlightSprite()
        {
            var path = StringUtil.PathBuilder(PATH, "Base", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            return stuff[TILE_HIGHLIGHT_SPRITE_INDEX] as Sprite;
        }

        private Sprite[] GetBackgroundDecoSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[8];
                //var sprites = new Sprite[stuff.Length - 1];
                //for (int itr = 1; itr < stuff.Length; itr++)  // TODO
                for (int itr = 1; itr < 9; itr++)
                {
                    sprites[itr - 1] = stuff.GetValue(itr) as Sprite;
                }
                return sprites;
            }
            else
                return null;
        }

        private Sprite[] GetBackgroundSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[8];
                //var sprites = new Sprite[stuff.Length - 1];
                //for (int itr = 1; itr < stuff.Length; itr++)  // TODO
                for (int itr = 1; itr < 9; itr++)
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
