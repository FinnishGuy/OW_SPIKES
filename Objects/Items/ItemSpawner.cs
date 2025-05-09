using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class ItemSpawner : Node
{
	private List<String> itemPaths = new()
	{
		"res://Objects/Items/Scenes/score_multiplier.tscn",
	};
	private List<PackedScene> itemPrefabs = new();

	private RandomNumberGenerator rng = new();

	private int maxItems = 5;
	public int CurrentItems {get; set; } = 0;
	
	private double spawnInterval = 10.0f; // seconds
	private double spawnTimer = 0;

	private Marker2D bottomLeft = null;
	private Marker2D topRight = null;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng.Randomize();

		for (int i = 0; i < itemPaths.Count; i++)
		{
			PackedScene item = GD.Load<PackedScene>("res://Objects/Items/Scenes/score_multiplier.tscn");
			itemPrefabs.Add(item);
		}
		

		bottomLeft = GetNode<Marker2D>("%BottomLeft");
		topRight = GetNode<Marker2D>("%TopRight");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		spawnTimer += delta;
		if (spawnTimer >= spawnInterval && CurrentItems < maxItems)
		{
			spawnTimer = 0.0f;
			SpawnItem();
		}
	}

	private void SpawnItem()
	{
		Vector2 spawnPoint = new(
			rng.RandiRange((int)bottomLeft.GlobalPosition.X, (int)topRight.GlobalPosition.X),
			rng.RandiRange((int)bottomLeft.GlobalPosition.Y, (int)topRight.GlobalPosition.Y)
		);
		GD.Print("Spawn point: " + spawnPoint);


		int randomIndex = rng.RandiRange(0, itemPrefabs.Count - 1);
		Node2D item = itemPrefabs[randomIndex].Instantiate<Node2D>();
		if (item == null)
		{
			GD.PrintErr("Failed to load item prefab.");
			return;
		}
		GD.Print("Item prefab loaded: " + item.Name);

		item.GlobalPosition = spawnPoint;
		GD.Print("Item spawned at: " + item.Position);
		
		this.GetParent().AddChild(item);
		GD.Print("Item path:" + item.GetPath());


		
		CurrentItems++;
	}
}
