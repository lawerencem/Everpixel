using Assets.Model.Equipment.Param;
using Assets.Template.Other;
using Assets.Template.Util;
using Assets.View.Character;

namespace Assets.View.Equipment
{
    public class EquipmentBridge : ASingleton<EquipmentBridge>
    {
        public EquipmentBridge() { }

        public VArmor GetRandomArmorSprite(ArmorParams a)
        {
            var random = new VArmor();
            var sprites = CharSpriteLoader.Instance.GetArmorSprites(a);
            random.Name = a.Name;
            random.Index = a.Sprites[RNG.Instance.Next(0, a.Sprites.Count)];
            random.Sprites = sprites;
            random.Type = a.Type;
            return random;
        }

        public VWeapon GetRandomWeaponSprite(WeaponParams w)
        {
            var random = new VWeapon();

            var sprites = CharSpriteLoader.Instance.GetWeaponSprites(w);
            random.Name = w.Name;
            random.Index = w.Sprites[RNG.Instance.Next(0, w.Sprites.Count)];
            random.Skill = w.Skill;
            random.Sprites = sprites;
            random.Use = w.Use;
            return random;
        }

        
    }
}
