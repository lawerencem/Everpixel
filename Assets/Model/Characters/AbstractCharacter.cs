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

namespace Model.Characters
{
    // TODO: Refactor!!!
    abstract public class AbstractCharacter<T>
    {
        // TODO: Let's put this somewhere else...
        protected const double BASE_STAM_RESTORE = 50;

        public T Type { get; set; }

        public SpellsByLevel ActiveSpells { get; set; }

        public Dictionary<ClassEnum, GenericClass> BaseClasses { get; set; }

        public CharacterPerkCollection Perks { get; set; }
        public PrimaryStats PrimaryStats { get; set; }
        public SecondaryStats SecondaryStats { get; set; }

        public ModContainer Mods;

        public CurrentPointsContainer Points;

        public List<GenericAbility> ActiveAbilities { get; set; }
        public List<GenericAbility> DefaultWpnAbilities { get; set; }
        public List<GenericInjury> Injuries { get; set; }

        // TODO: Put these in an equipment container class
        public GenericArmor Armor { get; set; }
        public GenericHelm Helm { get; set; }
        public GenericWeapon LWeapon { get; set; }
        public GenericWeapon RWeapon { get; set; }

        public CharacterStatusFlags StatusFlags { get; set; }

        public List<GenericEffect> Effects { get; set; }

        public List<GenericDoT> DoTs { get; set; }
        public List<GenericHoT> HoTs { get; set; }
        public List<Shield> Shields { get; set; }

        public void AddArmor(GenericArmor armor)
        {
            this.RemoveArmor();
            this.Armor = armor;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(armor, armor.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
                perk.TryModEquipmentMod(mods);
            this.Mods.AddMod(mods);
        }

        public void AddEffect(GenericEffect effect)
        {
            this.Effects.Add(effect);
        }

        public void AddDoT(GenericDoT dot) { this.DoTs.Add(dot); }
        public void AddHoT(GenericHoT hot) { this.HoTs.Add(hot); }

        public void AddHelm(GenericHelm helm)
        {
            this.RemoveHelm();
            this.Helm = helm;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
                perk.TryModEquipmentMod(mods);
            this.Mods.AddMod(mods);
        }

        public void AddInjury(GenericInjury injury)
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
            if (this.Points.CurrentStamina > (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina))
                this.Points.CurrentStamina = (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
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
                this.Mods.AddMod(mods);
            }
            else
            {
                this.RWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());
                foreach (var perk in this.Perks.EquipmentSStatPerks)
                {
                    perk.TryModEquipmentMod(mods);
                }
                this.Mods.AddMod(mods);
            }
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

        public int GetTileTraversalAPCost(HexTile tile)
        {
            // TODO: Work on this for height and various talents
            return tile.Cost * tile.Height;
        }

        public int GetTileTraversalStaminaCost(HexTile tile)
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
            foreach (var kvp in this.Mods.IndefPStatMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this.Injuries)
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this.Mods.PStatMods) { mod.TryScaleValue(stat, ref v); }
            return (int)v;
        }

        public double GetCurrentStatValue(SecondaryStatsEnum stat)
        {
            double v = 0;
            switch (stat)
            {
                case (SecondaryStatsEnum.AP): { v = (double)this.SecondaryStats.MaxAP; } break;
                case (SecondaryStatsEnum.Block): { v = (double)this.SecondaryStats.Block; } break;
                case (SecondaryStatsEnum.Concentration): { v = (double)this.SecondaryStats.Concentration; } break;
                case (SecondaryStatsEnum.Critical_Chance): { v = (double)this.SecondaryStats.CriticalChance; } break;
                case (SecondaryStatsEnum.Critical_Multiplier): { v = (double)this.SecondaryStats.CriticalMultiplier; } break;
                case (SecondaryStatsEnum.Damage_Ignore): { v = (double)this.SecondaryStats.DamageIgnore; } break;
                case (SecondaryStatsEnum.Damage_Reduction): { v = (double)this.SecondaryStats.DamageReduce; } break;
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
            foreach (var kvp in this.Mods.IndefSStatMods)
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

        public void TryAddMod(SecondaryStatModifier mod)
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

        private void SetCurrValue(SecondaryStatsEnum type, double v)
        {
            switch(type)
            {
                case (SecondaryStatsEnum.AP): { this.Points.CurrentAP += (int)v; } break;
                case (SecondaryStatsEnum.HP): { this.Points.CurrentHP += (int)v; } break;
                case (SecondaryStatsEnum.Morale): { this.Points.CurrentMorale += (int)v; } break;
                case (SecondaryStatsEnum.Stamina): { this.Points.CurrentStamina += (int)v; } break;
            }
        }
    }
}
