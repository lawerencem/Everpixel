using UnityEngine;

namespace Controller.Map
{
    public class CMapGUIControllerParams
    {
        public static readonly string ARMOR = "ArmorTextTag";
        public static readonly string AP = "APTextTag";
        public static readonly string EFFECTS_PATH = "Effects/";
        public static readonly string FIGHTING_FATALITY = "FightingFatality";
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

        public static readonly float ATTACK_TEXT_OFFSET = 0.15f;
        public static readonly float BLOCK_TEXT_OFFSET = 0.35f;
        public static readonly float CRIT_TEXT_OFFSET = 0.45f;
        public static readonly float DODGE_TEXT_OFFSET = 0.40f;
        public static readonly float DMG_TEXT_OFFSET = 0.025f;
        public static readonly float PARRY_TEXT_OFFSET = 0.30f;
        public static readonly float WEAPON_OFFSET = 0.075f;
        public static readonly float WEAPON_PARRY = 4f;

        public static readonly Color RED = new Color(255, 0, 0, 150);
        public static readonly Color WHITE = new Color(255, 255, 255, 255);

        public static readonly double FATALITY_CHANCE = 1.0;
    }
}
