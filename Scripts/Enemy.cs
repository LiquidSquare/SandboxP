using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	//Nodes References
	private StaticBody3D TargetA;
	private StaticBody3D TargetB;
	private StaticBody3D TargetC;
	private StaticBody3D TargetD;

	private NavigationAgent3D _navigationAgent;

	private float _movimentSpeed = 10.0f;

	public float gravity = 9.8f;

	private Node3D CurrentTarget;
	public Vector3 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override void _Ready()
	{
		//Nodes Instances
		_navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");

		TargetA = GetNode<StaticBody3D>("../TargetA");
		TargetB = GetNode<StaticBody3D>("../TargetB");
		TargetC= GetNode<StaticBody3D>("../TargetC");
		TargetD = GetNode<StaticBody3D>("../TargetD");

	
		CurrentTarget = TargetA;
		MovementTarget = TargetA.Position;

		_navigationAgent.PathDesiredDistance = 2.0f;
		_navigationAgent.TargetDesiredDistance = 2.0f;

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		if (_navigationAgent.IsNavigationFinished()) 
		{
			if (CurrentTarget == TargetA) CurrentTarget = TargetB;
			else if (CurrentTarget == TargetB) CurrentTarget = TargetC;
			else if (CurrentTarget == TargetC) CurrentTarget = TargetD;
			else if (CurrentTarget == TargetD) CurrentTarget = TargetA;
			
			MovementTarget = CurrentTarget.Position;
		}

		Vector3 currentAgentPosition = GlobalTransform.Origin;
		Vector3 nextPathPosition = _navigationAgent.GetNextPathPosition();

		Vector3 directionToNextPosition = (nextPathPosition - currentAgentPosition).Normalized();
		directionToNextPosition *= _movimentSpeed;

		Velocity = directionToNextPosition;

		MoveAndSlide();
	}

}
