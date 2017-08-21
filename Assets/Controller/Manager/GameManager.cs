using Assets.Controller.GUI;
using Assets.Controller.Manager;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Combat.Loader;
using Assets.Model.Biome.Enum;
using Template.Other;
using UnityEngine;

namespace Assets.Controller.Managers
{
    public class GameManager : MonoBehaviour
    {
        private LoaderManager _loader;
        private MapLoader _mapLoader;

        public static GameManager Instance = null;

        void Awake()
        {
            if (Instance == null)
                Instance = this;

            else if (Instance != this)
                Destroy(gameObject);

            InitGame();
        }

        void InitGame()
        {
            this.InitManagers();
        }

        void Update()
        {
            //CombatEventManager.Instance.Update();
        }

        private void InitManagers()
        {
            this._loader = LoaderManager.Instance;
            this._mapLoader = new MapLoader();
            var initInfo = new MapInitInfo();
            initInfo.Biome = EBiome.Grassland;
            initInfo.LParties.Add(new Pair<string, int>("Orcs", 20));
            initInfo.RParties.Add(new Pair<string, int>("Vikings", 20));
            initInfo.Cols = 12;
            initInfo.DecoCount = 5;
            initInfo.Rows = 12;
            var map = this._mapLoader.GetCombatMap(initInfo);
            CombatManager.Instance.Init(map);
            var gui = new GUILoader();
            gui.InitCombatGUI();
        }
    }
}