using Godot;
using System;
using System.Security;

public partial class Ball : CharacterBody2D
{
	Vector2 win_size;
	const int START_SPEED = 500;
	const int ACCEL = 50;
	int speed;
	Vector2 dir;
	const float MAX_Y_VECTOR  = 0.6f;

	// called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        win_size = GetViewportRect().Size;
    }

	public void NewBall()
	{
		Vector2 position = Position;
		//randomize start position and direction
		position.X = win_size.X / 2;
		position.Y = (float)GD.RandRange(200, win_size.Y - 200);
		speed = START_SPEED;
		dir = RandomDirection();
		Position = position;
	}

    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(dir * speed * (float)delta); 
		// PhysicsBody2D collider;
		if (collision != null)
		{
			var collider = (Node2D)collision.GetCollider();//should I cast this? what should I do here?
			//if ball hits paddle
			if(collider == GetNode<Player>("../Player") || collider == GetNode<CPU>("../CPU"))
			{
				speed += ACCEL;
				dir = NewDirection(collider);
			}
			//if ball hits a wall
			else
			{
				dir = dir.Bounce(collision.GetNormal());
			}
		}
	}

	// !!! dont forget to add the randomness to the new_dir.Y as well !!!
	public Vector2 RandomDirection()
	{
		//create a new vector2 for new_dir
		Vector2 new_dir = new Vector2();
		// Define the array
		int[] directx = {1, -1};
		// Create a random number generator
		Random random = new Random();
		// Pick a random number
		int randomIndex = random.Next(directx.Length);
		//set the new_dir.x to the randomly picked value
		new_dir.X = directx[randomIndex];
		new_dir.Y = (float)GD.RandRange(-1.0,1.0);
		return new_dir.Normalized();
	}

	public Vector2 NewDirection(Node2D collider)
		{
			// var position = Position;
			// var p_height = GetNode<Player>("../Player");
			var ballY = Position.Y;
			var padY = collider.Position.Y;
			
			ColorRect colorRect;
			// Get the paddle height
			
			colorRect = collider.GetNode<ColorRect>("ColorRect");
			var p_height = colorRect.Size.Y;

			var dist = ballY - padY;
			var new_dir = new Vector2();

			// flip the horizontal direction
			new_dir.X = dir.X > 0 ? -1 : 1;
			//change vertical direction
			new_dir.Y = (dist / (p_height / 2)) * MAX_Y_VECTOR;
			return new_dir.Normalized();
		}

}
