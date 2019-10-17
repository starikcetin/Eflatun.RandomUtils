using System;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Integer methods for <see cref="BetterRandom"/> class.
    /// </summary>
    public class IntMethods
    {
        private readonly BetterRandom _parent;

        public IntMethods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <see cref="int.MaxValue"/> (exclusive) </para>
        /// <para> All range: <see cref="int.MinValue"/> (inclusive) -> <see cref="int.MaxValue"/> (exclusive) </para>
        /// </summary>
        public int Next(RandomRange rangeType = RandomRange.NonNegative)
        {
            switch (rangeType)
            {
                case RandomRange.NonNegative:
                    // The System.Random.Next() is NonNegative.
                    return _parent.Random.Next();

                case RandomRange.All:
                    return _parent.Random.Next(int.MinValue, int.MaxValue);

                default:
                    throw new ArgumentOutOfRangeException("rangeType", rangeType, null);
            }
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <see cref="int.MaxValue"/> (exclusive) </para>
        /// </summary>
        public int NextMin(int min)
        {
            // In this method we don't need to specify the sign type since user decides the "lower boundary".
            // Which means, if the "min" is greater than or equal to 0, the return will be NonNegative.
            // Otherwise it can be both negative and positive (or 0), which is what RandomRange.All means.

            return _parent.Random.Next(min, int.MaxValue);
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// <para> All range: <see cref="int.MinValue"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public int NextMax(int max, RandomRange rangeType = RandomRange.NonNegative)
        {
            switch (rangeType)
            {
                case RandomRange.NonNegative:
                    if (max < 0)
                    {
                        throw new ArgumentOutOfRangeException("max", max,
                            "The 'max' parameter cannot be negative when the 'rangeType' parameter is 'NonNegative'.");
                    }

                    // The System.Random.Next() is NonNegative.
                    return _parent.Random.Next(max);

                case RandomRange.All:
                    return _parent.Random.Next(int.MinValue, max);

                default:
                    throw new ArgumentOutOfRangeException("rangeType", rangeType, null);
            }
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public int FromRange(int min, int max)
        {
            // In this method we don't need to specify the sign type since user decides the range directly.

            if (min > max)
            {
                throw new ArgumentException("'min' parameter cannot be greater than 'max' parameter.");
            }

            return _parent.Random.Next(min, max);
        }
    }
}
