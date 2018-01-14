using Assets.Model.Biome;
using Assets.Model.Zone;
using Assets.Template.Other;
using Assets.Template.Util;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.View.Map
{
    // TODO: Clean up
    public class MapSpriteLoader : ASingleton<MapSpriteLoader>
    {
        private const int POTENTIAL_ATTACK_SPRITE_INDEX = 1;
        private const int HOSTILE_HOVER_SPRITE_INDEX = 2;
        private const int TILE_HIGHLIGHT_SPRITE_INDEX = 3;

        private readonly List<int> LEVEL_ONE_BLOOD_SPATTER = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        private readonly List<int> LEVEL_TWO_BLOOD_SPATTER = new List<int>() { 9, 10, 11, 12, 13, 14, 15, 16};
        private readonly List<int> LEVEL_THREE_BLOOD_SPATTER = new List<int>() { 17, 18, 19, 20, 21, 22, 23, 24 };
        private readonly List<int> LEVEL_FOUR_BLOOD_SPATTER = new List<int>() { 25, 26, 27, 28, 29, 30, 31, 32};
        private readonly List<int> LEVEL_FIVE_BLOOD_SPATTER = new List<int>() { 33, 34, 35};

        private readonly List<int> TILE_BOTTOMS = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

        private const string PATH = "Sprites/CombatMap/";
        private const string DECO_EXTENSION = "_Deco";
        private const string TILE_EXTENSION = "_Tiles";
        private const string ZONE_EXTENSION = "_Zone";

        public MapSpriteLoader() { }

        public Sprite[] GetBiomeBackground(EBiome b)
        {
            var path = StringUtil.PathBuilder(PATH, b.ToString(), TILE_EXTENSION);
            return GetSprites(path);
        }

        public Sprite[] GetDecoSprites()
        {
            var path = StringUtil.PathBuilder(PATH, "Deco");
            return this.GetSprites(path);
        }

        public Sprite GetHeightBottom()
        {
            var path = StringUtil.PathBuilder(PATH, "Height_Bottom");
            var stuff = Resources.LoadAll(path);
            return stuff[ListUtil<int>.GetRandomElement(this.TILE_BOTTOMS)] as Sprite;
        }

        public Sprite GetHeightLeft()
        {
            var path = StringUtil.PathBuilder(PATH, "Height_Left");
            var stuff = Resources.LoadAll(path);
            return stuff[ListUtil<int>.GetRandomElement(this.TILE_BOTTOMS)] as Sprite;
        }

        public Sprite GetHeightRight()
        {
            var path = StringUtil.PathBuilder(PATH, "Height_Right");
            var stuff = Resources.LoadAll(path);
            return stuff[ListUtil<int>.GetRandomElement(this.TILE_BOTTOMS)] as Sprite;
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
            var index = ListUtil<int>.GetRandomElement(this.LEVEL_ONE_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelTwo()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomElement(this.LEVEL_TWO_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelThree()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomElement(this.LEVEL_THREE_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelFour()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomElement(this.LEVEL_FOUR_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetBloodSpatterLevelFive()
        {
            var path = StringUtil.PathBuilder(PATH, "Tileblood", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomElement(this.LEVEL_FIVE_BLOOD_SPATTER);
            return stuff[index] as Sprite;
        }

        public Sprite GetSlimeSplatter(int level)
        {
            var path = StringUtil.PathBuilder(PATH, "Slime_Zone");
            var stuff = Resources.LoadAll(path);
            int index = 1;
            switch(level)
            {
                case (1): { index = ListUtil<int>.GetRandomElement(this.LEVEL_ONE_BLOOD_SPATTER); } break;
                case (2): { index = ListUtil<int>.GetRandomElement(this.LEVEL_TWO_BLOOD_SPATTER); } break;
                case (3): { index = ListUtil<int>.GetRandomElement(this.LEVEL_THREE_BLOOD_SPATTER); } break;
                case (4): { index = ListUtil<int>.GetRandomElement(this.LEVEL_FOUR_BLOOD_SPATTER); } break;
                case (5): { index = ListUtil<int>.GetRandomElement(this.LEVEL_FIVE_BLOOD_SPATTER); } break;
            }
            return stuff[index] as Sprite;
        }

        public Sprite GetTileHighlightSprite()
        {
            var path = StringUtil.PathBuilder(PATH, "Base", TILE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            return stuff[TILE_HIGHLIGHT_SPRITE_INDEX] as Sprite;
        }

        public Sprite GetZoneSprite(ZoneData data)
        {
            var path = StringUtil.PathBuilder(PATH, data.SpritesPath, ZONE_EXTENSION);
            var stuff = Resources.LoadAll(path);
            var index = ListUtil<int>.GetRandomElement(data.SpriteIndexes);
            return stuff[index] as Sprite;
        }

        private Sprite[] GetSprites(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length > 1)
            {
                var sprites = new Sprite[stuff.Length - 1];
                for (int i = 1; i < stuff.Length; i++)
                    sprites[i - 1] = stuff.GetValue(i) as Sprite;
                return sprites;
            }
            else
                return null;
        }

    }
}
