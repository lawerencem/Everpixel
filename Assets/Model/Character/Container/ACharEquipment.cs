using Assets.Model.Character.Param;
using Assets.Model.Equipment.Type;
using System.Collections.Generic;
using Template.Other;

namespace Assets.Model.Character.Container
{
    public class ACharEquipment<T>
    {
        private AChar<T> _parent;
        private MArmor _armor;
        private MHelm _helm;
        private MWeapon _lWeapon;
        private MWeapon _rWeapon;

        public MArmor GetArmor() { return this._armor; }
        public MHelm GetHelm() { return this._helm; }
        public MWeapon GetLWeapon() { return this._lWeapon; }
        public MWeapon GetRWeapon() { return this._rWeapon; }

        public ACharEquipment(AChar<T> parent)
        {
            this._parent = parent;
        }

        public void AddArmor(MArmor armor)
        {
            this.RemoveArmor();
            this._armor = armor;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(armor, armor.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetMods().AddMod(mods);
        }

        public void AddHelm(MHelm helm)
        {
            this.RemoveHelm();
            this._helm = helm;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetMods().AddMod(mods);
        }

        public void AddWeapon(MWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this._lWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());

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
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());

                foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                    perk.TryModEquipmentMod(mods);
                //foreach (var perk in this._parent.GetPerks().GetEquipmentPerks())
                //    perk.TryProcessAdd(this._parent, weapon);
                // TODO

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
