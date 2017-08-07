using Controller.Map;

namespace Assets.Model.Zone
{
    public abstract class AZone
    {
        public TileController Tile { get; set; }

        public AZone(ZoneArgsCont arg) { this.Tile = arg.Tile; }

        //public virtual void ProcessEnterZone(EvZoneEnter e) { }
        //public virtual void ProcessExitZone(EvZoneExit e) { }
    }
}
