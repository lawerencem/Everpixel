using Assets.Model.Character.Param;
using Assets.View.Character;
using Assets.View.Equipment;
using Assets.View.Mount;

namespace Assets.View.Builder
{
    public class CharacterViewBuilder
    {
        public VChar Build(CharParams arg)
        {
            var view = CharBridge.Instance.GetRandomizedCharacterSprites(arg);
            view.Name = arg.Name;
            view.Type = arg.Type;
            view.Race = arg.Race;
            if (arg.Armor != null) { view.Armor = EquipmentBridge.Instance.GetRandomArmorSprite(arg.Armor); }
            if (arg.Helm != null) { view.Helm = EquipmentBridge.Instance.GetRandomArmorSprite(arg.Helm); }
            if (arg.LWeapon != null) { view.LWeapon = EquipmentBridge.Instance.GetRandomWeaponSprite(arg.LWeapon); }
            if (arg.Mount != null) { view.Mount = MountBridge.Instance.GetMountSprites(arg.Mount); }
            if (arg.RWeapon != null) { view.RWeapon = EquipmentBridge.Instance.GetRandomWeaponSprite(arg.RWeapon); }

            return view;
        }
    }
}
