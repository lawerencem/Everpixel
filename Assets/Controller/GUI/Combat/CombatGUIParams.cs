﻿using UnityEngine;

namespace Assets.Controller.GUI.Combat
{
    public class CombatGUIParams
    {
        public static readonly float DEFAULT_EPSILON = 0.05f;
        public static readonly float DEFAULT_OFFSET = 0.15f;

        public static readonly string EFFECTS_PATH = "Effects/";
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
        public static readonly string FIGHTING_FATALITY_EXPLOSION = "FightingFatalityExplosion";
        public static readonly string GREAT_STRIKE_BONE_FATALITY = "GreatStrikeBoneFatality";
        public static readonly string GREAT_STRIKE_BLOOD_FATALITY = "GreatStrikeBloodFatality";
        public static readonly string SCATTER_BONE_FATALITY = "ScatterBoneFatality";
        public static readonly string SCATTER_EXPLOSION_FATALITY = "ScatterExplosionFatality";
        public static readonly string SLASH_FATALITY = "SlashFatality";
        public static readonly string SLIME_FATALITY = "SlimeFatality";
        public static readonly string WEEN_BURSTER_FATALITY = "WeenBursterFatality";


        public static readonly double FATALITY_CHANCE = 0.25;

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
