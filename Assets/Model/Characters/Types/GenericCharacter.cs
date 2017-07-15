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
            this.Effects = new List<GenericEffect>();
            this.FlatSStatMods = new List<FlatSecondaryStatModifier>();
            this.IndefSStatMods = new List<Pair<object, List<IndefSecondaryStatModifier>>>();
            this.Injuries = new List<GenericInjury>();
            this.Perks = new CharacterPerkCollection();
            this.PStatMods = new List<PrimaryStatModifier>();
            this.Shields = new List<Shield>();
            this.StatusFlags = new CharacterStatusFlags();
            this.SStatMods = new List<SecondaryStatModifier>();
        }

        public void ModifyHP(int value, bool isHeal)
        {
            if (isHeal)
            {
                this.CurrentHP += value;
                if (this.CurrentHP > (int)this.GetCurrentStatValue(SecondaryStatsEnum.HP))
                    this.CurrentHP = (int)this.GetCurrentStatValue(SecondaryStatsEnum.HP);
            }
            else
            {
                int dmg = value;
                foreach (var shield in this.Shields)
                    shield.ProcessShieldDmg(ref dmg);
                this.Shields.RemoveAll(x => x.CurHP <= 0);

                if (dmg >= 0)
                    this.CurrentHP -= dmg;

                if (this.CurrentHP <= 0)
                {
                    var killed = new CharacterKilledEvent(CombatEventManager.Instance, this.ParentController);
                }
            }
        }
    }
}
