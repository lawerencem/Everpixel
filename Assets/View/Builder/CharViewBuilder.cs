using Assets.Model.Character;
using Assets.Template.Other;
using Assets.View.Character;
using Assets.View.Equipment;
using Assets.View.Mount;

namespace Assets.View.Builder
{
    public class CharViewBuilder : ASingleton<CharViewBuilder>
    {
        public VChar Build(PChar proxy)
        {
            var view = CharBridge.Instance.GetRandomizedCharacterSprites(proxy.GetParams());
            view.Name = proxy.GetParams().Name;
            view.Type = proxy.GetParams().Type;
            view.Race = proxy.GetParams().Race;
            if (proxy.GetArmor() != null)
                view.Armor = EquipmentBridge.Instance.GetRandomArmorSprite(proxy.GetArmor().Params);
            if (proxy.GetHelm() != null)
                view.Helm = EquipmentBridge.Instance.GetRandomArmorSprite(proxy.GetHelm().Params);
            if (proxy.GetLWeapon() != null)
                view.LWeapon = EquipmentBridge.Instance.GetRandomWeaponSprite(proxy.GetLWeapon().Params);
            if (proxy.GetMount() != null)
                view.Mount = MountBridge.Instance.GetMountSprites(proxy.GetMount().Params);
            if (proxy.GetRWeapon() != null)
                view.RWeapon = EquipmentBridge.Instance.GetRandomWeaponSprite(proxy.GetRWeapon().Params);

            return view;
        }
    }
}
