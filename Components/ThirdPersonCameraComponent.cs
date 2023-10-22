using Godot;
using System;

public partial class ThirdPersonCameraComponent : Node3D
{
	//Global Variables
	[Export]
	public float verticalCameraSpeed = 0.004f;
	[Export]
	public float horizontalCameraSpeed = 0.004f;

	//Inner Variables
	InputEventMouseMotion mouseMotion;

	public Node3D SpringArmPivot;
	public SpringArm3D SpringArm;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured; //TODO
		SpringArmPivot = GetNode<Node3D>("%SpringArmPivot");
		SpringArm = GetNode<SpringArm3D>("%SpringArm");
	}

	public override void _Input(InputEvent @event)
	{
		if( @event is InputEventMouseMotion )
		{
			mouseMotion = (InputEventMouseMotion)@event;
			SpringArmPivot.RotateY(-mouseMotion.Relative.X * horizontalCameraSpeed);
			SpringArm.RotateX(-mouseMotion.Relative.Y * verticalCameraSpeed);
		}
	}

}
