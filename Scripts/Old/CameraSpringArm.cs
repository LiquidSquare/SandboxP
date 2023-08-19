using Godot;
using System;

public partial class CameraSpringArm : SpringArm3D
{
	//Global Variables
	[Export]
	public float verticalCameraSpeed = 0.004f;
	[Export]
	public float horizontalCameraSpeed = 0.004f;

	InputEventMouseMotion mouseMotion;
	
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		if( @event is InputEventMouseMotion )
		{
			mouseMotion = (InputEventMouseMotion)@event;
			// RotateY(mouseMotion.Relative.X * verticalCameraSpeed);
			RotateX(mouseMotion.Relative.Y * horizontalCameraSpeed);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Debugger
		// DebuggerText = @$"
		// 	H_MI: { mouseMotion.Relative.X }
		// 	V_MI: { mouseMotion.Relative.Y }
		// ";
		// DebuggerDisplay.GetNode<Label>("TextLabel").Text = DebuggerText;
	}
}
