using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Action;
using Assets.Template.Other;
using Assets.View.Fatality.Magic;
using Assets.View.Fatality.Weapon.Ability;

namespace Assets.View.Fatality
{
    public class FatalityFactory : ASingleton<FatalityFactory>
    {
        public MFatality GetFatality(MAction a)
        {
            var data = new FatalityData();
            data.Action = a;
            data.Source = a.Data.Source;
            data.Target = a.Data.Target;
            foreach (var hit in a.Data.Hits)
            {
                if (hit.Data.IsFatality)
                    data.FatalHits.Add(hit);
                else
                    data.NonFatalHits.Add(hit);
            }
            var fatality = this.TryProcessSpecificFatality(a, data);
            if (fatality != null)
                return fatality;
            fatality = this.TryProcessSpellFatality(a, data);
            if (fatality != null)
                return fatality;
            fatality = this.TryProcessWeaponFatality(a, data);
            if (fatality != null)
                return fatality;
            return null;
        }

        private MFatality TryProcessSpellFatality(MAction a, FatalityData data)
        {
            switch (a.ActiveAbility.Data.MagicType)
            {
                case (EMagicType.Fighting): { return new FightingFatality(data); }
                default: return null;
            }
        }

        private MFatality TryProcessSpecificFatality(MAction a, FatalityData data)
        {
            var type = EFatality.None;
            if (a.Data.LWeapon)
            {
                if (a.Data.Source.Proxy.GetLWeapon() != null)
                    type = a.Data.Source.Proxy.GetLWeapon().CustomFatality;
            }
            else
            {
                if (a.Data.Source.Proxy.GetRWeapon() != null)
                    type = a.Data.Source.Proxy.GetRWeapon().CustomFatality;
            }
            return this.TryProcessSpecificFatalityHelper(type, data);
        }

        private MFatality TryProcessSpecificFatalityHelper(EFatality type, FatalityData data)
        {
            switch(type)
            {
                case (EFatality.Weenbow): { return new WeenbowFatality(data); }
                default: return null;
            }
        }

        private MFatality TryProcessWeaponFatality(MAction a, FatalityData data)
        {
            switch (a.Data.Ability)
            {
                case (EAbility.Chop): { return new ChopFatality(data); }
                case (EAbility.Crush): { return new CrushFatality(data); }
                case (EAbility.Slash): { return new SlashFatality(data); }
                default: return null;
            }
        }
    }
}
