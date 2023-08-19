using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody3D
{
	//Global Variables
	[Export]
	public float Speed = 5.0f;
	
	//Inner Variables
	Vector3 Direction = Vector3.Zero;
	float Gravity = 9.8f;
	string DebuggerText = ""; 

	float horizontalInput;
	float verticalInput;

	//Node's References
	Node3D Visuals;
	Node3D SpringArmPivot;
	Control DebuggerDisplay;
	AnimationPlayer AnimationPlayer;
	AnimationTree AnimationTree;

	public override void _Ready()
	{
		//Node's Initializations
		Visuals = GetNode<Node3D>("Visuals");
		SpringArmPivot = GetNode<Node3D>("SpringArmPivot");
		DebuggerDisplay = GetNode<Control>("../DebuggerDisplay");
		AnimationPlayer = GetNode<AnimationPlayer>("Visuals/AnimationPlayer");
		AnimationTree = GetNode<AnimationTree>("Visuals/AnimationTree");
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
			
            Vector3 newVisualsRotation = new Vector3()
            {
				X = Visuals.Rotation.X,
                Y = Mathf.LerpAngle(Visuals.Rotation.Y, Mathf.Atan2(velocity.X, velocity.Z), 0.15f),
				Z = Visuals.Rotation.Z
            };
            Visuals.Rotation = newVisualsRotation;

		} else {
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
		}

		AnimationTree.Set("parameters/BlendSpace1D/blend_position",velocity.Length() / Speed );

		Velocity = velocity;
		MoveAndSlide();

	}


	public override void _Process(double delta)
	{
		// Debugger
		DebuggerText = @$"
			Character  Rotation: { Rotation.Y }
			Spring Arm Rotation: { SpringArmPivot.Rotation.Y }
		";
		DebuggerDisplay.GetNode<Label>("TextLabel").Text = DebuggerText;
	}

}
