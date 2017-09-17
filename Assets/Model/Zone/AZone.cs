using Assets.Controller.Map.Tile;

namespace Assets.Model.Zone
{
    public abstract class AZone
    {
        public CTile Tile { get; set; }

        public AZone(ZoneArgsCont arg) { this.Tile = arg.Tile; }

        //public virtual void ProcessEnterZone(EvZoneEnter e) { }
        //public virtual void ProcessExitZone(EvZoneExit e) { }
    }
}
