using Controller.Map;
using Model.Characters;
using Model.Map;
using System.Collections.Generic;
using UnityEngine;
using View.Builders;
using View.Characters;

namespace Controller.Characters
{
    public class GenericCharacterController : MonoBehaviour
    {
        private GenericCharacter _model;
        private CharacterView _view;

        public TileController CurrentTile { get; set; }
        public GameObject Handle { get; set; }
        public GenericCharacter Model { get { return this._model; } }
        public List<GameObject> SpriteHandlers = new List<GameObject>();
        public CharacterView View { get { return this._view; } }

        private void BuildModel(CharacterParams p) { this._model = CharacterFactory.Instance.CreateNewObject(p); }
        private void BuildView(CharacterParams p) { var b = new CharacterViewBuilder(); this._view = b.Build(p); }

        public void SetModel(CharacterParams p) { this.BuildModel(p); this.BuildView(p); }
        public void SetView(CharacterView v, CharacterParams p) { this._view = v; this.BuildModel(p); }

        public void Init(GameObject o)
        {
            this.Handle = o;
        }
    }
}
