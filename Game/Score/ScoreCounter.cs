using Godot;
using System;

public partial class ScoreCounter : Node
{
	private Label scoreLabel = null;

	[Export] public int Score = 0;
	[Export] public int ScoreIncrement = 1;
	[Export] public double ScoreIncrementTime = 0.0f;
	[Export] public int PowerUpActive {get; set; } = 0;
	[Export] public int PowerUpDuration {get; set; } = 5;
	[Export] public double PowerUpTime {get; set; } = 0;
	[Export] public Boolean GameOver {get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scoreLabel = this.GetParent().GetNode<Node>("%UserInterface").GetNode<Label>("ScoreLabel");
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

			if (PowerUpActive == 1)
			{
				PowerUpTime += delta;
				Score += ScoreIncrement * PowerUpActive;

				if (PowerUpTime >= PowerUpDuration)
				{
					PowerUpActive = 0;
					PowerUpTime = 0;
				}
			}
			else
			{
				Score += ScoreIncrement;
			}
			
			scoreLabel.Text = Score.ToString();
		}
	}

	public void EndGame()
	{
		GameOver = true;
		this.GetParent().GetNode<GameOver>("GameOver").SetFinalScore(Score);
		this.GetParent().GetNode<VBoxContainer>("GameOver").Visible = true;

		scoreLabel.Visible = false;
	}

	public void ApplyPowerUp(int powerUp)
	{
		PowerUpActive = powerUp;
	}
}
