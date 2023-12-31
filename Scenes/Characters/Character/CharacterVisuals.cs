using Godot;
using System;

public partial class CharacterVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;
	float RotationSpeed = 10f;

	//Nodes References
	public AnimationTree AnimationTree;
	public AnimationPlayer AnimationPlayer;
	public Skeleton3D Skeleton;
	public CharacterBody3D Parent;
	[Export]
	public ThirdPersonMovementComponent ThirdPersonMovementComponent;
	[Export]
	public StateMachineComponent StateMachineComponent;
	public AnimationNodeStateMachinePlayback StateMachinePlayback;

	public override void _Ready()
	{
		Parent = GetParent<CharacterBody3D>();
		Skeleton = GetNode<Skeleton3D>("RootNode/GeneralSkeleton");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		StateMachineComponent = GetParent<Node3D>().GetNode<StateMachineComponent>("StateMachineComponent");
		ThirdPersonMovementComponent = Parent.GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
		StateMachinePlayback = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
	}

	public override void _Process(double delta)
	{
		/* ANIMATION STATE MACHINE */ //TODO
		switch(StateMachineComponent.CurrentState.Name)
		{
			case "WalkingState":
				StateMachinePlayback.Travel("Nla_Walking");
				RotateVisualsToFaceMovement(); //TODO
				break;
			case "BoxingIdleState":
				StateMachinePlayback.Travel("Nla_BoxingFightIdle");
				break;
			case "BoxWalkingState":
				StateMachinePlayback.Travel("Nla_BoxWalking");
				RotateVisualsToFaceMovement(); //TODO
				break;
			case "CrouchIdleState":
			StateMachinePlayback.Travel("Nla_CrouchIdle");
				break;
			case "IdleState":
				StateMachinePlayback.Travel("Nla_Idle");
				break;
			case "CrouchWalkingState":
				StateMachinePlayback.Travel("Nla_CrouchWalking");
				RotateVisualsToFaceMovement(); //TODO
				break;
			case "RunningState":
				StateMachinePlayback.Travel("Nla_Running");
				RotateVisualsToFaceMovement(); //TODO
				break;
		}
	}

    private void RotateVisualsToFaceMovement()
    {
        /* VISUALS ROTATION TO FACE MOVEMENT INPUT */
		if ( ThirdPersonMovementComponent.Direction != Vector3.Zero )
		{
			newVisualsRotation = new Vector3()
			{
				X = Rotation.X,
				Y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(ThirdPersonMovementComponent.Velocity.X, ThirdPersonMovementComponent.Velocity.Z) + Mathf.Pi, 0.25f),
				Z = Rotation.Z
			};
			Rotation = newVisualsRotation;
		}
    }
}
