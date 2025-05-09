using Godot;
using System;

public partial class HealthBar : Control
{
	public ProgressBar healthBar;

	public override void _Ready()
	{
		healthBar = GetChild<ProgressBar>(0);
	}

	public void UpdateHealthBar(float health)
	{
		healthBar.Value = health;
	}
}
