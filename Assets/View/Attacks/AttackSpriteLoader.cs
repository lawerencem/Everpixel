using Generics;
using Generics.Utilities;
using Model.Abilities;
using Model.Characters;
using Model.Equipment;
using Model.Mounts;
using UnityEngine;

namespace View.Characters
{
    public class AttackSpriteLoader : AbstractSingleton<AttackSpriteLoader>
    {
        private const string ATTACK_PATH = "Sprites/Attacks/";
        private const string EXTENSION = "_Spritesheet";

        public AttackSpriteLoader() { }

        public Sprite GetAttackSprite(Ability a)
        {
            var path = StringUtil.PathBuilder(ATTACK_PATH, a.Type.ToString());
            return GetSprite(path);
        }

        private Sprite GetSprite(string path)
        {
            var stuff = Resources.LoadAll(path);
            if (stuff.Length == 2)
                return stuff[1] as Sprite;
            else
                return null;
        }
    }
}
