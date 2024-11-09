using System.Collections;
using UnityEngine;

namespace Game.Helper
{
    public static class TransformExtension
    {
        public static IEnumerator RotateTo(this Transform transform, Quaternion from, Quaternion to, float duration)
        {
            var elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                transform.rotation = Quaternion.Slerp(from, to, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.rotation = to;
        }
    }
}