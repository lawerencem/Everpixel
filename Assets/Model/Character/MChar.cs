using Assets.Generics;
using Model.Spells;
using Characters.Params;
using Model.Classes;
using Model.Events.Combat;
using Model.Injuries;
using Model.Shields;
using System.Collections.Generic;
using Model.Effects;
using Model.OverTimeEffects;
using Model.Equipment;
using Assets.Model.Character.Enum;
using Model.Characters;
using Assets.Controller.Character;
using Assets.Model.Ability;
using Assets.Model.Characters;
using Assets.Model.Character.Param;
using Assets.Model.Equipment.Type;

namespace Assets.Model.Character
{
    public class MChar : AChar<ECharacterType>
    {
        private ERace _race;
        public ERace Race { get { return this._race; } }

        public CharController ParentController { get; set; }

        public MChar(ERace race)
        {
            this._race = race;
            this.ActiveAbilities = new List<MAbility>();
            this.ActiveSpells = new SpellsByLevel();
            this.BaseClasses = new Dictionary<EClass, MClass>();
            this.DefaultWpnAbilities = new List<MAbility>();
            this.DoTs = new List<MDoT>();
            this.Effects = new List<MEffect>();
            this.HoTs = new List<GenericHoT>();
            this.Injuries = new List<MInjury>();
            this.Mods = new ModContainer();
            this.Perks = new CharPerkCollection();
            this.Points = new CurrentPointsContainer();
            this.Shields = new List<Shield>();
            this.StatusFlags = new FCharacterStatus();
        }

        public void AddWeapon(MWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this.LWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());

                foreach (var perk in this.Perks.EquipmentSStatPerks)
                    perk.TryModEquipmentMod(mods);
                foreach (var perk in this.Perks.EquipmentPerks)
                    perk.TryProcessAdd(this, weapon);

                this.Mods.AddMod(mods);
            }
            else
            {
                this.RWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());

                foreach (var perk in this.Perks.EquipmentSStatPerks)
                    perk.TryModEquipmentMod(mods);
                foreach (var perk in this.Perks.EquipmentPerks)
                    perk.TryProcessAdd(this, weapon);

                this.Mods.AddMod(mods);
            }
        }

        public void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.Points.CurrentHP += value;
                if (this.GetCurrentHP() > (int)this.GetCurrentStatValue(ESecondaryStat.HP))
                    this.Points.CurrentHP = (int)this.GetCurrentStatValue(ESecondaryStat.HP);
            }
            else
            {
                int dmg = value;
                foreach (var shield in this.Shields)
                    shield.ProcessShieldDmg(ref dmg);
                this.Shields.RemoveAll(x => x.CurHP <= 0);

                if (dmg >= 0)
                    this.Points.CurrentHP -= dmg;

                if (this.GetCurrentHP() <= 0)
                {
                    var killed = new CharacterKilledEvent(CombatEventManager.Instance, this.ParentController);
                }
            }
        }

        public void ModifyStamina(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.Points.CurrentStamina += value;
                if (this.GetCurrentStamina() > (int)this.GetCurrentStatValue(ESecondaryStat.Stamina))
                    this.Points.CurrentStamina = (int)this.GetCurrentStatValue(ESecondaryStat.Stamina);
            }
            else
            {
                this.Points.CurrentStamina -= value;
                if (this.Points.CurrentStamina < 0)
                    this.Points.CurrentStamina = 0;
            }
        }
    }
}
