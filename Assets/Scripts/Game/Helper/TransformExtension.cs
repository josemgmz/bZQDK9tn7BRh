using System.Collections;
using UnityEngine;

namespace Game.Helper
{
    /// <summary>
    /// Provides extension methods for <see cref="Transform"/>.
    /// </summary>
    public static class TransformExtension
    {
        /// <summary>
        /// Rotates the transform from the specified start rotation to the end rotation over the given duration.
        /// </summary>
        /// <param name="transform">The transform to rotate.</param>
        /// <param name="from">The starting rotation.</param>
        /// <param name="to">The ending rotation.</param>
        /// <param name="duration">The duration of the rotation in seconds.</param>
        /// <returns>An enumerator that performs the rotation over time.</returns>
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