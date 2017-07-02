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

        // TODO: Put these in a stats Mods container class
        public List<FlatSecondaryStatModifier> FlatSStatMods { get; set; }
        public List<PrimaryStatModifier> PStatMods { get; set; }
        public List<SecondaryStatModifier> SStatMods { get; set; }
        public List<Pair<object, List<IndefPrimaryStatModifier>>> IndefPStatMods { get; set; }
        public List<Pair<object, List<IndefSecondaryStatModifier>>> IndefSStatMods { get; set; }

        // TODO: Put these in a current stats container class
        public int CurrentAP { get; set; }
        public int CurrentHP { get; set; }
        public int CurrentMorale { get; set; }
        public int CurrentStamina { get; set; }

        public List<GenericAbility> ActiveAbilities { get; set; }
        public List<GenericAbility> DefaultWpnAbilities { get; set; }
        public List<GenericInjury> Injuries { get; set; }

        // TODO: Put these in an equipment container class
        public GenericArmor Armor { get; set; }
        public GenericHelm Helm { get; set; }
        public GenericWeapon LWeapon { get; set; }
        public GenericWeapon RWeapon { get; set; }

        public CharacterStatusFlags StatusFlags { get; set; }

        public List<Shield> Shields { get; set; }

        public void AddArmor(GenericArmor armor)
        {
            this.RemoveArmor();
            this.Armor = armor;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(armor, armor.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
            {
                perk.TryModEquipmentMod(mods);
            }
            this.IndefSStatMods.Add(mods);
        }

        public void AddHelm(GenericHelm helm)
        {
            this.RemoveHelm();
            this.Helm = helm;
            var mods = new Pair<object, List<IndefSecondaryStatModifier>>(helm, helm.GetStatModifiers());
            foreach (var perk in this.Perks.EquipmentSStatPerks)
            {
                perk.TryModEquipmentMod(mods);
            }
            this.IndefSStatMods.Add(mods);
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
            this.CurrentStamina += (int)toAdd;
            if (this.CurrentStamina > (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina))
                this.CurrentStamina = (int)this.GetCurrentStatValue(SecondaryStatsEnum.Stamina);
        }

        public void AddWeapon(GenericWeapon weapon, bool lWeapon)
        {
            // TODO: 2handed weapon check
            if (lWeapon)
            {
                this.LWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());
                foreach (var perk in this.Perks.EquipmentSStatPerks)
                {
                    perk.TryModEquipmentMod(mods);
                }
                this.IndefSStatMods.Add(mods);
            }
            else
            {
                this.RWeapon = weapon;
                var mods = new Pair<object, List<IndefSecondaryStatModifier>>(weapon, weapon.GetStatModifiers());
                foreach (var perk in this.Perks.EquipmentSStatPerks)
                {
                    perk.TryModEquipmentMod(mods);
                }
                this.IndefSStatMods.Add(mods);
            }
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
            foreach (var kvp in this.IndefPStatMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this.Injuries)
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this.PStatMods) { mod.TryScaleValue(stat, ref v); }
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
            foreach (var kvp in this.IndefSStatMods)
                foreach (var mod in kvp.Y)
                    mod.TryScaleValue(stat, ref v);
            foreach (var injury in this.Injuries)
                injury.TryScaleStat(stat, ref v);
            foreach (var mod in this.SStatMods) { mod.TryScaleValue(stat, ref v); }
            foreach (var perk in this.Perks.SStatModPerks)
                perk.TryModSStat(stat, ref v);
            foreach (var mod in this.FlatSStatMods) { mod.TryFlatScaleStat(stat, ref v); }
            return v;
        }

        public void ProcessEndOfTurn()
        {
            this.RestoreStamina();
            this.ProcessBuffDurations();
            this.ProcessShields();
        }

        public void TryAddMod(FlatSecondaryStatModifier mod)
        {
            this.FlatSStatMods.Add(mod);
            this.SetCurrValue(mod.Type, mod.FlatMod);
        }

        public void TryAddMod(SecondaryStatModifier mod)
        {
            var oldValue = this.GetCurrentStatValue(mod.Type);
            this.SStatMods.Add(mod);
            var newValue = this.GetCurrentStatValue(mod.Type);
            var delta = newValue - oldValue;
            this.SetCurrValue(mod.Type, delta);
        }

        private void ProcessBuffDurations()
        {
            foreach (var buff in this.FlatSStatMods)
                buff.ProcessTurn();
            foreach (var buff in this.PStatMods)
                buff.ProcessTurn();
            foreach (var buff in this.SStatMods)
                buff.ProcessTurn();
            this.FlatSStatMods.RemoveAll(x => x.Duration <= 0);
            this.PStatMods.RemoveAll(x => x.Duration <= 0);
            this.SStatMods.RemoveAll(x => x.Duration <= 0);
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
                case (SecondaryStatsEnum.AP): { this.CurrentAP += (int)v; } break;
                case (SecondaryStatsEnum.HP): { this.CurrentHP += (int)v; } break;
                case (SecondaryStatsEnum.Morale): { this.CurrentMorale += (int)v; } break;
                case (SecondaryStatsEnum.Stamina): { this.CurrentStamina += (int)v; } break;
            }
        }
    }
}