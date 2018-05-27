using System;
using Assets.Model.Map.Deco;
using Assets.Template.Hex;
using Assets.View.Map.Deco;
using UnityEngine;
using Assets.Controller.Map.Tile;

namespace Assets.Controller.Map.Environment
{
    public class CDeco : IHexOccupant
    {
        private GameObject _gameHandle;
        private MDeco _model;
        private VDeco _view;

        public GameObject GameHandle { get { return this._gameHandle; } }
        public MDeco Model { get { return this._model; } }
        public VDeco View { get { return this._view; } }

        public void SetGameHandle(GameObject h) { this._gameHandle = h; }
        public void SetModel(MDeco m) { this._model = m; }
        public void SetView(VDeco v) { this._view = v; }

        public void SetCurrentHex(IHex hex)
        {
            this._model.SetCurrentHex(hex);
        }
    }
}
