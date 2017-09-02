using System.Collections.Generic;

namespace Assets.Template.Hex
{
    public interface IHex<T>
    {
        List<T> GetAdjacent();
        List<T> GetAoETiles(int dist);
        T GetRandomNearbyTile(int probes);
        List<T> GetRaycastTiles(T t, int dist);
        List<T> GetRayTilesViaDistN(T t, int dist);
        List<T> GetRayTilesViaDistNE(T t, int dist);
        List<T> GetRayTilesViaDistSE(T t, int dist);
        List<T> GetRayTilesViaDistS(T t, int dist);
        List<T> GetRayTilesViaDistSW(T t, int dist);
        List<T> GetRayTilesViaDistNW(T t, int dist);
        T GetN();
        T GetNE();
        T GetSE();
        T GetS();
        T GetSW();
        T GetNW();
        bool IsTileN(T target, int dist);
        bool IsTileNE(T target, int dist);
        bool IsTileSE(T target, int dist);
        bool IsTileS(T target, int dist);
        bool IsTileSW(T target, int dist);
        bool IsTileNW(T target, int dist);
    }
}
