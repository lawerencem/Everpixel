using UnityEngine;

namespace Assets.View
{
    public class ViewParams
    {
        // Char View Params
        public static readonly float MOUNT_X_OFFSET = 0.05f;
        public static readonly float MOUNT_Y_OFFSET = 0.15f;
        public static readonly float HELM_OFFSET = 0.15f;
        public static readonly float WEAPON_OFFSET = 0.09f;

        // Map Params
        public static readonly Vector3 MAP_CENTER = new Vector3(-8, 5, 0);
        public static readonly float OFFSET = 0.63f;

        // Script Params
        public static readonly float BOB_PER_FRAME = 0.0025f;
        public static readonly float BOB_PER_FRAME_DIST = 0.075f;

        // Tile Params
        public static readonly float TILE_DECO_ALPHA = 0.75f;
        public static readonly Vector2 TILE_COLLIDER_SIZE = new Vector2(0.45f, 0.45f);
    }
}