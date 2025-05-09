using Godot;
using System;
using System.Collections.Generic;

public partial class ObstacleSpawner : Node
{
	RandomNumberGenerator rng = new();

	private Node spawnPointsParent = null;
	private List<int> spawnPoints = new();

	[Export] private Node obstacleParent;
	[Export] private PackedScene obstaclePrefab;

	[Export] private double durationBetweenSpawns = 0.0f;
	[Export] private double spawnTimer = 0.0f;
	[Export] private double difficultyTimer = 0.0f;

	[Export] private double difficulty = 1.0f;

	[Export] private float speedModifier = 1.0f;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rng.Randomize();

		spawnPoints.Add(0); // Left top corner
		spawnPoints.Add(1); // Left bottom corner
		spawnPoints.Add(2); // Right top corner
		spawnPoints.Add(3); // Right bottom corner

		spawnPointsParent = GetNode("ObstacleSpawnPoints");
		if (spawnPointsParent == null)
		{
			GD.Print("Spawn points parent node not found.");
			return;
		}
		if (spawnPointsParent.GetChildCount() == 0)
		{
			GD.Print("No spawn points available.");
			return;
		}

		obstaclePrefab = GD.Load<PackedScene>("res://Objects/Obstacles/Scenes/obstacle_wall.tscn");
		
		
		obstacleParent = GetNode("ObstacleParent");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		difficultyTimer += delta;

		// Increases difficulty by 10% every 2 seconds
		if (difficultyTimer >= 2.0f)
		{
			difficultyTimer -= 2.0f;
			difficulty *= 1.10f;
			
			// GD.Print("Difficulty increased: " + difficulty);
		}



		spawnTimer += delta * difficulty;
		durationBetweenSpawns += delta;
		while (spawnTimer >= 3.0f)
		{
			// GD.Print("\nTime since last spawn: " + durationBetweenSpawns + "\n");
			durationBetweenSpawns = 0.0f;

			spawnTimer -= 3.0f;
			SpawnObstacle();
		}


		
		// GD.Print("Spawn timer: " + spawnTimer);

		// GD.Print("Difficulty: " + difficulty);
		// GD.Print("Difficulty timer: " + difficultyTimer);
	}

	private void SpawnObstacle()
	{
		if (spawnPoints.Count == 0)
		{
			GD.Print("No spawn points available.");
			return;
		}

		
		// float speed = GetSpeed();
		speedModifier = ((float)difficulty - 1) / 3.0f + 1.0f;
		float speed = 300f * speedModifier;
		
		
		// Randomly select a spawn point
		int spawnIndex = rng.RandiRange(0, spawnPoints.Count - 1);

		Marker2D spawnPoint;
		spawnPoint = spawnPointsParent.GetChild<Marker2D>(spawnPoints[spawnIndex]);
		Vector2 spawnPosition = new(spawnPoint.GlobalPosition.X, rng.RandfRange(spawnPoint.GlobalPosition.Y - 125, spawnPoint.GlobalPosition.Y + 125));
		
		// Vector2 direction = GetDirection(spawnIndex);
		Vector2 direction = new();
		switch (spawnIndex)
		{
			case < 2: // Left top corner or Left bottom corner
				direction = new Vector2(1, 0);
				break;
			default: // Right top corner or Right bottom corner
				direction = new Vector2(-1, 0);
				break;
		}
		
		RigidBody2D obstacleInstance = GetObstacleInstance();
		obstacleInstance.GlobalPosition = spawnPosition;
		obstacleInstance.AddToGroup("Obstacle");
		obstacleParent.AddChild(obstacleInstance);
		
		obstacleInstance.CallDeferred("LaunchWall", spawnPosition, direction, speed);
		
		// GD.Print("Obstacle instance created: " + obstacleInstance);
		// GD.Print("");
		// GD.Print("Spawn index: " + spawnIndex);
		// GD.Print("Spawn position" + spawnPosition);
		// GD.Print("");
		// GD.Print("Speed: " + speed);
		// GD.Print("Speed modifier: " + speedModifier);
		// GD.Print("");
		// GD.Print("Direction: " + direction);
		// GD.Print("Difficulty: " + difficulty);
	}

	// private float GetSpeed()
	// {
	// 	speedModifier = ((float)difficulty - 1) / 3.0f + 1.0f;
	// 	float speed = 300f * speedModifier;

	// 	return speed;
	// }

	// private Vector2 SetSpawnPosition(int spawnIndex)
	// {
	// 	// Randomly select a spawn point
	// 	Marker2D spawnPoint;


	// 	spawnPoint = spawnPointsParent.GetChild<Marker2D>(spawnPoints[spawnIndex]);

	// 	Vector2 spawnPosition = new(spawnPoint.GlobalPosition.X, rng.RandfRange(spawnPoint.GlobalPosition.Y - 125, spawnPoint.GlobalPosition.Y + 125));

	// 	return spawnPosition;
	// }

	// private static Vector2 GetDirection(int spawnIndex)
	// {
	// 	Vector2 direction = new();

	// 	switch (spawnIndex)
	// 	{
	// 		case 0: // Left top corner
	// 			direction = new Vector2(1, 0);
	// 			break;
	// 		case 1: // Left bottom corner
	// 			direction = new Vector2(1, 0);
	// 			break;
	// 		case 2: // Right top corner
	// 			direction = new Vector2(-1, 0);
	// 			break;
	// 		case 3: // Right bottom corner
	// 			direction = new Vector2(-1, 0);
	// 			break;
	// 		default:
	// 			GD.Print("Invalid spawn index.");
	// 			break;
	// 	}

	// 	return direction;
	// }

	private RigidBody2D GetObstacleInstance()
	{
		RigidBody2D obstacleInstance = obstaclePrefab.Instantiate<RigidBody2D>();
		obstacleInstance.AddToGroup("Obstacle");
		
		return obstacleInstance;
	}
}
