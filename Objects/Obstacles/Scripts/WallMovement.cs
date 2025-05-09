using Godot;
using System;

public partial class WallMovement : RigidBody2D
{
	public void LaunchWall(Vector2 position, Vector2 direction, float speed)
	{
		this.Freeze = false;

		this.GlobalPosition = position;

		Vector2 velocity = new(direction.X * speed, 0);
		this.LinearVelocity = velocity;

		// GD.Print("\nWall launched at position: " + position+
		// 	"\nWall direction: " + direction +
		// 	"\nWall speed: " + speed +
		// 	"\nWall velocity: " + velocity +
		// 	"\n \n////////////////////////////");
	}
}
