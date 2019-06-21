using System;

namespace func_rocket
{
	public class ControlTask
	{
		public static Turn ControlRocket(Rocket rocket, Vector target)
		{
            var distance = target - rocket.Location;
            var diffWithVelocity = distance.Angle - rocket.Velocity.Angle;
            var diffWithDirection = distance.Angle - rocket.Direction;
            var currentAngle = Math.Abs(diffWithVelocity) < 0.5 || Math.Abs(diffWithDirection) < 0.5
                ? (diffWithDirection + diffWithVelocity) / 2 
                : diffWithDirection;

            return currentAngle < 0 ? Turn.Left : Turn.Right;
        }
	}
}