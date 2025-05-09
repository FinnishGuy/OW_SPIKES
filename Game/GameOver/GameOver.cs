using Godot;
using System;

public partial class GameOver : VBoxContainer
{
	private SceneManager sceneManager;


	[Export] private Label finalScoreLabel;


	[Export] private TextureButton retryButton;
	private Callable retryButtonCallable;

	[Export] private TextureButton menuButton;
	private Callable menuButtonCallable;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetTree().GetRoot().GetChild(0) as SceneManager;
		if (sceneManager == null)
		{
			GD.PrintErr("SceneManager not found in the scene tree.");
			return;
		}

		finalScoreLabel = GetNode<Label>("%FinalScore");


		retryButton = GetNode<TextureButton>("%RetryButton");
		retryButtonCallable = new Callable(this, nameof(OnRetryButtonPressed));
		
		retryButton.Connect("pressed", retryButtonCallable);

		
		menuButton = GetNode<TextureButton>("%MenuButton");
		menuButtonCallable = new Callable(this, nameof(OnMenuButtonPressed));

		menuButton.Connect("pressed", menuButtonCallable);
	}

	
	// Called when the "Retry" button is pressed
	public void OnRetryButtonPressed()
	{
		GD.Print("Retry button pressed");
		sceneManager.ChangeScene(1); // Load the game scene
		GD.Print("Scene changed to: 1");
	}

	// Called when the "Menu" button is pressed
	public void OnMenuButtonPressed()
	{
		GD.Print("Menu button pressed");
		sceneManager.ChangeScene(0); // Load the main menu scene
		GD.Print("Scene changed to: 0");
	}

	public void SetFinalScore(int score)
	{
		finalScoreLabel.Text = "Final Score: " + score.ToString();
		GD.Print("Final score set to: " + score);
	}
}
