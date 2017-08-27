using UnityEngine;

namespace Assets.Controller.GUI.Combat
{
    public class CombatGUIParams
    {
        public static readonly string ARMOR = "ArmorTextTag";
        public static readonly string AP = "APTextTag";
        public static readonly string EFFECTS_PATH = "Effects/";
        public static readonly string HELM = "HelmTextTag";
        public static readonly string HP = "HPTextTag";
        public static readonly string IMG = "ActingBoxImgTag";
        public static readonly string L_WEAP = "LWeaponTextTag";
        public static readonly string MAP_GUI_LAYER = "BackgroundTileGUI";
        public static readonly string MORALE = "MoraleTextTag";
        public static readonly string NAME = "ActingBoxNameTag";
        public static readonly string PARTICLES_EXTENSION = "Particles";
        public static readonly string PARTICLES_LAYER = "Particles";
        public static readonly string R_WEAP = "RWeaponTextTag";
        public static readonly string STAM = "StaminaTextTag";
        public static readonly string UI_LAYER = "UI";

        public static readonly float ATTACK_TEXT_OFFSET = 0.20f;
        public static readonly float BLOCK_TEXT_OFFSET = 0.30f;
        public static readonly float CRIT_TEXT_OFFSET = 0.50f;
        public static readonly float DODGE_TEXT_OFFSET = 0.40f;
        public static readonly float DMG_TEXT_OFFSET = 0.20f;
        public static readonly float INJURY_TEXT_OFFSET = 0.60f;
        public static readonly float PARRY_TEXT_OFFSET = 0.50f;
        public static readonly float WEAPON_OFFSET = 0.075f;
        public static readonly float WEAPON_PARRY = 4f;

        public static readonly Color BLUE = new Color(0, 0, 255, 150);
        public static readonly Color GREEN = new Color(0, 255, 0, 150);
        public static readonly Color RED = new Color(255, 0, 0, 150);
        public static readonly Color WHITE = new Color(255, 255, 255, 255);

        public static readonly string CRUSH_FATALITY = "CrushFatality";
        public static readonly string FIGHTING_FATALITY = "FightingFatality";
        public static readonly string SLASH_FATALITY = "SlashFatality";

        public static readonly double FATALITY_CHANCE = 1.0;

        // Attack params
        public static readonly float ATTACK_LERP = 0.85f;
        public static readonly float ATTACK_SPEED = 8f;
        public static readonly float FLINCH_DIST = 0.08f;
        public static readonly float FLINCH_SPEED = 8f;

    }
}
