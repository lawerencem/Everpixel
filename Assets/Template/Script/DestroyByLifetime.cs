using UnityEngine;

namespace Template.Script
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
