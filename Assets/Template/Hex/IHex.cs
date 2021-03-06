﻿using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public interface IHex
    {
        List<IHex> GetAdjacent();
        List<IHex> GetAoETiles(int dist);
        IHex GetRandomNearbyTile(int probes);
        int GetCost();
        int GetCol();
        int GetHeight();
        int GetRow();
        IHexOccupant GetCurrentOccupant();
        List<IHex> GetRaycastTiles(IHex target, int dist);
        List<IHex> GetRayTilesViaDistN(int dist);
        List<IHex> GetRayTilesViaDistNE(int dist);
        List<IHex> GetRayTilesViaDistSE(int dist);
        List<IHex> GetRayTilesViaDistS(int dist);
        List<IHex> GetRayTilesViaDistSW(int dist);
        List<IHex> GetRayTilesViaDistNW(int dist);
        IHex GetN();
        IHex GetNE();
        IHex GetSE();
        IHex GetS();
        IHex GetSW();
        IHex GetNW();
        bool IsTileN(IHex target, int dist);
        bool IsTileNE(IHex target, int dist);
        bool IsTileSE(IHex target, int dist);
        bool IsTileS(IHex target, int dist);
        bool IsTileSW(IHex target, int dist);
        bool IsTileNW(IHex target, int dist);
    }
}
