using System;
using System.Collections.Generic;

namespace func_rocket
{
	public class LevelsTask
	{
		private static readonly Physics standardPhysics = new Physics();
        private static Vector target = new Vector(600, 200);
        private static Rocket rocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);

        public static IEnumerable<Level> CreateLevels()
        {
            yield return new Level("Zero", rocket, target, (size, v) => Vector.Zero, standardPhysics);
            yield return new Level("Heavy", rocket, target, (size, v) => new Vector(0, 0.9), standardPhysics);
            yield return new Level("Up", rocket,
                new Vector(700, 500),
                (size, v) => new Vector(0, -300.0 / (size.Height - v.Y + 300)), standardPhysics);
            yield return new Level("WhiteHole", rocket, target, (size, v) =>
                GetGravityVector(getWhiteHoleDistance(v), -140), standardPhysics);
            yield return new Level("BlackHole", rocket, target, (size, v) =>
                GetGravityVector(getBlackHoleDistance(v), 300), standardPhysics);
            yield return new Level("BlackAndWhite", rocket, target,
                (size, v) => 
                    (GetGravityVector(getWhiteHoleDistance(v), -140) + 
                     GetGravityVector(getBlackHoleDistance(v), 300)) / 2.0, 
                standardPhysics);
        }

        private static Func<Vector, Vector> getWhiteHoleDistance = v => target - v;
        private static Func<Vector, Vector> getBlackHoleDistance = v => (target + rocket.Location) / 2.0 - v;

        private static Vector GetGravityVector(Vector distance, double constant)
        {
            var length = distance.Length;
            return distance.Normalize() * constant * length / (length * length + 1);
        }
    }
}