using UnityEngine;

namespace Generics.Scripts
{
    public class DestroyByLifetime : MonoBehaviour
    {
        public float lifetime;

        public void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
