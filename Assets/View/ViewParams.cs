using UnityEngine;

namespace Assets.View
{
    public class ViewParams
    {
        // Bark Params
        public static readonly float BARK_DELAY = 0.25f;
        public static readonly float BARK_DUR = 2.5f;

        // Btn Params
        public static readonly Vector3 WPN_IMG_SCALE = new Vector3(0.2f, 0.2f, 0.2f);

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
        public static readonly float MOVE_SPEED = 8f;
        public static readonly float MOVE_EPSILON = 0.02f;

        // Splatter Params
        public static readonly float SPLATTER_SCALAR = 100;
        public static readonly int SPLATTER_VARIANCE = 15;

        // Tile Params
        public static readonly float TILE_DECO_ALPHA = 0.75f;
        public static readonly Vector2 TILE_COLLIDER_SIZE = new Vector2(0.45f, 0.45f);
        
    }
}