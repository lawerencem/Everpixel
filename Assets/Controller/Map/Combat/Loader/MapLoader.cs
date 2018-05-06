using Assets.Model.Map;
using Assets.View;
using System;
using UnityEngine;

namespace Assets.Controller.Map.Combat.Loader
{
    public class MapLoader
    {
        private MMapController _map;

        public Transform MapHolder;
        public Transform BackgroundTiles;

        public MapLoader()
        {
            this.MapHolder = new GameObject("Map").transform;
            this.MapHolder.tag = "BattleMap";
            this.BackgroundTiles = new GameObject("BackgroundTiles").transform;
            this.BackgroundTiles.transform.SetParent(this.MapHolder);
            this._map = new MMapController();
        }

        public MMapController GetCombatMap(MapInitInfo info)
        {
            var builder = new MapBuilder();
            var hexMap = builder.GetMap(info.Rows, info.Cols, ViewParams.OFFSET, ViewParams.MAP_CENTER);
            var map = new MMap(hexMap);
            this._map.SetMap(map);
            this.InitTiles(info);
            this.InitArmies(info);
            this.InitChars(info);
            return this._map;
        }

        private void InitArmies(MapInitInfo info)
        {
            var loader = new PartyLoader();
            loader.Init(this._map, info);
        }

        private void InitChars(MapInitInfo info)
        {
            try
            {
                CharLoader.Instance.Init(this.MapHolder, this._map, info);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        private void InitTiles(MapInitInfo info)
        {
            var loader = new TileLoader();
            loader.InitLandmarks(this._map, info);
            loader.InitTiles(this._map, info, BackgroundTiles);
            loader.RenderHeights(this._map);
            loader.InitMapEnvironment(this._map, info);
        }
    }
}

