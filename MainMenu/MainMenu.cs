using Godot;
using System;

public partial class MainMenu : Node
{
	Node sceneManager;

	[Export] private TextureButton startButton;
	private Callable startButtonCallable;

	[Export] private TextureButton quitButton;
	private Callable quitButtonCallable;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sceneManager = GetParent<Node>();

		startButton = GetNode<TextureButton>("%StartButton");
		startButtonCallable = new Callable(this, nameof(OnStartButtonPressed));
	
		quitButton = GetNode<TextureButton>("%QuitButton");
		quitButtonCallable = new Callable(this, nameof(OnQuitButtonPressed));

		startButton.Connect("pressed", startButtonCallable);
		quitButton.Connect("pressed", quitButtonCallable);
	}

	// Called when the "Start" button is pressed
	public void OnStartButtonPressed()
	{
		GD.Print("Start button pressed");

		if (!(sceneManager == null))
		{
			sceneManager.CallDeferred("ChangeScene", 1);
		}
		else
		{
			GD.Print("SceneManager not found.");
			return;
		}

		GD.Print("Scene changed to: 1");
	}

	// Called when the "Quit" button is pressed
	public void OnQuitButtonPressed()
	{
		GD.Print("Exit button pressed");
		GetTree().Quit();
	}
}
