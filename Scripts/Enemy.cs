using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	private NavigationAgent3D _navigationAgent;

	private float _movimentSpeed = 2.0f;
	private Vector3 _movementTargetPosition = new Vector3(-3.0f, 0.0f, 2.0f);

	public float gravity = 9.8f;

	public Vector3 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override void _Ready()
	{
		_navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
	
		_navigationAgent.PathDesiredDistance = 0.5f;
		_navigationAgent.TargetDesiredDistance = 0.5f;

		Callable.From(ActorSetup).CallDeferred();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (_navigationAgent.IsNavigationFinished()) return;

		Vector3 currentAgentPosition = GlobalTransform.Origin;
		Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Vector3 directionToNextPosition = (nextPathPosition - currentAgentPosition).Normalized();
		directionToNextPosition *= _movimentSpeed;

		Velocity = directionToNextPosition;

		MoveAndSlide();
	}

	private async void ActorSetup()
	{
		await ToSignal(GetTree(),SceneTree.SignalName.PhysicsFrame);
		MovementTarget = _movementTargetPosition;
	}

	// public const float Speed = 5.0f;
	// public const float JumpVelocity = 4.5f;

	// // Get the gravity from the project settings to be synced with RigidBody nodes.
	// public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	// public override void _PhysicsProcess(double delta)
	// {
	// 	Vector3 velocity = Velocity;

	// 	// Add the gravity.
	

	// 	// Handle Jump.
	// 	if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
	// 		velocity.Y = JumpVelocity;

	// 	// Get the input direction and handle the movement/deceleration.
	// 	// As good practice, you should replace UI actions with custom gameplay actions.
	// 	Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
	// 	Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
	// 	if (direction != Vector3.Zero)
	// 	{
	// 		velocity.X = direction.X * Speed;
	// 		velocity.Z = direction.Z * Speed;
	// 	}
	// 	else
	// 	{
	// 		velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	// 		velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
	// 	}

	// 	Velocity = velocity;
	// 	MoveAndSlide();
	// }
}
