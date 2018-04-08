using Assets.Controller.Character;
using Assets.Controller.Map.Tile;
using Assets.Model.Equipment.Enum;
using Assets.Model.Map.Tile;
using Assets.Template.Hex;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AoELogic
    {
        public List<CTile> GetAdjacentTiles(CChar c)
        {
            return c.Tile.GetAdjacent();
        }

        public  List<CTile> GetArcCastTiles(AbilityArgs arg)
        {
            var list = new List<CTile>();
            var tile = arg.Source.Tile.Model;
            var hexes = tile.GetArcTiles(arg.Target.Model);
            foreach (var hex in hexes)
                list.Add(hex.Controller);
            return list;
        }

        public List<CTile> GetAoETiles(AbilityArgs arg, int aoe)
        {
            var list = new List<CTile>();
            var tile = arg.Target.Model;
            var hexes = tile.GetAoETiles(aoe);
            foreach (var hex in hexes) { list.Add(hex.Controller); }
            return list;
        }

        public List<CTile> GetTargetableRaycastTiles(AbilityArgs arg)
        {
            var list = new List<CTile>();
            var hexes = new List<IHex>();
            var source = arg.Source.Tile.Model;
            hexes.AddRange(source.GetRayTilesViaDistN(arg.Range));
            hexes.AddRange(source.GetRayTilesViaDistNE(arg.Range));
            hexes.AddRange(source.GetRayTilesViaDistSE(arg.Range));
            hexes.AddRange(source.GetRayTilesViaDistS(arg.Range));
            hexes.AddRange(source.GetRayTilesViaDistSW(arg.Range));
            hexes.AddRange(source.GetRayTilesViaDistNW(arg.Range));
            foreach (var hex in hexes)
            {
                var tile = hex as MTile;
                list.Add(tile.Controller);
            }
            return list;
        }

        public List<CTile> GetRaycastTilesViaSourceAndTarget(AbilityArgs arg)
        {
            var list = new List<CTile>();
            var hexes = new List<IHex>();
            var s = arg.Source.Tile.Model;
            var tgt = arg.Target.Model;
            if (s.IsTileN(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistN(arg.Range);
            else if (s.IsTileNE(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistNE(arg.Range);
            else if (s.IsTileSE(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistSE(arg.Range);
            else if (s.IsTileS(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistS(arg.Range);
            else if (s.IsTileSW(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistSW(arg.Range);
            else if (s.IsTileNW(tgt, arg.Range))
                hexes = s.GetRayTilesViaDistNW(arg.Range);
            foreach (var hex in hexes)
            {
                var tile = hex as MTile;
                list.Add(tile.Controller);
            }
            return list;
        }

        public List<CTile> GetRingCastTiles(AbilityArgs arg)
        {
            var list = new List<CTile>();
            var t = arg.Source.Tile;
            var tiles = t.Model.GetAoETiles(arg.AoE);
            foreach (var tile in tiles) { list.Add(tile.Controller); }
            list.Remove(arg.Source.Tile);
            return list;
        }

        public List<CTile> GetPotentialTargets(AbilityArgs arg)
        {
            int dist = arg.Range;
            if (arg.LWeapon)
            {
                if (arg.Source.Proxy.GetLWeapon() != null)
                    dist += (int)arg.Source.Proxy.GetLWeapon().GetStat(EWeaponStat.Range_Mod);
            }
            else
            {
                if (arg.Source.Proxy.GetRWeapon() != null)
                    dist += (int)arg.Source.Proxy.GetRWeapon().GetStat(EWeaponStat.Range_Mod);
            }
            var hexTiles = arg.Source.Tile.Model.GetAoETiles(dist);
            var tileControllers = new List<CTile>();
            foreach (var hex in hexTiles)
            {
                tileControllers.Add(hex.Controller);
            }
            return tileControllers;
        }
    }
}
