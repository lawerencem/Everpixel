using Controller.Map;
using Model.Events.Combat;

namespace Assets.Model.Zone
{
    public abstract class AZone
    {
        public TileController Tile { get; set; }

        public AZone(ZoneArgsContainer arg) { this.Tile = arg.Tile; }

        public virtual void ProcessEnterZone(ZoneEnterEvent e) { }
        public virtual void ProcessExitZone(ZoneExitEvent e) {  }
    }
}
