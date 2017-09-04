using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AoELogic
    {
        public List<TileController> GetAdjacentTiles(CharController c)
        {
            //var list = new List<TileController>();
            //foreach (var neighbor in c.CurrentTile.Adjacent)
            //    list.Add(neighbor);
            //return list;
            return null;
        }

        public List<TileController> GetAoETiles(AbilityArgs arg, int aoe)
        {
            var list = new List<TileController>();
            var t = arg.Target.Model;
            var hexes = t.GetAoETiles(aoe);
            foreach (var hex in hexes) { list.Add(hex.Controller); }
            return list;
        }

        public List<TileController> GetRaycastTiles(AbilityArgs arg)
        {
            return null;
            //var list = new List<TileController>();
            //MTile initTile;
            //var s = arg.Source.CurrentTile.Model;
            //var t = arg.Target.Model;
            //var range = arg.Range;
            //if (s.IsHexN(t, range))
            //    initTile = s.GetN();
            //else if (s.IsHexNE(t, range))
            //    initTile = s.GetNE();
            //else if (s.IsHexSE(t, range))
            //    initTile = s.GetSE();
            //else if (s.IsHexS(t, range))
            //    initTile = s.GetS();
            //else if (s.IsHexSW(t, range))
            //    initTile = s.GetSW();
            //else
            //    initTile = s.GetNW();
            //var hexes = initTile.GetRaycastTiles(t, range);
            //foreach (var hex in hexes)
            //    list.Add(hex.Parent);
            //return list;
        }

        public List<TileController> GetPotentialTargets(AbilityArgs arg)
        {
            int dist = arg.Range;
            if (arg.LWeapon)
            {
                if (arg.Source.Proxy.GetLWeapon() != null)
                    dist += (int)arg.Source.Proxy.GetLWeapon().RangeMod;
            }
            else
            {
                if (arg.Source.Proxy.GetRWeapon() != null)
                    dist += (int)arg.Source.Proxy.GetRWeapon().RangeMod;
            }
            var hexTiles = arg.Source.Tile.Model.GetAoETiles(dist);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexTiles)
            {
                tileControllers.Add(hex.Controller);
            }
            return tileControllers;
        }
    }
}
