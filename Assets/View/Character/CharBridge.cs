using Assets.Model.Character.Enum;
using Assets.Model.Character.Param;
using Assets.Model.Character.Table;
using Template.Other;
using Template.Utility;

namespace Assets.View.Character
{
    public class CharBridge : ASingleton<CharBridge>
    {
        private const int TORSO_OFFSET = 3;

        public CharBridge() { }

        public VChar GetRandomizedCharacterSprites(CharParams c)
        {
            var random = new VChar();
            switch(c.Type)
            {
                case (ECharType.Critter): { random = GetRandomCritter(c); } break;
                case (ECharType.Humanoid): { random = GetRandomHumanoid(c); } break;
            }
            return random;
        }

        private VChar GetRandomCritter(CharParams c)
        {
            var p = new VChar();
            p.Sprites = CharSpriteLoader.Instance.GetCritterSprites(c);
            return p;
        }

        private VChar GetRandomHumanoid(CharParams c)
        {
            var p = new VChar();
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
            p.Sprites = CharSpriteLoader.Instance.GetCharacterSprites(c);

            return p;
        }
    }
}
