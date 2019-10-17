using System;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Double methods for <see cref="BetterRandom"/> class.
    /// </summary>
    public class DoubleMethods
    {
        private readonly BetterRandom _parent;

        public DoubleMethods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> +1 (exclusive) </para>
        /// <para> All range: -1 (exclusive) -> +1 (exclusive) </para>
        /// </summary>
        public double Next01(RandomRange rangeType = RandomRange.NonNegative)
        {
            switch (rangeType)
            {
                case RandomRange.NonNegative:
                    // The System.Random.NextDouble() is NonNegative.
                    return _parent.Random.NextDouble();

                case RandomRange.All:
                    // We need to randomize the sign, because the System.Random.NextDouble() is NonNegative.
                    return _parent.Random.NextDouble() * 2 - 1;

                default:
                    throw new ArgumentOutOfRangeException("rangeType", rangeType, null);
            }
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> +1 (exclusive) </para>
        /// </summary>
        public double Next01Min(double min)
        {
            // In this method we don't need to specify the sign type since user decides the "lower boundary".
            // Which means, if the "min" is greater than or equal to 0, the return will be NonNegative.
            // Otherwise it can be both negative and positive (or 0), which is what RandomRange.All means.

            if (min <= -1d || min >= 1d)
            {
                throw new ArgumentOutOfRangeException("min", min,
                    "The 'min' parameter for this method must be in range of -1 (exclusive) and +1 (exclusive).");
            }

            return FromRange(min, 1d);
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// <para> All range: -1 (exclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public double Next01Max(double max, RandomRange rangeType = RandomRange.NonNegative)
        {
            switch (rangeType)
            {
                case RandomRange.NonNegative:
                    if (max <= 0d || max > 1d)
                    {
                        throw new ArgumentOutOfRangeException("max", max,
                            "The 'max' parameter for this method must be in range of 0 (exclusive) and +1 (inclusive) when the 'rangeType' parameter is 'NonNegative'.");
                    }

                    return FromRange(0d, max);

                case RandomRange.All:
                    if (max <= -1d || max > 1d)
                    {
                        throw new ArgumentOutOfRangeException("max", max,
                            "The 'max' parameter for this method must be in range of -1 (exclusive) and +1 (inclusive) when the 'rangeType' parameter is 'All'.");
                    }

                    var dist = max + 1; //the distance between max and -1.
                    var syMax = dist / 2; //the new max value that makes symetrical '-syMax 0 +syMax' range.
                    var syRnd = FromRange(0d, syMax) *
                                _parent.RandomSign(); //a random in range '-syMax -> +syMax' (both exclusive).
                    var diff = syMax - max; //difference between original max and syMax.
                    return
                        syRnd - diff; //remove the difference from syRnd so we get the random number in original range.

                default:
                    throw new ArgumentOutOfRangeException("rangeType", rangeType, null);
            }
        }

        /// <summary>
        /// <para> NonNegative range: 0 (inclusive) -> <see cref="double.MaxValue"/> (exclusive) </para>
        /// <para> All range: <see cref="double.MinValue"/> (inclusive) -> <see cref="double.MaxValue"/> (exclusive) </para>
        /// </summary>
        public double NextUnl(RandomRange rangeType = RandomRange.NonNegative)
        {
            double mantissa = (Next01(rangeType) * 2.0d) - 1.0d;
            double exponent = Math.Pow(2.0d, _parent.Int.FromRange(-126, 128));
            return (mantissa * exponent);
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public double FromRange(double min, double max)
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
