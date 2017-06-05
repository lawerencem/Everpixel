﻿using Assets.Controller.Managers;
using Assets.Model.Equipment.Factories;
using Controller.Managers;
using Controller.Managers.Map;
using Model.Characters.XML;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _cameraManager;
    private CombatMapLoader _combatMapManager;
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
        Debug.Log("Initializing");
        this.InitManagers();
    }

    void Update()
    {
        CombatEventManager.Instance.Update();
    }

    private void InitManagers()
    {
        this._cameraManager = new GameObject();
        this._cameraManager.AddComponent<CameraManager>();
        this._combatMapManager = new CombatMapLoader();
        this._loader = LoaderManager.Instance;

        this._combatMapManager.InitMap(Model.Biomes.BiomeEnum.Grassland);
    }
}