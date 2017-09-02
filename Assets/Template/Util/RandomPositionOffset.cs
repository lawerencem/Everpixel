using System;
using UnityEngine;

namespace Assets.Template.Util
{
    public class RandomPositionOffset
    {
        public static Vector3 RandomOffset(Vector3 pos, float min, float max)
        {
            var x = RNG.Instance.GetRandomBetweenRange(min, max);
            x *= (int)RNG.Instance.RandomNegOrPos();
            var y = RNG.Instance.GetRandomBetweenRange(min, max);
            y *= RNG.Instance.RandomNegOrPos();
            var offset = pos;
            offset.x += Convert.ToSingle(x);
            offset.y += Convert.ToSingle(y);
            return offset;
        }
    }
}
