using Controller.Managers.Map;
using Generics;
using Model.Abilities;
using Model.Abilities.Magic;
using Model.Events.Combat;

namespace View.Fatalities
{
    public class FatalityFactory : AbstractSingleton<FatalityFactory>
    {
        public GenericFatality GetFatality(CMapGUIControllerHit parent, DisplayActionEvent e)
        {
            var active = this.TryProcessActiveAbility(parent, e);
            if (active != null)
                return active;
            active = this.TryProcessWeaponAbility(parent, e);
            if (active != null)
                return active;
            return new GenericFatality(FatalityEnum.None, parent, e);
        }

        private GenericFatality TryProcessActiveAbility(CMapGUIControllerHit parent, DisplayActionEvent e)
        {
            switch (e.EventController.Action.MagicType)
            {
                case (MagicTypeEnum.Fighting): { return new FightingFatality(parent, e); }
            }
            return null;
        }

        private GenericFatality TryProcessWeaponAbility(CMapGUIControllerHit parent, DisplayActionEvent e)
        {
            switch (e.EventController.Action.Type)
            {
                case (EAbility.Crush): { return new CrushFatality(parent, e); }
                case (EAbility.Slash): { return new SlashFatality(parent, e); }
            }
            return null;
        }
    }
}
