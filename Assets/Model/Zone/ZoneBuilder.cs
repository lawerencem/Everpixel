using Assets.Data.Zone.Table;
using Assets.Model.Zone.Duration;
using Assets.Template.Util;
using UnityEngine;

namespace Assets.Model.Zone
{
    public class ZoneBuilder
    {
        private const string ZONE_PATH = "Sprites/Map/";

        public AZone Build(AZone zone)
        {
            var zoneParams = ZoneTable.Instance.Table[zone.Type];
            var data = this.GetZoneData(zone);
            data.Handle = new GameObject();
            var renderer = data.Handle.AddComponent<SpriteRenderer>();
            renderer.sprite = this.GetZoneSprite(zone, zoneParams);
            return zone;
        }

        private ZoneData GetZoneData(AZone zone)
        {
            if (zone.GetType().Equals(typeof(ADurationZone)))
                return new DurationZoneData();
            else
                return null;
        }

        private Sprite GetZoneSprite(AZone zone, ZoneParams zoneParams)
        {
            var path = ZONE_PATH + zone.Type.ToString();
            var sprites = Resources.LoadAll(path);
            var roll = ListUtil<int>.GetRandomElement(zoneParams.Sprites);
            return sprites[roll] as Sprite;
        }
    }
}
