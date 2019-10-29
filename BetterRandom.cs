using System.Collections.Generic;
using Eflatun.RandomUtils.Helpers;

namespace Eflatun.RandomUtils
{
    /// <summary>
    /// A better Random class.
    /// </summary>
    public class BetterRandom
    {
        /// <summary>
        /// The internal <see cref="System.Random"/> class (in <see cref="System"/> namespace) that this instance uses.
        /// </summary>
        public System.Random Random { get; private set; }

        public int Seed { get; private set; }

        public IntMethods Int { get; }
        public DoubleMethods Double { get; }
        public FloatMethods Float { get; }
        public Vector2Methods Vector2 { get; }
        public Vector3Methods Vector3 { get; }
        public AngleMethods Angle { get; }

        /// <summary>
        /// Initializes a new BetterRandom with <see cref="System.Environment.TickCount"/> as the seed.
        /// </summary>
        public BetterRandom() : this(System.Environment.TickCount)
        {
        }

        /// <summary>
        /// Initializes a new BetterRandom with the <see cref="seed"/>.
        /// </summary>
        public BetterRandom(int seed)
        {
            Initialize(seed);
            Int = new IntMethods(this);
            Double = new DoubleMethods(this);
            Float = new FloatMethods(this);
            Vector2 = new Vector2Methods(this);
            Vector3 = new Vector3Methods(this);
            Angle = new AngleMethods(this);
        }

        public void Reset()
        {
            Initialize(Seed);
        }

        public void ChangeSeed(int newSeed)
        {
            Initialize(newSeed);
        }

        private void Initialize(int seed)
        {
            Seed = seed;
            Random = new System.Random(seed);
        }

        /// <summary>
        /// Returns +1 or -1 randomly.
        /// </summary>
        public int RandomSign()
        {
            return Int.FromRange(0, 2) * 2 - 1; //0*2 -1 = -1 | 1*2 -1 = 1
        }

        /// <summary>
        /// Returns a random item from the list.
        /// </summary>
        public T RandomItem<T>(IList<T> list)
        {
            int randomIndex = Int.FromRange(0, list.Count);
            return list[randomIndex];
        }
    }
}
