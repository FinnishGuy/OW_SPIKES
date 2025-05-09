using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

public partial class SceneManager : Node
{
	private Dictionary<int, string> sceneDictionary = new Dictionary<int, string>();


	public override void _Ready()
	{
		sceneDictionary.Add(0, "res://MainMenu/main_menu.tscn");
		sceneDictionary.Add(1, "res://Game/gameplay.tscn");

		// Load the initial scene
		if (this.GetChildCount() == 0)
		{
			LoadScene(0);
		}
		else
		{
			GD.Print("SceneManager already has a child scene.");
		}
	}

	public void ChangeScene(int loadSceneIndex)
	{
		if (this.GetChildCount() > 0)
		{
			Node currentScene = this.GetChild(0);
			currentScene.QueueFree();
		}

		LoadScene(loadSceneIndex);
		GD.Print("Scene changed to: " + loadSceneIndex);
	}

	public void LoadScene(int sceneIndex)
	{
		if (sceneDictionary.ContainsKey(sceneIndex))
		{
			string scenePath = sceneDictionary[sceneIndex];
			PackedScene newScene = ResourceLoader.Load<PackedScene>(scenePath);
			Node loadedScene = newScene.Instantiate();
			this.AddChild(loadedScene);
		}
		else
		{
			GD.PrintErr("Invalid scene index: " + sceneIndex);
		}
	}
}
