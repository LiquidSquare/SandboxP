using Godot;
using System;

public partial class SpringArmPivot : Node3D
{
	//Global Variables
	[Export]
	public float verticalCameraSpeed = 0.004f;
	[Export]
	public float horizontalCameraSpeed = 0.004f;

	//Inner Variables
	InputEventMouseMotion mouseMotion;

	SpringArm3D SpringArm;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		SpringArm = GetNode<SpringArm3D>("SpringArm");
	}

	public override void _Input(InputEvent @event)
	{
		if( @event is InputEventMouseMotion )
		{
			mouseMotion = (InputEventMouseMotion)@event;
			RotateY(-mouseMotion.Relative.X * horizontalCameraSpeed);
			SpringArm.RotateX(-mouseMotion.Relative.Y * verticalCameraSpeed);
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		// this.Rotation = Player.Rotation;
	}

	public override void _Process(double delta)
	{
	}
}
