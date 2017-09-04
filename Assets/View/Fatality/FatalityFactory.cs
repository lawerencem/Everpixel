using Assets.Model.Ability.Enum;
using Assets.Model.Ability.Magic;
using Assets.Model.Action;
using Assets.Template.Other;
using Assets.View.Fatality.Magic;
using Assets.View.Fatality.Weapon;

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
                case (EAbility.Chop): { return new ChopFatality(data); }
                case (EAbility.Crush): { return new CrushFatality(data); }
                case (EAbility.Slash): { return new SlashFatality(data); }
            }
            return null;
        }
    }
}
