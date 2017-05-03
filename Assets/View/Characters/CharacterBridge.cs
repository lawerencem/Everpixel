using Generics;
using Generics.Utilities;
using Model.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace View.Characters
{
    public class CharacterBridge : AbstractSingleton<CharacterBridge>
    {
        public readonly List<int> HumanoidFaceIndexes = new List<int>() { 8 };
        public readonly List<int> HumanoidDeco1Indexes = new List<int>() { 16, 17, 18, 19 };
        public readonly List<int> HumanoidDeco2Indexes = new List<int>() { 24, 25, 26, 27 };
        public readonly List<int> HumanoidDeco3Indexes = new List<int>() { 32, 33, 34, 35 };
        public readonly List<int> HumanoidTorsoIndexes = new List<int>() { 0, 1, 2 };

        public CharacterBridge() { }

        public CharacterView GetRandomizedCharacterSprites(CharacterParams c)
        {
            var random = new CharacterView();
            switch(c.Type)
            {
                case (CharacterTypeEnum.Critter): { random = GetRandomCritter(c); } break;
                case (CharacterTypeEnum.Humanoid): { random = GetRandomHumanoid(c); } break;
            }
            return random;
        }

        public CharacterView GetRandomizedCharacterSprites(PredefinedCharacterParams c)
        {
            var random = new CharacterView();
            switch (c.Type)
            {
                case (CharacterTypeEnum.Humanoid): { random = GetRandomHumanoid(c); } break;
            }
            return random;
        }

        private CharacterView GetRandomCritter(CharacterParams c)
        {
            var p = new CharacterView();
            p.Sprites = CharacterSpriteLoader.Instance.GetCritterSprites(c);
            return p;
        }

        private CharacterView GetRandomHumanoid(CharacterParams c)
        {
            var p = new CharacterView();

            p.Face = HumanoidFaceIndexes[RNG.Instance.Next(0, HumanoidFaceIndexes.Count)];
            p.Deco1 = HumanoidDeco1Indexes[RNG.Instance.Next(0, HumanoidDeco1Indexes.Count)];
            p.Deco2 = HumanoidDeco2Indexes[RNG.Instance.Next(0, HumanoidDeco2Indexes.Count)];
            p.Deco3 = HumanoidDeco3Indexes[RNG.Instance.Next(0, HumanoidDeco3Indexes.Count)];
            p.Torso = HumanoidTorsoIndexes[RNG.Instance.Next(0, HumanoidTorsoIndexes.Count)];
            p.Sprites = CharacterSpriteLoader.Instance.GetCharacterSprites(c);

            return p;
        }

        private CharacterView GetRandomHumanoid(PredefinedCharacterParams c)
        {
            var p = new CharacterView();

            p.Face = HumanoidFaceIndexes[RNG.Instance.Next(0, HumanoidFaceIndexes.Count)];
            p.Deco1 = HumanoidDeco1Indexes[RNG.Instance.Next(0, HumanoidDeco1Indexes.Count)];
            p.Deco2 = HumanoidDeco2Indexes[RNG.Instance.Next(0, HumanoidDeco2Indexes.Count)];
            p.Deco3 = HumanoidDeco3Indexes[RNG.Instance.Next(0, HumanoidDeco3Indexes.Count)];
            p.Torso = HumanoidTorsoIndexes[RNG.Instance.Next(0, HumanoidTorsoIndexes.Count)];
            p.Sprites = CharacterSpriteLoader.Instance.GetCharacterSprites(c);

            return p;
        }
    }
}
