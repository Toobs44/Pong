using Godot;
using System;

public partial class CPU : StaticBody2D
{
	Vector2 ball_pos;
	int dist;
	float move_by;
	int win_height;
	int p_height;

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
		// move the paddle towards the ball
		ball_pos = GetNode<Ball>("../Ball").Position;
		dist = (int)Position.Y - (int)ball_pos.Y;

		if (Math.Abs(dist) > Main.PADDLE_SPEED * delta)
		{
			move_by = Main.PADDLE_SPEED * (float)delta * (dist / Math.Abs(dist));
		}
		else
		{
			move_by = dist;
		}
		
		position -= move_by;

		Position = new Vector2(
		x: Position.X,
		// y: Mathf.Clamp(position, 0, win_height)//this works too but half of the paddle will clip the wall
		y: Mathf.Clamp(position, p_height / 2, win_height - p_height / 2)
		);
	}
}
