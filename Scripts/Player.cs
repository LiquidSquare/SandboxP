using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody3D
{
	//Global Variables
	[Export]
	public float Speed = 5.0f;
	
	//Inner Variables
	Vector3 Direction = Vector3.Up;
	float Gravity = 9.8f;

	float horizontalInput;
	float verticalInput;

	//Node's References
	Node3D SpringArmPivot;

	public override void _Ready()
	{
		//Node's Initializations
		SpringArmPivot = GetNode<Node3D>("ThirdPersonCameraComponent/SpringArmPivot");
	}
 
	public override void _Input(InputEvent @event)
	{
		horizontalInput = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		verticalInput = Input.GetActionStrength("down") - Input.GetActionStrength("up");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if(!IsOnFloor() )
			velocity.Y -= Gravity * (float)delta;
		
		Direction = Transform.Basis * new Vector3( horizontalInput, 0, verticalInput ).Normalized();
		Direction = Direction.Rotated(Vector3.Up, SpringArmPivot.Rotation.Y); 

		if ( Direction != Vector3.Zero )
		{
			velocity.X = Mathf.Lerp( velocity.X, Direction.X * Speed, 0.15f );
			velocity.Z = Mathf.Lerp( velocity.Z, Direction.Z * Speed, 0.15f );

		} else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
		}
		
		Velocity = velocity;
		MoveAndSlide();

	}


	public override void _Process(double delta)
	{
		var debuggText = @$"
			Character  Rotation: X:{ Rotation.X }, Y:{ Rotation.Y }, Z:{ Rotation.Z }
			Spring Arm Rotation: { SpringArmPivot.Rotation.Y }
		";
		// Debugger
		// DebuggerDisplay.Call("AddDebugInformation", debuggText);
	}

}
