using Assets.Model.Ability;
using Assets.Model.Character.Param;
using Assets.Model.Effect;
using Assets.Model.Equipment.Enum;
using Assets.Model.Injury;
using Assets.View.Fatality;
using System.Collections.Generic;

namespace Assets.Model.Equipment.Weapon
{
    public class MWeaponData : MEquipmentData
    {
        public string Name { get; set; }
        public List<MAbility> Abilities { get; set; }
        public double AccuracyMod { get; set; }
        public double APMod { get; set; }
        public double ArmorIgnore { get; set; }
        public double ArmorPierce { get; set; }
        public double BlockIgnore { get; set; }
        public bool CustomBullet { get; set; }
        public EFatality CustomFatality { get; set; }
        public double Damage { get; set; }
        public string Description { get; set; }
        public double DodgeMod { get; set; }
        public List<MEffect> Effects { get; set; }
        public bool Embed { get; set; }
        public string EmbedPath { get; set; }
        public double FatigueMod { get; set; }
        public double InitiativeMod { get; set; }
        public List<EInjury> Injuries { get; set; }
        public double MeleeBlockChance { get; set; }
        public double ParryMod { get; set; }
        public double RangeBlockChance { get; set; }
        public double RangeMod { get; set; }
        public double ShieldDamagePercent { get; set; }
        public EWeaponSkill Skill { get; set; }
        public string SpriteFXPath { get; set; }
        public double StaminaMod { get; set; }
        public EWeaponType WpnType { get; set; }
    }

    public class MWeapon : MEquipment
    {
        private MWeaponData _data;
        public MWeaponData Data { get { return this._data; } }

        public MWeapon() : base(EEquipmentType.Held)
        {
            this._data = new MWeaponData();
            this.Data.Abilities = new List<MAbility>();
            this.Data.AccuracyMod = 1;
            this.Data.APMod = 1;
            this.Data.ArmorIgnore = 1;
            this.Data.ArmorPierce = 1;
            this.Data.BlockIgnore = 1;
            this.Data.Effects = new List<MEffect>();
            this.Data.Embed = false;
            this.Data.EmbedPath = "";
            this.Data.Damage = 0;
            this.Data.Durability = 0;
            this.Data.FatigueMod = 1;
            this.Data.InitiativeMod = 1;
            this.Data.Injuries = new List<EInjury>();
            this.Data.MaxDurability = 0;
            this.Data.ParryMod = 1;
            this.Data.RangeMod = 0;
            this.Data.ShieldDamagePercent = 1;
            this.Data.StaminaMod = 1;
        }

        public bool IsTypeOfShield()
        {
            if (this.Data.Skill == EWeaponSkill.Small_Shield ||
                this.Data.Skill == EWeaponSkill.Medium_Shield ||
                this.Data.Skill == EWeaponSkill.Large_Shield)
                return true;
            else
                return false;
        }

        public List<StatMod> GetStatModifiers()
        {
            var toReturn = new List<StatMod>();

            var ap = this.GetModProto();
            var init = this.GetModProto();
            var parry = this.GetModProto();
            var stam = this.GetModProto();

            ap.Scalar = this.Data.APMod;
            init.Scalar = this.Data.InitiativeMod;
            parry.Scalar = this.Data.ParryMod;
            stam.Scalar = this.Data.StaminaMod;

            toReturn.Add(new StatMod(ap));
            toReturn.Add(new StatMod(init));
            toReturn.Add(new StatMod(parry));
            toReturn.Add(new StatMod(stam));

            return toReturn;
        }
    }
}
