using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	[Signal]
	public delegate void RestartEventHandler();
	[Signal]
	public delegate void ContinueEventHandler();

	public void OnContinueButtonPressed()
	{
		EmitSignal(SignalName.Continue);
	}
	
	public void OnRestartButtonPressed()
	{
		EmitSignal(SignalName.Restart);
	}
}
