﻿using UnityEngine;

namespace Assets.Controller.GUI.Combat
{
    public class CombatGUIParams
    {
        public static readonly float DEFAULT_EPSILON = 0.05f;
        public static readonly float DEFAULT_OFFSET = 0.15f;

        public static readonly string EFFECTS_PATH = "Effects/";
        public static readonly string MAP_GUI_LAYER = "BackgroundTileGUI";
        public static readonly string PARTICLES_EXTENSION = "Particles";
        public static readonly string PARTICLES_LAYER = "Particles";
        public static readonly string UI_LAYER = "UI";

        public static readonly float FLOAT_OFFSET = 0.20f;

        public static readonly Color BLUE = new Color(0, 0, 255, 150);
        public static readonly Color GREEN = new Color(0, 255, 0, 150);
        public static readonly Color RED = new Color(255, 0, 0, 150);
        public static readonly Color WHITE = new Color(255, 255, 255, 255);

        public static readonly string CHOP_FATALITY = "ChopFatality";
        public static readonly string CRUSH_FATALITY = "CrushFatality";
        public static readonly string FIGHTING_FATALITY = "FightingFatality";
        public static readonly string SLASH_FATALITY = "SlashFatality";

        public static readonly double FATALITY_CHANCE = 1.0;

        // Attack params
        public static readonly float ATTACK_LERP = 0.85f;
        public static readonly float ATTACK_SPEED = 8f;
        public static readonly float BULLET_SPEED = 5f;
        public static readonly float DODGE_LERP = 0.35f;
        public static readonly float DODGE_SPEED = 6f;
        public static readonly float FLINCH_DIST = 0.08f;
        public static readonly float FLINCH_SPEED = 8f;
        public static readonly float PARRY_OFFSET = 0.10f;
        public static readonly float PARRY_SPEED = 8f;
        

        // Bullet Param Stuff
        public static readonly float X_CORRECTION = 0.165f;
        public static readonly float BULLET_OFFSET = 0.075f;
        public static readonly float MAX_ROTATION = 25f;

        // Particle Stuff
        public static readonly float PARTICLE_DUR = 4f;

        // Singe FX Stuff
        public static readonly float SINGLE_FX_DUR = 3f;
        public static readonly float SINGLE_FX_OFFSET = 0.35f;
    }
}
