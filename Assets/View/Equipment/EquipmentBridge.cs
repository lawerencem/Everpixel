using Generics;
using Generics.Utilities;
using Model.Equipment;

namespace View.Equipment
{
    public class EquipmentBridge : AbstractSingleton<EquipmentBridge>
    {
        public EquipmentBridge() { }

        public ArmorView GetRandomArmorSprite(ArmorParams a)
        {
            var random = new ArmorView();
            var sprites = CharacterSpriteLoader.Instance.GetArmorSprites(a);
            random.Name = a.Name;
            random.Index = a.Sprites[RNG.Instance.Next(0, a.Sprites.Count)];
            random.Skill = a.Skill;
            random.Sprites = sprites;
            random.Type = a.Type;
            return random;
        }

        public WeaponView GetRandomWeaponSprite(WeaponParams w)
        {
            var random = new WeaponView();

            var sprites = CharacterSpriteLoader.Instance.GetWeaponSprites(w);
            random.Name = w.Name;
            random.Index = w.Sprites[RNG.Instance.Next(0, w.Sprites.Count)];
            random.Skill = w.Skill;
            random.Sprites = sprites;
            random.Use = w.Use;
            return random;
        }

        
    }
}
