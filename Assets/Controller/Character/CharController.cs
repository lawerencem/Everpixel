using Assets.Controller.Map.Tile;
using Assets.Model.Character;
using Assets.View.Character;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Controller.Character
{
    public class CharController
    {
        private GameObject _handle;
        private MChar _model;
        private VChar _view;
        private TileController _tile;

        public GameObject Handle { get { return this._handle; } }
        public MChar Model { get { return this._model; } }
        public VChar View { get { return this._view; } }
        public TileController Tile { get { return this._tile; } }
        
        public bool KillFXProcessed { get; set; }   // TODO: Don't like this here...

        public void SetModel(MChar m) { this._model = m; }
        public void SetView(VChar v) { this._view = v; }

        public List<GameObject> Particles { get; set; }
        public Dictionary<string, GameObject> SpriteHandlerDict = new Dictionary<string, GameObject>();

        public CharController()
        {
            this._handle = new GameObject("Character");
        }
    }
}

