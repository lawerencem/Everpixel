﻿namespace Assets.View.Fatality
{
    public class FatalityParams
    {
        public static readonly float DEFAULT_EPSILON = 0.005f;
        public static readonly float DEFAULT_DUR = 5f;
        public static readonly float DEFAULT_X_SHAKE_DIST = 0.025f;
        public static readonly float DEFAULT_X_SHAKE_SPEED = 2f;
        public static readonly float PER_FRAME = 0.0025f;
        public static readonly float PER_FRAME_DIST = 0.075f;

        public static readonly float FATALITY_ATTACK_SPEED = 0.8f;
        public static readonly float FATALITY_BULLET_LERP = 0.15f;
        public static readonly float FATALITY_BULLET_SPEED = 1.5f;
        public static readonly float FATALITY_MELEE_LERP = 0.30f;

        // Fighting Fatality
        public static readonly float FIGHTING_SPEED = 3f;

        // Fire Fatality
        public static readonly float FIRE_INTERVAL = 0.1f;
        public static readonly float FIRE_SPEED = 8f;

        // Slash Fatality
        public static readonly float SLASH_HEAD_OFFSET = 0.5f;
        public static readonly float SLASH_RAYCAST_SPEED = 1.8f;
        public static readonly float SLASH_ROTATION_SPEED = 3f;

        // Weenbow Fatality
        public static readonly float WEEN_INTERVAL = 0.0005f;
        public static readonly float WEEN_OFFSET = 20f;
        public static readonly float WEEN_SPEED = 8f;

        public static readonly float ZOOM_BULLET_HANG = 0.25f;
        public static readonly float ZOOM_FOV = 12f;
        public static readonly float ZOOM_Y_OFFSET = 0.5f;
        public static readonly float ZOOM_MELEE_HANG = 0.8f;
        public static readonly float ZOOM_SPEED = 150f;
    }
}
