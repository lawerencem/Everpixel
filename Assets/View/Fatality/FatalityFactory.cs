using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Action;
using Assets.View.Fatality.Magic;
using Assets.View.Fatality.Weapon;
using Template.Other;

namespace Assets.View.Fatality
{
    public class FatalityFactory : ASingleton<FatalityFactory>
    {
        public MFatality GetFatality(MAction a)
        {
            var data = new FatalityData();
            data.Target = a.Data.Target;
            var active = this.TryProcessSpellFatality(a, data);
            if (active != null)
                return active;
            active = this.TryProcessWeaponFatality(a, data);
            if (active != null)
                return active;
            return null;
        }

        private MFatality TryProcessSpellFatality(MAction a, FatalityData data)
        {
            switch (a.ActiveAbility.Data.MagicType)
            {
                case (EMagicType.Fighting): { return new FightingFatality(data); }
            }
            return null;
        }

        private MFatality TryProcessWeaponFatality(MAction a, FatalityData data)
        {
            switch (a.Data.Ability)
            {
                case (EAbility.Crush): { return new CrushFatality(data); }
                case (EAbility.Slash): { return new SlashFatality(data); }
            }
            return null;
        }
    }
}
