﻿using Assets.Model.Map;
using Assets.Template.Hex;
using Assets.View;
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
            var hexMap = HexMapBuilder.GetMap(info.Rows, info.Cols, ViewParams.OFFSET, ViewParams.MAP_CENTER);
            var map = new MMap(hexMap);
            this._map.SetMap(map);
            this.InitTiles(info);
            this.InitParties(info);
            this.InitChars(info);
            return this._map;
        }

        private void InitChars(MapInitInfo info)
        {
            var loader = new CharLoader(this.MapHolder);
            loader.Init(this._map, info);
        }

        private void InitParties(MapInitInfo info)
        {
            var loader = new PartyLoader();
            loader.Init(this._map, info);
        }

        private void InitTiles(MapInitInfo info)
        {
            var loader = new TileLoader();
            loader.InitTiles(this._map, info, BackgroundTiles);
            loader.InitMapDeco(this._map, info);
        }
    }
}

