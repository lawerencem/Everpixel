using Assets.Controller.GUI;
using Assets.Controller.Manager;
using Assets.Controller.Manager.Combat;
using Assets.Controller.Map.Combat;
using Assets.Controller.Map.Combat.Loader;
using Assets.Model.Biome;
using Assets.Model.Culture;
using Assets.Model.Party.Param;
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
            initInfo.Biome = EBiome.Snow;
            var lParty = new PartyBuildParams();
            var rParty = new PartyBuildParams();

            lParty.Culture = ECulture.Norse;
            lParty.Difficulty = 220;
            lParty.Name = "Raiding Party";

            rParty.Culture = ECulture.Jomonese;
            rParty.Difficulty = 220;
            rParty.Name = "Raiding Party";

            initInfo.LParties.Add(lParty);
            initInfo.RParties.Add(rParty);
            initInfo.Cols = 14;
            initInfo.Rows = 14;
            var map = this._mapLoader.GetCombatMap(initInfo);
            CombatManager.Instance.Init(map);
        }
    }
}