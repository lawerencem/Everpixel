using System;
using System.Collections.Generic;
using Generics;
using Model.Characters;
using View.Characters;
using View.Equipment;
using Model.Equipment;
using View.Mounts;

namespace View.Builders
{
    public class CharacterViewBuilder
    {
        public CharacterView Build(CharacterParams arg)
        {
            var view = CharacterBridge.Instance.GetRandomizedCharacterSprites(arg);
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
