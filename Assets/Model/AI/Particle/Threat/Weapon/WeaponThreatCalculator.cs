using Assets.Controller.Equipment.Weapon;
using Assets.Data.AI.Weapon;
using Assets.Model.Equipment.Enum;

namespace Assets.Model.AI.Particle.Threat.Weapon
{
    public class WeaponThreatCalculator
    {
        public double GetWeaponThreatPoints(CWeapon weapon)
        {
            double threat = 0;
            if (weapon != null)
            {
                threat += this.GetAccuracyThreat(weapon);
                threat += this.GetArmorIgnoreThreat(weapon);
                threat += this.GetArmorPierceThreat(weapon);
                threat += this.GetBlockIgnoreThreat(weapon);
                threat += this.GetDamageThreat(weapon);
                threat += this.GetShieldDamageThreat(weapon);
            }
            return threat;
        }

        private double GetAccuracyThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Accuracy_Mod);
            return WeaponThreatTable.Instance.Table[EWeaponStat.Accuracy_Mod] * threat;
        }

        private double GetArmorIgnoreThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Armor_Ignore);
            if (threat > 0)
                return WeaponThreatTable.Instance.Table[EWeaponStat.Armor_Ignore] / threat;
            else
                return 0;
        }

        private double GetArmorPierceThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Armor_Pierce);
            if (threat > 0)
                return WeaponThreatTable.Instance.Table[EWeaponStat.Armor_Pierce] / threat;
            else
                return 0;
        }

        private double GetBlockIgnoreThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Block_Ignore);
            return WeaponThreatTable.Instance.Table[EWeaponStat.Block_Ignore] * threat;
        }

        private double GetDamageThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Damage);
            return WeaponThreatTable.Instance.Table[EWeaponStat.Damage] * threat;
        }

        private double GetShieldDamageThreat(CWeapon weapon)
        {
            double threat = weapon.GetStat(EWeaponStat.Shield_Damage_Percent);
            return WeaponThreatTable.Instance.Table[EWeaponStat.Shield_Damage_Percent] * threat;
        }
    }
}
