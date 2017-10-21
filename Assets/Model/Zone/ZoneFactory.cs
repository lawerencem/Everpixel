using Assets.Model.Zone.Duration;
using Assets.Template.Other;

namespace Assets.Model.Zone
{
    public class ZoneFactory : ASingleton<ZoneFactory>
    {
        public AZone CreateNewObject(EZone type)
        {
            var zoneBuilder = new ZoneBuilder();
            switch(type)
            {
                case (EZone.Zone_Slime): { return zoneBuilder.Build(new ZoneSlime()); }
                default: return null;
            }
        }
    }
}
