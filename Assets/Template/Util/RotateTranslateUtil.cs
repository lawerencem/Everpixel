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

        public void RandomRotateAndTranslate(GameObject o, int variance, float scalar)
        {
            this.RandomRotate(o);
            this.RandomTranslate(o, variance, scalar);
        }

        public void RandomTranslate(GameObject o, int variance, float scalar)
        {
            //var x = RNG.Instance.Next(-75, 75) / 100;
            //var y = RNG.Instance.Next(-75, 75) / 100;
            var x = RNG.Instance.Next(-variance, variance) / scalar;
            var y = RNG.Instance.Next(-variance, variance) / scalar;
            var position = o.transform.position;
            position.x += x;
            position.y += y;
            o.transform.position = position;
        }
    }
}
