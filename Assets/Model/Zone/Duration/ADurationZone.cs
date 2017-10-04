using UnityEngine;

namespace Assets.Model.Zone.Duration
{
    public class DurationZoneData : ZoneData
    {
        public int Dur { get; set; }
    }

    public abstract class ADurationZone : AZone
    {
        protected DurationZoneData _data;

        public void SetData(DurationZoneData data) { this._data = data; }

        public void ProcessTurn()
        {
            this._data.Dur--;
        }
    }
}
