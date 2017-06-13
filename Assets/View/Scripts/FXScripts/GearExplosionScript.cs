using Controller.Characters;
using Generics.Scripts;
using Generics.Utilities;
using UnityEngine;

namespace View.Scripts
{
    public class GearExplosionScript : MonoBehaviour
    {
        private Callback _callback;
        public delegate void Callback();

        private GameObject _toMove;

        public virtual void Update()
        {

        }

        public virtual void Init(GameObject source, GenericCharacterController character, Callback callback = null)
        {
            this._toMove = source;
            this._callback = callback;
            var tile = character.CurrentTile;
            var toGo = character.CurrentTile.Model.GetRandomNearbyTile(3);
            var move = source.AddComponent<RaycastMove>();
            var roll = RNG.Instance.NextDouble();
            move.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
            move.Init(source, toGo.Center, 3f);
        }
    }
}
