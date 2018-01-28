using Assets.Controller.Equipment.Weapon;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Armor;
using Assets.Template.Other;
using System.Collections.Generic;

namespace Assets.Model.Character.Container
{
    public class ACharEquipment
    {
        private AChar _parent;
        private CArmor _armor;
        private CHelm _helm;
        private CWeapon _lWeapon;
        private CWeapon _rWeapon;

        public CArmor GetArmor() { return this._armor; }
        public CHelm GetHelm() { return this._helm; }
        public CWeapon GetLWeapon() { return this._lWeapon; }
        public CWeapon GetRWeapon() { return this._rWeapon; }

        public ACharEquipment(AChar parent)
        {
            this._parent = parent;
        }

        public void AddArmor(CArmor armor)
        {
            this.RemoveArmor();
            this._armor = armor;
            var mods = new Pair<object, List<StatMod>>(
                armor, armor.Model.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetStatMods().AddMod(mods);
        }

        public void AddHelm(CHelm helm)
        {
            this.RemoveHelm();
            this._helm = helm;
            var mods = new Pair<object, List<StatMod>>(helm, helm.Model.GetStatModifiers());
            foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                perk.TryModEquipmentMod(mods);
            this._parent.GetStatMods().AddMod(mods);
        }

        public void AddWeapon(CWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this._lWeapon = weapon;
                var mods = new Pair<object, List<StatMod>>(weapon, weapon.Model.GetStatModifiers());

                foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                    perk.TryModEquipmentMod(mods);
                //foreach (var perk in this._parent.GetPerks().GetEquipmentPerks())
                //    perk.TryProcessAdd(this._parent, weapon);
                // TODO

                this._parent.GetStatMods().AddMod(mods);
            }
            else
            {
                this._rWeapon = weapon;
                var mods = new Pair<object, List<StatMod>>(weapon, weapon.Model.GetStatModifiers());

                foreach (var perk in this._parent.GetPerks().GetEquipmentSStatPerks())
                    perk.TryModEquipmentMod(mods);
                //foreach (var perk in this._parent.GetPerks().GetEquipmentPerks())
                //    perk.TryProcessAdd(this._parent.Controller.Proxy, weapon);
                // TODO

                this._parent.GetStatMods().AddMod(mods);
            }
        }

        public void RemoveArmor()
        {
            this._parent.GetStatMods().RemoveGearMods(this._armor);
            this._armor = null;
        }

        public void RemoveHelm()
        {
            this._parent.GetStatMods().RemoveGearMods(this._helm);
            this._helm = null;
        }

        public void RemoveWeapon(bool lWeapon)
        {
            if (lWeapon)
            {
                this._parent.GetStatMods().RemoveGearMods(this._lWeapon);
                this._lWeapon = null;
            }
            else
            {
                this._parent.GetStatMods().RemoveGearMods(this._rWeapon);
                this._rWeapon = null;
            }
        }
    }
}
