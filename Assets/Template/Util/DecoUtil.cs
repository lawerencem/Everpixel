using UnityEngine;

namespace Assets.Template.Util
{
    public class DecoUtil
    {
        public static void AttachParticles(GameObject deco, GameObject tgt)
        {
            if (deco != null && tgt != null)
            {
                deco.transform.SetParent(tgt.transform);
                deco.transform.position = tgt.transform.position;
            }
        }
    }
}
