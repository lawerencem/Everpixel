using Assets.Model.Equipment.Enum;
using Assets.Model.Equipment.Weapon;
using Assets.View.Equipment;
using Assets.View.Fatality;

namespace Assets.Controller.Equipment.Weapon
{
    public class CWeapon
    {
        private MWeapon _model;
        private WeaponParams _params;
        private VWeapon _view;

        public MWeapon Model { get { return this._model; } }
        public WeaponParams Params { get { return this._params; } }
        public VWeapon View { get { return this._view; } }

        public bool CustomBullet { get { return this.Model.Data.CustomBullet; } }
        public EFatality CustomFatality { get { return this.Model.Data.CustomFatality; } }
        public bool Embed { get { return this.Model.Data.Embed; } }
        public string EmbedPath { get { return this._model.Data.EmbedPath; } }
        public string SpriteFXPath { get { return this.Model.Data.SpriteFXPath; } }

        public void SetModel(MWeapon m) { this._model = m; }
        public void SetParams(WeaponParams p) { this._params = p; }
        public void SetView(VWeapon v) { this._view = v; }

        public double GetStat(EWeaponStat stat)
        {
            switch(stat)
            {
                case (EWeaponStat.Accuracy_Mod): { return this._model.Data.AccuracyMod; }
                case (EWeaponStat.AP_Mod): { return this._model.Data.APMod; }
                case (EWeaponStat.Armor_Ignore): { return this._model.Data.ArmorIgnore; }
                case (EWeaponStat.Armor_Pierce): { return this._model.Data.ArmorPierce; }
                case (EWeaponStat.Block_Ignore): { return this._model.Data.BlockIgnore; }
                case (EWeaponStat.Damage): { return this._model.Data.Damage; }
                case (EWeaponStat.Dodge_Mod): { return this._model.Data.DodgeMod; }
                case (EWeaponStat.Fatigue_Mod): { return this._model.Data.FatigueMod; }
                case (EWeaponStat.Initiative_Mod): { return this._model.Data.InitiativeMod; }
                case (EWeaponStat.Max_Durability): { return this._model.Data.MaxDurability; }
                case (EWeaponStat.Melee_Block_Chance): { return this._model.Data.MeleeBlockChance; }
                case (EWeaponStat.Parry_Mod): { return this._model.Data.ParryMod; }
                case (EWeaponStat.Ranged_Block_Chance): { return this._model.Data.RangeBlockChance; }
                case (EWeaponStat.Range_Mod): { return this._model.Data.RangeMod; }
                case (EWeaponStat.Shield_Damage_Percent): { return this._model.Data.ShieldDamagePercent; }
                case (EWeaponStat.Stamina_Mod): { return this._model.Data.StaminaMod; }
                case (EWeaponStat.Value): { return this._model.Data.Value; }
                default: return -1;
            }
        }

        public double GetDurabilityPercentage()
        {
            return this.Model.Data.Durability / this.Model.Data.MaxDurability;
        }

        public bool IsTypeOfShield() { return this._model.IsTypeOfShield(); }
    }
}
