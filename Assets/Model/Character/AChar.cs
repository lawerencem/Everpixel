using Assets.Generics;
using Model.Spells;
using Characters.Params;
using Model.Abilities;
using Model.Classes;
using Model.Equipment;
using Model.Injuries;
using Model.Map;
using Model.Shields;
using System.Collections.Generic;
using Model.Effects;
using Model.OverTimeEffects;
using Model.Perks;
using Assets.Model.Ability;
using Assets.Model.Character.Param;
using Assets.Model.Character.Enum;
using Assets.Model.Equipment.Types;
using Assets.Model.Equipment.Type;
using Assets.Model.Injury;

namespace Assets.Model.Character
{
    // TODO: Refactor!!!
    abstract public class AChar<T>
    {
        // TODO: Let's put this somewhere else...
        protected const double BASE_STAM_RESTORE = 50;

        public T Type { get; set; }

        public SpellsByLevel ActiveSpells { get; set; }

        public Dictionary<EClass, MClass> BaseClasses { get; set; }

        public CharPerkCollection Perks { get; set; }
        public PrimaryStats PrimaryStats { get; set; }
        public SecondaryStats SecondaryStats { get; set; }

        public ModContainer Mods;

        public CurrentPointsContainer Points;

        public List<MAbility> ActiveAbilities { get; set; }
        public List<MAbility> DefaultWpnAbilities { get; set; }
        public List<MInjury> Injuries { get; set; }

        // TODO: Put these in an equipment container class
        public MArmor Armor { get; set; }
        public MHelm Helm { get; set; }
        public MWeapon LWeapon { get; set; }
        public MWeapon RWeapon { get; set; }

        public FCharacterStatus StatusFlags { get; set; }

        public List<MEffect> Effects { get; set; }

        public List<MDoT> DoTs { get; set; }
        public List<GenericHoT> HoTs { get; set; }
        public List<Shield> Shields { get; set; }

        public void AddArmor(MArmor armor)
        {
            this.RemoveArmor();
            this.Armor = armor;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(armor, armor.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
                perk.TryModEquipmentMod(mods);
            this.Mods.AddMod(mods);
        }

        public void AddEffect(MEffect effect)
        {
            this.Effects.Add(effect);
        }

        public void AddDoT(MDoT dot) { this.DoTs.Add(dot); }
        public void AddHoT(GenericHoT hot) { this.HoTs.Add(hot); }

        public void AddHelm(MHelm helm)
        {
            this.RemoveHelm();
            this.Helm = helm;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
                perk.TryModEquipmentMod(mods);
            this.Mods.AddMod(mods);
        }

        public void AddInjury(MInjury injury)
        {
            this.Injuries.Add(injury);
        }

        public void AddShield(Shield toAdd)
        {
            this.Shields.Add(toAdd);
        }

        public void AddStamina(double toAdd)
        {
            this.Points.CurrentStamina += (int)toAdd;
            if (this.Points.CurrentStamina > (int)this.GetCurrentStatValue(ESecondaryStat.Stamina))
                this.Points.CurrentStamina = (int)this.GetCurrentStatValue(ESecondaryStat.Stamina);
        }

        public int GetCurrentAP()
        {
            return this.Points.CurrentAP;
        }

        public int GetCurrentHP()
        {
            return this.Points.CurrentHP;
        }

        public int GetCurrentMorale()
        {
            return this.Points.CurrentMorale;
        }

        public int GetCurrentStamina()
        {
            return this.Points.CurrentStamina;
        }

        public int GetTileTraversalAPCost(MTile tile)
        {
            // TODO: Work on this for height and various talents
            return tile.Cost * tile.Height;
        }

        public int GetTileTraversalStaminaCost(MTile tile)
        {
            return tile.Cost * tile.Height;
        }

        public void RemoveArmor()
        {
            this.Mods.RemoveGearMods(this.Armor);
            this.Armor = null;
        }

        public void RemoveHelm()
        {
            this.Mods.RemoveGearMods(this.Helm);
            this.Helm = null;
        }

        public void RemoveWeapon(bool lWeapon)
        {
            if (lWeapon)
            {
                this.Mods.RemoveGearMods(this.LWeapon);
                this.LWeapon = null;
            }
            else
            {
                this.Mods.RemoveGearMods(this.RWeapon);
                this.RWeapon = null;
            }
        }

        public int GetCurrentStatValue(EPrimaryStat stat)
        {
            double v = 0;
            switch (stat)
            {
                case (EPrimaryStat.Agility): { v = (double)this.PrimaryStats.Agility; } break;
                case (EPrimaryStat.Constitution): { v = (double)this.PrimaryStats.Constitution; } break;
                case (EPrimaryStat.Intelligence): { v = (double)this.PrimaryStats.Intelligence; } break;
                case (EPrimaryStat.Might): { v = (double)this.PrimaryStats.Might; } break;
                case (EPrimaryStat.Perception): { v = (double)this.PrimaryStats.Perception; } break;
                case (EPrimaryStat.Resolve): { v = (double)this.PrimaryStats.Resolve; } break;
            }
            foreach (var kvp in this.Mods.IndefPStatGearMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this.Injuries)
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this.Mods.PStatMods) { mod.TryScaleValue(stat, ref v); }
            return (int)v;
        }

