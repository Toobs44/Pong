using Godot;
using System;

public partial class PlayerRef : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	public Vector2 ScreenSize;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    // I dont need gravity so I will comment this out.
    //public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
		ScreenSize = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (Input.IsActionPressed("left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("right"))
			velocity.X += 1;
		Velocity = velocity * Speed * (float)delta;
		MoveAndSlide();
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Position.Y);


        /*Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();*/
    }
}
