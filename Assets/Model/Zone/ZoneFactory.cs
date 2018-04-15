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
                case (EZone.Spear_Wall_Zone): { return zoneBuilder.Build(new SpearWallZone()); }
                case (EZone.Slime_Zone): { return zoneBuilder.Build(new SlimeZone()); }
                default: return null;
            }
        }
    }
}
