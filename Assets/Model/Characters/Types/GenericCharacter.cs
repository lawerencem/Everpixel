using Assets.Generics;
using Characters.Params;
using Controller.Characters;
using Controller.Managers;
using Model.Classes;
using Model.Events.Combat;
using System.Collections.Generic;

namespace Model.Characters
{
    public class GenericCharacter : AbstractCharacter<CharacterTypeEnum>
    {
        public GenericCharacterController ParentController { get; set; }

        public GenericCharacter()
        {
            this.BaseClasses = new Dictionary<ClassEnum, GenericClass>();
            this.PStatMods = new List<PrimaryStatModifier>();
            this.SStatMods = new List<SecondaryStatModifier>();
            this.IndefSStatMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
            this.Injuries = new List<Model.Injuries.GenericInjury>();
        }

        public void AddStamina(double toAdd)
        {
            this.CurrentStamina += (int)toAdd;
            if (this.CurrentStamina > this.GetCurrentStatValue(SecondaryStatsEnum.Stamina))
                this.CurrentStamina = this.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
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

        public void RestoreStamina()
        {
            this.AddStamina(BASE_STAM_RESTORE);
        }
    }
}
