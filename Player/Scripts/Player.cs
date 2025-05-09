using Godot;
using System;

public partial class Player : Node2D
{
	private Boolean touching = false;
	private Vector2 targetPosition;

	private int health {get; set;}
	private ProgressBar healthBar;
	private Node userInterfaceNode = null;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = 100;
		this.GlobalPosition = new Vector2(640, 360);

		Node gameplayScene = GetParent();
		healthBar = gameplayScene.GetNode<ProgressBar>("UserInterface/HealthBar");
		userInterfaceNode = gameplayScene.GetNode("UserInterface");
	}


	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventScreenTouch screenTouch)
		{
			if (screenTouch.IsPressed())
			{
				GD.Print("Screen touched at: " + screenTouch.Position);
				this.GlobalPosition = screenTouch.Position;
				touching = true;
			}
			else
			{
				touching = false;
			}
		}

		if (@event is InputEventScreenDrag screenDrag && touching)
		{
			//GD.Print("Screen dragged at: " + screenDrag.Position);
			targetPosition = screenDrag.Position;

			// Move the player to the target position
			this.GlobalPosition = targetPosition;
		}
	}

	public void IsHit()
	{
		if (health > 0)
		{
			health -= 34;
			GD.Print("Player hit! Health: " + health);
			healthBar.Value = health;
			if (health <= 0)
			{
				GetParent().GetNode<Node2D>("ObjectScene").QueueFree();
				userInterfaceNode.CallDeferred("EndGame");
				GD.Print("Player is dead!");
				this.QueueFree();
			}
		}
	}
}
