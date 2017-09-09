using Assets.Template.Other;
using Assets.Template.Util;
using UnityEngine;

namespace Assets.Template.Utility
{
    public class RotateTranslateUtil : ASingleton<RotateTranslateUtil>
    {
        public RotateTranslateUtil() { }

        public void RandomRotate(GameObject o)
        {            
            var roll = RNG.Instance.NextDouble();
            o.transform.Rotate(new Vector3(0, 0, (float)(roll * 360)));
        }

        public void RandomRotateAndTranslate(GameObject o, float range)
        {
            this.RandomRotate(o);
            this.RandomTranslate(o, range);
        }

        public void RandomTranslate(GameObject o, float range)
        {
            var x = RNG.Instance.GetRandomBetweenRange(range);
            var y = RNG.Instance.GetRandomBetweenRange(range);
            var position = o.transform.position;
            position.x += x;
            position.y += y;
            o.transform.position = position;
        }
    }
}
