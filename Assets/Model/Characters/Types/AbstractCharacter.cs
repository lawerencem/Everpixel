using Assets.Generics;
using Characters.Params;
using Model.Equipment;
using System.Collections.Generic;

namespace Model.Characters
{
    abstract public class AbstractCharacter<T>
    {
        public T Type { get; set; }

        public PrimaryStats PrimaryStats { get; set; }
        public SecondaryStats SecondaryStats { get; set; }
        
        public List<PrimaryStatModifier> PStatMods { get; set; }
        public List<SecondaryStatModifier> SStatMods { get; set; }
        public List<Pair<object, List<IndefSecondaryStatModifier>>> IndefSStatMods { get; set; }

        public GenericArmor Armor { get; set; }
        public GenericHelm Helm { get; set; }
        public GenericWeapon LWeapon { get; set; }
        public GenericWeapon RWeapon { get; set; }

        public void AddArmor(GenericArmor armor)
        {
            this.RemoveArmor();
            this.Armor = armor;
            this.IndefSStatMods.Add(new Pair<object, List<IndefSecondaryStatModifier>>(armor, armor.GetStatModifiers()));
        }

        public void AddHelm(GenericHelm helm)
        {
            this.RemoveHelm();
            this.Helm = helm;
            this.IndefSStatMods.Add(new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.GetStatModifiers()));
        }

        public void AddWeapon(GenericWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this.LWeapon = weapon;
            }
                
            else
            {
                this.RWeapon = weapon;
            }
                
        }

        public void RemoveArmor()
        {
            var kvp = this.IndefSStatMods.Find(x => x.X == this.Armor);
            if (!kvp.Equals(null))
                this.IndefSStatMods.Remove(kvp);
            this.Armor = null;
        }

        public void RemoveHelm()
        {
            var kvp = this.IndefSStatMods.Find(x => x.X == this.Helm);
            if (!kvp.Equals(null))
                this.IndefSStatMods.Remove(kvp);
            this.Helm = null;
        }

        public void RemoveWeapon(bool lWeapon)
        {
            if (lWeapon)
            {
                var kvp = this.IndefSStatMods.Find(x => x.X == this.LWeapon);
                if (!kvp.Equals(null))
                    this.IndefSStatMods.Remove(kvp);
                this.LWeapon = null;
            }
            else
            {
                var kvp = this.IndefSStatMods.Find(x => x.X == this.RWeapon);
                if (!kvp.Equals(null))
                    this.IndefSStatMods.Remove(kvp);
                this.RWeapon = null;
            }
        }

        public int GetCurrentStatValue(PrimaryStatsEnum stat)
        {
            double v = 0;
            switch (stat)
            {
                case (PrimaryStatsEnum.Agility): { v = (double)this.PrimaryStats.Agility; } break;
                case (PrimaryStatsEnum.Constitution): { v = (double)this.PrimaryStats.Constitution; } break;
                case (PrimaryStatsEnum.Intelligence): { v = (double)this.PrimaryStats.Intelligence; } break;
                case (PrimaryStatsEnum.Might): { v = (double)this.PrimaryStats.Might; } break;
                case (PrimaryStatsEnum.Perception): { v = (double)this.PrimaryStats.Perception; } break;
                case (PrimaryStatsEnum.Resolve): { v = (double)this.PrimaryStats.Resolve; } break;
            }
            foreach (var mod in this.PStatMods) { mod.TryScaleValue(stat, ref v); }
            return (int)v;
        }

        public int GetCurrentStatValue(SecondaryStatsEnum stat)
        {
            double v = 0;
            switch (stat)
            {
                case (SecondaryStatsEnum.AP): { v = (double)this.SecondaryStats.MaxAP; } break;
                case (SecondaryStatsEnum.Block): { v = (double)this.SecondaryStats.Block; } break;
                case (SecondaryStatsEnum.Concentration): { v = (double)this.SecondaryStats.Concentration; } break;
                case (SecondaryStatsEnum.Critical_Chance): { v = (double)this.SecondaryStats.CriticalChance; } break;
                case (SecondaryStatsEnum.Critical_Multiplier): { v = (double)this.SecondaryStats.CriticalMultiplier; } break;
                case (SecondaryStatsEnum.Dodge): { v = (double)this.SecondaryStats.DodgeSkill; } break;
                case (SecondaryStatsEnum.Fortitude): { v = (double)this.SecondaryStats.Fortitude; } break;
                case (SecondaryStatsEnum.HP): { v = (double)this.SecondaryStats.MaxHP; } break;
                case (SecondaryStatsEnum.Initiative): { v = (double)this.SecondaryStats.Initiative; } break;
                case (SecondaryStatsEnum.Melee): { v = (double)this.SecondaryStats.MeleeSkill; } break;
                case (SecondaryStatsEnum.Morale): { v = (double)this.SecondaryStats.Morale; } break;
                case (SecondaryStatsEnum.Parry): { v = (double)this.SecondaryStats.ParrySkill; } break;
                case (SecondaryStatsEnum.Power): { v = (double)this.SecondaryStats.Power; } break;
                case (SecondaryStatsEnum.Ranged): { v = (double)this.SecondaryStats.RangedSkill; } break;
                case (SecondaryStatsEnum.Reflex): { v = (double)this.SecondaryStats.Reflex; } break;
                case (SecondaryStatsEnum.Spell_Duration): { v = (double)this.SecondaryStats.SpellDuration; } break;
                case (SecondaryStatsEnum.Spell_Penetration): { v = (double)this.SecondaryStats.SpellPenetration; } break;
                case (SecondaryStatsEnum.Stamina): { v = (double)this.SecondaryStats.Stamina; } break;
                case (SecondaryStatsEnum.Will): { v = (double)this.SecondaryStats.Will; } break;
            }
            foreach (var mod in this.SStatMods) { mod.TryScaleValue(stat, ref v); }
            foreach (var kvp in this.IndefSStatMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            return (int)v;
        }
    }
}