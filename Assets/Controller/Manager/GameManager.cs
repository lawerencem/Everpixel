using Assets.Controller.Manager;
using UnityEngine;

namespace Assets.Controller.Managers
{
    public class GameManager : MonoBehaviour
    {
        //private CombatMapLoader _combatMapManager;
        private LoaderManager _loader;

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
            //this._combatMapManager = new CombatMapLoader();
            this._loader = LoaderManager.Instance;

            //this._combatMapManager.InitMap(Model.Biomes.BiomeEnum.Grassland);
        }
    }
}