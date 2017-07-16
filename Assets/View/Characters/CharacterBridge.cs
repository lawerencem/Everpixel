using Generics;
using Generics.Utilities;
using Model.Characters;
using System.Collections.Generic;

namespace View.Characters
{
    public class CharacterBridge : AbstractSingleton<CharacterBridge>
    {
        private const int TORSO_OFFSET = 3;

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

        private CharacterView GetRandomCritter(CharacterParams c)
        {
            var p = new CharacterView();
            p.Sprites = CharacterSpriteLoader.Instance.GetCritterSprites(c);
            return p;
        }

        private CharacterView GetRandomHumanoid(CharacterParams c)
        {
            var p = new CharacterView();
            var table = RaceParamsTable.Instance.Table;

            p.Face = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.Face);
            if (table[c.Race].Sprites.HeadDeco1.Count > 0)
                p.HeadDeco1 = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.HeadDeco1);
            else
                p.HeadDeco1 = -1;
            if (table[c.Race].Sprites.HeadDeco2.Count > 0)
                p.HeadDeco2 = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.HeadDeco2);
            else
                p.HeadDeco2 = -1;
            if (table[c.Race].Sprites.TorsoDeco1.Count > 0)
                p.TorsoDeco1 = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.TorsoDeco1);
            else
                p.TorsoDeco1 = -1;
            if (table[c.Race].Sprites.TorsoDeco2.Count > 0)
                p.TorsoDeco2 = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.TorsoDeco2);
            else
                p.TorsoDeco2 = -1;
            p.Torso = ListUtil<int>.GetRandomListElement(table[c.Race].Sprites.Torso);
            p.Head = p.Torso - TORSO_OFFSET;
            p.Sprites = CharacterSpriteLoader.Instance.GetCharacterSprites(c);

            return p;
        }
    }
}
