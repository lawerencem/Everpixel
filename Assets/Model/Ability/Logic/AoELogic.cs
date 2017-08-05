using Assets.Controller.Managers;
using Controller.Characters;
using Controller.Map;
using Model.Events.Combat;
using Model.Map;
using System.Collections.Generic;

namespace Assets.Model.Ability.Logic
{
    public class AoELogic
    {
        public List<TileController> GetAdjacentTiles(GenericCharacterController c)
        {
            var list = new List<TileController>();
            foreach (var neighbor in c.CurrentTile.Adjacent)
                list.Add(neighbor);
            return list;
        }

        public List<TileController> GetAoETiles(TileController source, TileController target, int range)
        {
            var list = new List<TileController>();
            list.Add(source);
            return list;
        }

        public List<TileController> GetRaycastTiles(TileController source, TileController target, int range)
        {
            var list = new List<TileController>();
            HexTile initTile;
            var s = source.Model;
            var t = target.Model;
            if (s.IsHexN(t, range))
                initTile = s.GetN();
            else if (s.IsHexNE(t, range))
                initTile = s.GetNE();
            else if (s.IsHexSE(t, range))
                initTile = s.GetSE();
            else if (s.IsHexS(t, range))
                initTile = s.GetS();
            else if (s.IsHexSW(t, range))
                initTile = s.GetSW();
            else
                initTile = s.GetNW();
            var hexes = initTile.GetRaycastTiles(t, range);
            foreach (var hex in hexes)
                list.Add(hex.Parent);
            return list;
        }

        public List<TileController> GetStandardAttackTiles(AttackSelectedEvent e, GenericCharacterController c, CombatManager m)
        {
            int distMod = 0;
            distMod += AbilityTable.Instance.Table[e.AttackType].Params.Range;

            if (e.RWeapon)
            {
                if (c.Model.RWeapon != null)
                    distMod += (int)c.Model.RWeapon.RangeMod;
            }
            else
            {
                if (c.Model.LWeapon != null)
                    distMod += (int)c.Model.LWeapon.RangeMod;
            }
            var hexTiles = m.Map.GetAoETiles(c.CurrentTile.Model, distMod);
            var tileControllers = new List<TileController>();
            foreach (var hex in hexTiles)
            {
                tileControllers.Add(hex.Parent);
                TileControllerFlags.SetAwaitingActionFlagTrue(hex.Parent.Flags);
            }
            return tileControllers;
        }
    }
}
