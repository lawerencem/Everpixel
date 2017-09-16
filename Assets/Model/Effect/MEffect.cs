using Assets.Model.Ability.Enum;
using Assets.Model.Combat.Hit;

namespace Assets.Model.Effect
{
    public class MEffectData
    {
        public EAbility AbilityCondition { get; set; }
        public ECastType CastCondition { get; set; }
        public int Duration { get; set; }
        public string ParticlePath { get; set; }
        public string SummonKey { get; set; }
        public string WeaponCondition { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public MEffectData()
        {
            this.WeaponCondition = "None";
        }
    }

    public class MEffect
    {
        private MEffectData _data;
        private EEffect _type;

        public MEffectData Data { get { return this._data; } }
        public EEffect Type { get { return this._type; } }

        public void SetData(MEffectData data) { this._data = data; }
        
        public MEffect(EEffect type)
        {
            this._type = type;
        }

        public virtual bool CheckConditions(MHit hit)
        {
            if (this.Data.AbilityCondition != EAbility.None)
                if (hit.Data.Ability.Type != this.Data.AbilityCondition)
                    return false;
            if (this.Data.CastCondition != ECastType.None)
                if (hit.Data.Ability.Data.CastType != this.Data.CastCondition)
                    return false;
            if (this.Data.WeaponCondition != "None")
            {
                if (hit.Data.Ability.Data.ParentWeapon == null)
                    return false;
                else
                {
                    var name = hit.Data.Ability.Data.ParentWeapon.Data.Name;
                    if (!name.Contains(this.Data.WeaponCondition))
                        return false;
                }
            }
            return true;
        }

        public virtual MEffectData CloneData()
        {
            var data = new MEffectData();
            data.AbilityCondition = this.Data.AbilityCondition;
            data.CastCondition = this.Data.CastCondition;
            data.Duration = this.Data.Duration;
            data.ParticlePath = this.Data.ParticlePath;
            data.SummonKey = this.Data.SummonKey;
            data.WeaponCondition = this.Data.WeaponCondition;
            data.X = this.Data.X;
            data.Y = this.Data.Y;
            return data;
        }

        public virtual void TryProcessHit(MHit hit) { }
        public virtual void TryProcessTurn(MHit hit) { }
    }
}
