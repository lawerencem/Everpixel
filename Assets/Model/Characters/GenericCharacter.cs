using Assets.Generics;
using Model.Spells;
using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Abilities;
using Model.Classes;
using Model.Events.Combat;
using Model.Injuries;
using Model.Shields;
using System.Collections.Generic;
using Model.Effects;
using Model.OverTimeEffects;
using Model.Equipment;

namespace Model.Characters
{
    public class GenericCharacter : AbstractCharacter<CharacterTypeEnum>
    {
        private RaceEnum _race;
        public RaceEnum Race { get { return this._race; } }

        public GenericCharacterController ParentController { get; set; }

        public GenericCharacter(RaceEnum race)
        {
            this._race = race;
            this.ActiveAbilities = new List<GenericAbility>();
            this.ActiveSpells = new SpellsByLevel();
            this.BaseClasses = new Dictionary<ClassEnum, GenericClass>();
            this.DefaultWpnAbilities = new List<GenericAbility>();
            this.DoTs = new List<GenericDoT>();
            this.Effects = new List<GenericEffect>();
            this.HoTs = new List<GenericHoT>();
            this.Injuries = new List<GenericInjury>();
            this.Mods = new ModContainer();
            this.Perks = new CharacterPerkCollection();
            this.Points = new CurrentPointsContainer();
            this.Shields = new List<Shield>();
            this.StatusFlags = new CharacterStatusFlags();
        }

        public void AddWeapon(GenericWeapon weapon, bool lWeapon)
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
                if (this.GetCurrentHP() > (int)this.GetCurrentStatValue(SecondaryStatsEnum.HP))
                    this.Points.CurrentHP = (int)this.GetCurrentStatValue(SecondaryStatsEnum.HP);
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
                if (this.GetCurrentStamina() > (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina))
                    this.Points.CurrentStamina = (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
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
