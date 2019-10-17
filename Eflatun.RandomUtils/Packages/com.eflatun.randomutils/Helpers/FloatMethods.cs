using System;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Float methods for <see cref="BetterRandom"/> class.
    /// </summary>
    public class FloatMethods
    {
        // --- IMPORTANT NOTE ---
        // All implementations of this class is a copy from 'DoubleMethods'.
        // So do the improvments on 'that' class and copy on this one.

        private readonly BetterRandom _parent;

        public FloatMethods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> +1 (exclusive) </para>
        /// <para> All range: -1 (exclusive) -> +1 (exclusive) </para>
        /// </summary>
        public float Next01(RandomRange rangeType = RandomRange.NonNegative)
        {
            return (float) _parent.Double.Next01(rangeType);
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> +1 (exclusive) </para>
        /// </summary>
        public float Next01Min(float min)
        {
            // In this method we don't need to specify the sign type since user decides the "lower boundary".
            // Which means, if the "min" is greater than or equal to 0, the return will be NonNegative.
            // Otherwise it can be both negative and positive (or 0), which is what RandomRange.All means.

            if (min <= -1f || min >= 1f)
            {
                throw new ArgumentOutOfRangeException("min", min,
                    "The 'min' parameter for this method must be in range of -1 (exclusive) and +1 (exclusive).");
            }

            return FromRange(min, 1f);
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// <para> All range: -1 (exclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public float Next01Max(float max, RandomRange rangeType = RandomRange.NonNegative)
        {
            switch (rangeType)
            {
                case RandomRange.NonNegative:
                    if (max <= 0f || max > 1f)
                    {
                        throw new ArgumentOutOfRangeException("max", max,
                            "The 'max' parameter for this method must be in range of 0 (exclusive) and +1 (inclusive) when the 'rangeType' parameter is 'NonNegative'.");
                    }

                    return FromRange(0f, max);

                case RandomRange.All:
                    if (max <= -1f || max > 1f)
                    {
                        throw new ArgumentOutOfRangeException("max", max,
                            "The 'max' parameter for this method must be in range of -1 (exclusive) and +1 (inclusive) when the 'rangeType' parameter is 'All'.");
                    }

                    var dist = max + 1; //the distance between max and -1.
                    var syMax = dist / 2; //the new max value that makes symetrical '-syMax 0 +syMax' range.
                    var syRnd = FromRange(0f, syMax) *
                                _parent.RandomSign(); //a random in range '-syMax -> +syMax' (both exclusive).
                    var diff = syMax - max; //difference between original max and syMax.
                    return
                        syRnd - diff; //remove the difference from syRnd so we get the random number in original range.

                default:
                    throw new ArgumentOutOfRangeException("rangeType", rangeType, null);
            }
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <see cref="float.MaxValue"/> (exclusive) </para>
        /// <para> All range: <see cref="float.MinValue"/> (inclusive) -> <see cref="float.MaxValue"/> (exclusive) </para>
        /// </summary>
        public float NextUnl(RandomRange rangeType = RandomRange.NonNegative)
        {
            return (float) _parent.Double.NextUnl(rangeType);
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public float FromRange(float min, float max)
        {
            // In this method we don't need to specify the sign type since user decides the range directly.

            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min", min,
                    "'min' parameter cannot be greater than 'max' parameter.");
            }

            return Next01() * (max - min) + min;
        }
    }
}
