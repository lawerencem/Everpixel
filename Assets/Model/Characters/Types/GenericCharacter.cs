using Assets.Generics;
using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Abilities;
using Model.Classes;
using Model.Events.Combat;
using System.Collections.Generic;

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
            this.ActiveAbilities = new List<GenericActiveAbility>();
            this.BaseClasses = new Dictionary<ClassEnum, GenericClass>();
            this.DefaultWpnAbilities = new List<WeaponAbility>();
            this.IndefSStatMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
            this.Injuries = new List<Model.Injuries.GenericInjury>();
            this.Perks = new CharacterPerkCollection();
            this.PStatMods = new List<PrimaryStatModifier>();
            this.StatusFlags = new CharacterStatusFlags();
            this.SStatMods = new List<SecondaryStatModifier>();
        }

        public void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.CurrentHP += value;
                if (this.CurrentHP > this.GetCurrentStatValue(SecondaryStatsEnum.HP))
                    this.CurrentHP = this.GetCurrentStatValue(SecondaryStatsEnum.HP);
            }
            else
            {
                this.CurrentHP -= value;
                if (this.CurrentHP <= 0)
                {
                    var killed = new CharacterKilledEvent(CombatEventManager.Instance, this.ParentController);
                }
            }
        }
    }
}
