using Assets.Controller.GUI;
using Assets.Controller.Manager;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Combat.Loader;
using Assets.Model.Biome.Enum;
using Assets.Template.Other;
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

        }

        private void InitManagers()
        {
            this._loader = LoaderManager.Instance;
            this._mapLoader = new MapLoader();
            var gui = new GUILoader();
            gui.InitCombatGUI();
            var initInfo = new MapInitInfo();
            initInfo.Biome = EBiome.Grassland;
            initInfo.LParties.Add(new Pair<string, int>("Lizardman War Party", 20));
            initInfo.RParties.Add(new Pair<string, int>("Trolls", 20));
            initInfo.Cols = 12;
            initInfo.DecoCount = 5;
            initInfo.Rows = 12;
            var map = this._mapLoader.GetCombatMap(initInfo);
            CombatManager.Instance.Init(map);
        }
    }
}