        public double GetCurrentStatValue(ESecondaryStat stat)
        {
            double v = 0;
            switch (stat)
            {
                case (ESecondaryStat.AP): { v = (double)this.SecondaryStats.MaxAP; } break;
                case (ESecondaryStat.Block): { v = (double)this.SecondaryStats.Block; } break;
                case (ESecondaryStat.Concentration): { v = (double)this.SecondaryStats.Concentration; } break;
                case (ESecondaryStat.Critical_Chance): { v = (double)this.SecondaryStats.CriticalChance; } break;
                case (ESecondaryStat.Critical_Multiplier): { v = (double)this.SecondaryStats.CriticalMultiplier; } break;
                case (ESecondaryStat.Damage_Ignore): { v = (double)this.SecondaryStats.DamageIgnore; } break;
                case (ESecondaryStat.Damage_Reduction): { v = (double)this.SecondaryStats.DamageReduce; } break;
                case (ESecondaryStat.Dodge): { v = (double)this.SecondaryStats.DodgeSkill; } break;
                case (ESecondaryStat.Fortitude): { v = (double)this.SecondaryStats.Fortitude; } break;
                case (ESecondaryStat.HP): { v = (double)this.SecondaryStats.MaxHP; } break;
                case (ESecondaryStat.Initiative): { v = (double)this.SecondaryStats.Initiative; } break;
                case (ESecondaryStat.Melee): { v = (double)this.SecondaryStats.MeleeSkill; } break;
                case (ESecondaryStat.Morale): { v = (double)this.SecondaryStats.Morale; } break;
                case (ESecondaryStat.Parry): { v = (double)this.SecondaryStats.ParrySkill; } break;
                case (ESecondaryStat.Power): { v = (double)this.SecondaryStats.Power; } break;
                case (ESecondaryStat.Ranged): { v = (double)this.SecondaryStats.RangedSkill; } break;
                case (ESecondaryStat.Reflex): { v = (double)this.SecondaryStats.Reflex; } break;
                case (ESecondaryStat.Spell_Duration): { v = (double)this.SecondaryStats.SpellDuration; } break;
                case (ESecondaryStat.Spell_Penetration): { v = (double)this.SecondaryStats.SpellPenetration; } break;
                case (ESecondaryStat.Stamina): { v = (double)this.SecondaryStats.Stamina; } break;
                case (ESecondaryStat.Will): { v = (double)this.SecondaryStats.Will; } break;
            }
            foreach (var kvp in this.Mods.IndefSStatGearMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this.Injuries)
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this.Mods.SStatMods) { mod.TryScaleValue(stat, ref v); }
            foreach (var perk in this.Perks.SStatModPerks)
                perk.TryModSStat(stat, ref v);
            foreach (var mod in this.Mods.FlatSStatMods) { mod.TryFlatScaleStat(stat, ref v); }
            return v;
        }

        public void ProcessEndOfTurn()
        {
            this.RestoreStamina();
            this.ProcessBuffDurations();
            this.ProcessShields();
        }

        public void SetCurrentAP(int ap)
        {
            this.Points.CurrentAP = ap;
        }

        public void SetCurrentHP(int hp)
        {
            this.Points.CurrentHP = hp;
        }

        public void SetCurrentMorale(int mor)
        {
            this.Points.CurrentMorale = mor;
        }

        public void SetCurrentStam(int stam)
        {
            this.Points.CurrentStamina = stam;
        }

        public void TryAddMod(FlatSecondaryStatModifier mod)
        {
            this.Mods.FlatSStatMods.Add(mod);
            this.SetCurrValue(mod.Type, mod.FlatMod);
        }

        public void TryAddMod(SecondaryStatMod mod)
        {
            var oldValue = this.GetCurrentStatValue(mod.Type);
            this.Mods.SStatMods.Add(mod);
            var newValue = this.GetCurrentStatValue(mod.Type);
            var delta = newValue - oldValue;
            this.SetCurrValue(mod.Type, delta);
        }

        private void ProcessBuffDurations()
        {
            foreach (var buff in this.Mods.FlatSStatMods)
                buff.ProcessTurn();
            foreach (var buff in this.Mods.PStatMods)
                buff.ProcessTurn();
            foreach (var buff in this.Mods.SStatMods)
                buff.ProcessTurn();
            this.Mods.RemoveZeroDurations();
        }

        private void ProcessShields()
        {
            foreach (var shield in this.Shields)
                shield.ProcessTurn();
            this.Shields.RemoveAll(x => x.Dur <= 0);
        }

        private void RestoreStamina()
        {
            this.AddStamina(BASE_STAM_RESTORE);
        }

        private void SetCurrValue(ESecondaryStat type, double v)
        {
            switch(type)
            {
                case (ESecondaryStat.AP): { this.Points.CurrentAP += (int)v; } break;
                case (ESecondaryStat.HP): { this.Points.CurrentHP += (int)v; } break;
                case (ESecondaryStat.Morale): { this.Points.CurrentMorale += (int)v; } break;
                case (ESecondaryStat.Stamina): { this.Points.CurrentStamina += (int)v; } break;
            }
        }
    }
}
