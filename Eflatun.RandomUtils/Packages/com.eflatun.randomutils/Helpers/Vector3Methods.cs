using UnityEngine;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Vector3 methods for <see cref="BetterRandom"/> class.
    /// </summary>
    public class Vector3Methods
    {
        private readonly BetterRandom _parent;

        public Vector3Methods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// <para> NonNegative range: (0, 0, 0) (inclusive) -> (<see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>) (exclusive) </para>
        /// <para> All range: (<see cref="float.MinValue"/>, <see cref="float.MinValue"/>, <see cref="float.MinValue"/>) (inclusive) -> (<see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>) (exclusive) </para>
        /// </summary>
        public Vector3 Next(RandomRange rangeType = RandomRange.NonNegative)
        {
            var x = _parent.Float.NextUnl(rangeType);
            var y = _parent.Float.NextUnl(rangeType);
            var z = _parent.Float.NextUnl(rangeType);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public Vector3 FromRange(Vector3 min, Vector3 max)
        {
            var x = _parent.Float.FromRange(min.x, max.x);
            var y = _parent.Float.FromRange(min.y, max.y);
            var z = _parent.Float.FromRange(min.z, max.z);

            return new Vector3(x, y, z);
        }
    }
}
