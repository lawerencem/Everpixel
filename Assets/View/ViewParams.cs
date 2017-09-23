using UnityEngine;

namespace Assets.View
{
    public class ViewParams
    {
        // Bark Params
        public static readonly float BARK_DELAY = 0.5f;
        public static readonly float BARK_DUR = 4.5f;

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

        // Hit Priorities
        public static readonly int DMG_PRIORITY = 10;
        public static readonly int DODGE_PRIORITY = 6;
        public static readonly int PARRY_PRIRORITY = 8;
        public static readonly int HEAL_PRIORITY = 12;

        // Script Params
        public static readonly float BOB_PER_FRAME = 0.0025f;
        public static readonly float BOB_PER_FRAME_DIST = 0.075f;
        public static readonly float MOVE_SPEED = 8f;
        public static readonly float MOVE_EPSILON = 0.02f;

        // Splatter Params
        public static readonly float SPLATTER_VARIANCE = 0.15f;

        // Tile Params
        public static readonly float TILE_DECO_ALPHA = 0.75f;
        public static readonly Vector2 TILE_COLLIDER_SIZE = new Vector2(0.45f, 0.45f);
        
    }
}