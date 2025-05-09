using Godot;
using System;

public partial class ScoreMultiplier : Node2D
{
	private double despawnTimer = 5.0f;

	private Node userInterface = null;

	private Area2D triggerArea;

	private Callable AreaEnteredCallable; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		triggerArea = GetNode<Area2D>("%Area2D");
		AreaEnteredCallable = new Callable(this, nameof(OnAreaEntered));
		triggerArea.Connect("area_entered", AreaEnteredCallable);

		if (userInterface == null)
		{
			userInterface = GetParent().GetParent().GetNode("UserInterface");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (despawnTimer <= 0.0)
		{
			this.QueueFree();
		}
		else
		{
			despawnTimer -= delta;
		}
	}

	public void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Player"))
		{
			userInterface.CallDeferred("StartScoreMultiplier");
			this.QueueFree();
			// Start the timer
		}
	}

	public void ReduceItemCount()
	{
		
	}
}
