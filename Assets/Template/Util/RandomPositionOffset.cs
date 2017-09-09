using System;
using UnityEngine;

namespace Assets.Template.Util
{
    public class RandomPositionOffset
    {
        public static Vector3 RandomOffset(Vector3 pos, double range)
        {
            var x = RNG.Instance.GetRandomBetweenRange(range);
            var y = RNG.Instance.GetRandomBetweenRange(range);
            var offset = pos;
            offset.x += Convert.ToSingle(x);
            offset.y += Convert.ToSingle(y);
            return offset;
        }

        public static Vector3 Random3DOffset(Vector3 pos, double range)
        {
            var x = RNG.Instance.GetRandomBetweenRange(range);
            var y = RNG.Instance.GetRandomBetweenRange(range);
            var z = RNG.Instance.GetRandomBetweenRange(range);
            var offset = pos;
            offset.x += Convert.ToSingle(x);
            offset.y += Convert.ToSingle(y);
            offset.z += Convert.ToSingle(z);
            return offset;
        }
    }
}
