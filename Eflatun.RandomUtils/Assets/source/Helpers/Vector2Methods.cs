using System;
using UnityEngine;

namespace Eflatun.RandomUtils.Helpers
{
    /// <summary>
    /// Vector2 methods for <see cref="BetterRandom"/> class.
    /// </summary>
    public class Vector2Methods
    {
        private readonly BetterRandom _parent;

        public Vector2Methods(BetterRandom parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// <para> NonNegative range: (0, 0) (inclusive) -> (<see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>) (exclusive) </para>
        /// <para> All range: (<see cref="float.MinValue"/>, <see cref="float.MinValue"/>) (inclusive) -> (<see cref="float.MaxValue"/>, <see cref="float.MaxValue"/>) (exclusive) </para>
        /// </summary>
        public Vector2 Next(RandomRange rangeType = RandomRange.NonNegative)
        {
            var x = _parent.Float.NextUnl(rangeType);
            var y = _parent.Float.NextUnl(rangeType);

            return new Vector2(x, y);
        }

        /// <summary>
        /// <para> Range: <paramref name="min"/> (inclusive) -> <paramref name="max"/> (exclusive) </para>
        /// </summary>
        public Vector2 FromRange(Vector2 min, Vector2 max)
        {
            var x = _parent.Float.FromRange(min.x, max.x);
            var y = _parent.Float.FromRange(min.y, max.y);

            return new Vector2(x, y);
        }

        /// <summary>
        /// <para>Returns a random point that is exactly on the circumference of the unit circle.</para>
        /// <para>Lenght: 1, Rotation: 0 (inclusive) - 360 (exclusive)</para>
        /// </summary>
        /// <remarks> Unit Circle: A circle whose center is on the origin, with a radius of 1. </remarks>
        public Vector2 OnUnitCircle()
        {
            var rndAngle = _parent.Angle.NextInRadians();

            return Geometry2D.PolarToCartesian(1, rndAngle);
        }

        /// <summary>
        /// <para>Returns a random point that is exactly on the circumference of the circle whose radius is <paramref name="radius"/>.</para>
        /// <para>Lenght: <paramref name="radius"/>, Rotation: 0 (inclusive) - 360 (exclusive)</para>
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        public Vector2 OnCircle(float radius)
        {
            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException("radius", radius, "Radius of a circle cannot be negative.");
            }

            return OnUnitCircle() * radius;
        }

        /// <summary>
        /// <para>Returns a random point that is inside the unit circle.</para>
        /// <para>Lenght: 0 (inclusive) - 1 (exclusive), Rotation: 0 (inclusive) - 360 (exclusive)</para>
        /// </summary>
        /// <remarks> Unit Circle: A circle whose center is on the origin, with a radius of 1. </remarks>
        public Vector2 InUnitCircle()
        {
            var rndLength = Mathf.Sqrt(_parent.Float.Next01());
            var rndAngle = _parent.Angle.NextInRadians();

            return Geometry2D.PolarToCartesian(rndLength, rndAngle);
        }

        /// <summary>
        /// <para>Returns a random point that is inside the circle whose radius is <paramref name="radius"/>.</para>
        /// <para>Lenght: 0 (inclusive) - <paramref name="radius"/> (exclusive), Rotation: 0 (inclusive) - 360 (exclusive)</para>
        /// </summary>
        /// <param name="radius">Radius of the circle.</param>
        public Vector2 InCircle(float radius)
        {
            if (radius < 0)
            {
                throw new ArgumentOutOfRangeException("radius", radius, "Radius of a circle cannot be negative.");
            }

            return InUnitCircle() * radius;
        }

        /// <summary>
        /// <para>Returns a random point that is inside the area in between the circles with radiuses of <paramref name="innerRadius"/> and <paramref name="outerRadius"/>.</para>
        /// <para>Lenght: <paramref name="innerRadius"/> (inclusive) - <paramref name = "outerRadius" /> (exclusive), Rotation: 0 (inclusive) - 360 (exclusive)</para>
        /// </summary>
        /// <param name="innerRadius">The radius of inner circle.</param>
        /// <param name="outerRadius">The radius of outer circle.</param>
        public Vector2 InBetweenCircles(float innerRadius, float outerRadius)
        {
            // ----
            // Source: http://stackoverflow.com/questions/13064912/generate-a-uniformly-random-point-within-an-annulus-ring#comment17761088_13067452
            // ----

            var u = _parent.Float.FromRange(innerRadius, outerRadius);
            var v = _parent.Float.FromRange(0, innerRadius + outerRadius);

            float length = v < u ? u : innerRadius + outerRadius - u;
            var rndAngle = _parent.Angle.NextInRadians();

            return Geometry2D.PolarToCartesian(length, rndAngle);
        }
    }
}
