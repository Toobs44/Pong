using Godot;
using System;

public partial class Player : StaticBody2D
{
	int win_height;
	public int p_height;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get access to the ColorRect node directly
		ColorRect colorRect = GetNode<ColorRect>("ColorRect");

		// Get the size of the ColorRect.RectSize
		Vector2 size = colorRect.Size;

		win_height = (int)GetViewportRect().Size.Y;//save the y of the vector2 as an int.
		p_height = (int)size.Y;// save y of the paddle vector as an int.
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var position = Position.Y;

		if(Input.IsActionJustPressed("Pause_Menu"))
		{
			GetNode<CanvasLayer>("../PauseMenu").Show();
			GetTree().Paused = true;
		}	

		//Main.PADDLE_SPEED gets the const variable from the script of the parent node Main to be used here.
		if(Input.IsActionPressed("up"))
		{
			position -= Main.PADDLE_SPEED * (float)delta;

		}
		else if(Input.IsActionPressed("down"))
		{
			position += Main.PADDLE_SPEED * (float)delta;

		}

		// Update the player's position and limit the movement to the window
        Position = new Vector2(
			x: Position.X,
			// y: Mathf.Clamp(position, 0, win_height)//this works too but half of the paddle will clip the wall
			y: Mathf.Clamp(position, p_height / 2, win_height - p_height / 2)
			);
	}
}
