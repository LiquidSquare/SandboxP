using Godot;
using System;

public partial class ThirdPersonMovementComponent : Node3D
{

	[Export]
	public float Speed = 0.0f;
	[Export]
	public float Gravity = 9.8f; //TODO

	public float horizontalInput = 0.0f;
	public float verticalInput = 0.0f;
	public Vector3 Direction = Vector3.Up;
	public Vector3 Velocity = Vector3.Zero;

	[Export]
	public CharacterBody3D Target;

	[Export]
	public ThirdPersonCameraComponent ThirdPersonCamera;
	[Export]
	public StateMachineComponent StateMachine;

	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{
		horizontalInput = Input.GetActionStrength("right") - Input.GetActionStrength("left");
		verticalInput = Input.GetActionStrength("down") - Input.GetActionStrength("up");
	}

	public override void _PhysicsProcess(double delta)
	{

		//TODO
		Speed = StateMachine.CurrentState.Name == "RunningState" ? 4.0f : 1.9f;

		Velocity = Target.Velocity;

		if(!Target.IsOnFloor() )
		{
			Velocity.Y -= Gravity * (float)delta;
		}
		
		Direction = Transform.Basis * new Vector3( horizontalInput, 0, verticalInput ).Normalized();
		Direction = Direction.Rotated(Vector3.Up, ThirdPersonCamera.SpringArmPivot.Rotation.Y ); 

		if ( Direction != Vector3.Zero )
		{
			Velocity.X = Mathf.Lerp( Velocity.X, Direction.X * Speed, 0.15f );
			Velocity.Z = Mathf.Lerp( Velocity.Z, Direction.Z * Speed, 0.15f );

		} else {
			Velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			Velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

	}

}
