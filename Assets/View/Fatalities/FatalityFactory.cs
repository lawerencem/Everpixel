﻿using Controller.Managers.Map;
using Generics;
using Model.Abilities;
using Model.Abilities.Magic;
using Model.Events.Combat;

namespace View.Fatalities
{
    public class FatalityFactory : AbstractSingleton<FatalityFactory>
    {
        public GenericFatality GetFatality(CMapGUIControllerHit parent, DisplayHitStatsEvent e)
        {
            var active = this.TryProcessActiveAbility(parent, e);
            if (active != null)
                return active;

            return new GenericFatality(FatalityEnum.None, parent, e);
        }

        private GenericFatality TryProcessActiveAbility(CMapGUIControllerHit parent, DisplayHitStatsEvent e)
        {
            if (e.Hit.Ability.Type.GetType() == (typeof(ActiveAbilitiesEnum)))
            {
                var active = e.Hit.Ability as GenericActiveAbility;
                switch (active.MagicType)
                {
                    case (MagicTypeEnum.Fighting): { return new FightingFatality(parent, e); }
                }
            }

            return null;
        }
    }
}
