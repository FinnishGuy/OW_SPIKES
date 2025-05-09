using Godot;
using System;

public partial class CollisionDetection : Area2D
{
	[Export] private Area2D wallArea = null;
	[Export] private Node2D player = null;
	Callable onAreaEntered;
	Callable onAreaExited;

	public Boolean isInvincible = false;
	public double invincibilityDuration = 1.0f;
	public double invincibilityTimer = 0.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent<Node2D>();

		onAreaEntered = new Callable(this, nameof(_AreaEntered));
		onAreaExited = new Callable(this, nameof(_AreaExited));
		this.Connect("area_entered", onAreaEntered);
		this.Connect("area_exited", onAreaExited);
	}

	public override void _Process(double delta)
	{
		while (isInvincible)
		{
			invincibilityTimer += delta;

			if (invincibilityTimer >= invincibilityDuration)
			{
				isInvincible = false;
				invincibilityTimer = 0.0f;
			}
		}
	}

	// public void _BodyEntered(RigidBody2D colBody)
	// {
	// 	GD.Print("Collision detected");

	// 	if (colBody.IsInGroup("Obstacle"))
	// 	{
	// 		GD.Print("Collision with obstacle detected");
	// 		isColliding = true;
	// 	}
	// }

	public void _AreaEntered(Area2D colArea)
	{
		if (colArea.IsInGroup("Obstacle"))
		{
			if (!isInvincible)
			{
				isInvincible = true;
				
				player.Call("IsHit");

				invincibilityTimer = 0.0f;
				GD.Print("Player hit");
			}
		}
	}

	// public void _BodyExited(RigidBody2D colBody)
	// {
	// 	if (colBody.IsInGroup("Obstacle"))
	// 	{
	// 		GD.Print("Collision with obstacle ended");
	// 		isColliding = false;
	// 	}
	// }

	public void _AreaExited(Area2D colArea)
	{
		if (colArea.IsInGroup("Obstacle"))
		{
			GD.Print("Collision with obstacle ended");
		}
	}
}
