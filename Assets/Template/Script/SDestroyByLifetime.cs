using UnityEngine;

namespace Template.Script
{
    public class SDestroyByLifetime : MonoBehaviour
    {
        public float lifetime;

        public void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}
