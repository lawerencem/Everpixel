using Assets.Model.Zone.Duration;
using Assets.Template.Other;

namespace Assets.Model.Zone
{
    public class ZoneFactory : ASingleton<ZoneFactory>
    {
        public AZone CreateNewObject(EZone type)
        {
            switch(type)
            {
                case (EZone.Slime): { return new SlimeZone(); }
                default: return null;
            }
        }
    }
}
