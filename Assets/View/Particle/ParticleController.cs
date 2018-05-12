using Assets.Template.Other;
using UnityEngine;

namespace Assets.View.Particle
{
    public class ParticleController
    {
        public void AttachParticle(GameObject tgt, GameObject particles)
        {
            particles.transform.SetParent(tgt.transform);
            particles.transform.position = tgt.transform.position;
        }

        public GameObject CreateParticle(string path)
        {  
            var prefab = Resources.Load(path);
            var particles = GameObject.Instantiate(prefab) as GameObject;
            var renderer = particles.GetComponent<Renderer>();
            renderer.sortingLayerName = SortingLayers.PARTICLES;
            return particles;
        }

        public void RotateParticle(GameObject particle, float qty)
        {
            particle.transform.Rotate(new Vector3(0, 0, qty));
        }
    }
}
