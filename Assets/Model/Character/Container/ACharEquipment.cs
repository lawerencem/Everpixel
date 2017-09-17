using Assets.Controller.Equipment.Weapon;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Armor;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class ACharEquipment<T>
    {
        private AChar<T> _parent;
        private CArmor _armor;
        private CHelm _helm;
        private CWeapon _lWeapon;
        private CWeapon _rWeapon;

        public CArmor GetArmor() { return this._armor; }
        public CHelm GetHelm() { return this._helm; }
        public CWeapon GetLWeapon() { return this._lWeapon; }
        public CWeapon GetRWeapon() { return this._rWeapon; }

        public ACharEquipment(AChar<T> parent)
        {
            this._parent = parent;
        }

        public void AddArmor(CArmor armor)
        {
            this.RemoveArmor();
            this._armor = armor;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(
                armor, armor.Model.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetMods().AddMod(mods);
        }

        public void AddHelm(CHelm helm)
        {
            this.RemoveHelm();
            this._helm = helm;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.Model.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetMods().AddMod(mods);
        }

        public void AddWeapon(CWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this._lWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.Model.GetStatModifiers());

                foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                    perk.TryModEquipmentMod(mods);
                //foreach (var perk in this._parent.GetPerks().GetEquipmentPerks())
                //    perk.TryProcessAdd(this._parent, weapon);
                // TODO

                this._parent.GetMods().AddMod(mods);
            }
            else
            {
                this._rWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.Model.GetStatModifiers());

                foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                    perk.TryModEquipmentMod(mods);
                foreach (var perk in this._parent.GetPerks().GetEquipmentPerks())
                    perk.TryProcessAdd(this._parent.Controller.Proxy, weapon);

                this._parent.GetMods().AddMod(mods);
            }
        }

        public void RemoveArmor()
        {
            this._parent.GetMods().RemoveGearMods(this._armor);
            this._armor = null;
        }

        public void RemoveHelm()
        {
            this._parent.GetMods().RemoveGearMods(this._helm);
            this._helm = null;
        }

        public void RemoveWeapon(bool lWeapon)
        {
            if (lWeapon)
            {
                this._parent.GetMods().RemoveGearMods(this._lWeapon);
                this._lWeapon = null;
            }
            else
            {
                this._parent.GetMods().RemoveGearMods(this._rWeapon);
                this._rWeapon = null;
            }
        }
    }
}
