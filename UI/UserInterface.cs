using Godot;
using System;

public partial class UserInterface : Node
{
	private Label scoreLabel = null;
	private ProgressBar healthBar = null;
	private TextureRect multiplierBackground = null;

	[Export] public int Score = 0;
	[Export] public int ScoreIncrement = 1;
	[Export] public double ScoreIncrementTime = 0.0f;
	[Export] public double multiplierTimer = 0.0f;
	[Export] public Boolean GameOver {get; set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scoreLabel = (Label)GetNode("ScoreCounter");
		if (scoreLabel == null)
		{
			GD.PrintErr("ScoreLabel not found in UserInterface.");
			return;
		}
		scoreLabel.Text = Score.ToString();

		healthBar = GetNode<ProgressBar>("HealthBar");
		if (healthBar == null)
		{
			GD.PrintErr("HealthBar not found in UserInterface.");
			return;
		}
		healthBar.Value = 100; // Set initial health value

		multiplierBackground = healthBar.GetChild<TextureRect>(0);
		if (multiplierBackground == null)
		{
			GD.PrintErr("MultiplierBackground not found in UserInterface.");
			return;
		}
		multiplierBackground.Visible = false; // Hide multiplier background initially
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!GameOver)
		{
			ScoreIncrementTime += delta;

			if (ScoreIncrementTime >= 5.0f)
			{
				ScoreIncrementTime = 0.0f;
				ScoreIncrement++;
			}

			if (multiplierTimer > 0)
			{
				multiplierTimer -= delta;
				Score += ScoreIncrement * 2; // Double score while power-up is active
			}
			else
			{
				Score += ScoreIncrement;
			}

			if (multiplierTimer <= 0 && multiplierBackground.Visible)
			{
				multiplierBackground.Visible = false; // Hide multiplier background when timer ends
			}
			
			scoreLabel.Text = Score.ToString();
		}
	}

	public void EndGame()
	{
		GameOver = true;
		GetNode<GameOver>("GameOver").SetFinalScore(Score);
		GetNode<VBoxContainer>("GameOver").Visible = true;

		scoreLabel.Visible = false;
		multiplierBackground.Visible = false;
	}

	public void StartScoreMultiplier()
	{
		multiplierTimer += 5.0f;
		multiplierBackground.Visible = true;
	}
}
