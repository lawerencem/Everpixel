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
            this._loader.LoadFiles();
            this._mapLoader = new MapLoader();
            var initInfo = new MapInitInfo();
            initInfo.Biome = EBiome.Grassland;
            var lParty = new PartyBuildParams();
            var rParty = new PartyBuildParams();

            lParty.AIControlled = true;
            lParty.Culture = ECulture.Orcish;
            lParty.Difficulty = 2000;
            lParty.Name = "Ween Herders";

            rParty.AIControlled = true;
            rParty.Culture = ECulture.Goblin;
            rParty.Difficulty = 2000;
            rParty.Name = "Battle Party";

            initInfo.LParties.Add(lParty);
            initInfo.RParties.Add(rParty);
            initInfo.Cols = 20;
            initInfo.Rows = 20;
            var map = this._mapLoader.GetCombatMap(initInfo);
            CombatManager.Instance.Init(map);
        }
    }
}