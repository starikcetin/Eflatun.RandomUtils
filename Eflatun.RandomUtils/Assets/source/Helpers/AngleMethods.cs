using UnityEngine;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Methods related to angles for <see cref="BetterRandom"/> class.
    /// </summary>
    public class AngleMethods
    {
        private readonly BetterRandom _parent;

        public AngleMethods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Returns a random angle in radians, range of 0 (inclusive) - 2*Pi (exclusive).
        /// </summary>
        public float NextInRadians()
        {
            return 2 * Mathf.PI * _parent.Float.Next01();
        }

        /// <summary>
        /// Returns a random angle in degrees, range of 0 (inclusive) - 360 (exclusive).
        /// </summary>
        public float NextInDegrees()
        {
            return NextInRadians() * Mathf.Rad2Deg;
        }
    }
}