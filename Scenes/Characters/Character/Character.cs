using Godot;
using System;
using System.Diagnostics;

public partial class Character : CharacterBody3D
{	
    /* COMPONENTS */
    private ViewArea3DComponent ViewAreaComponent;
    private ThirdPersonCameraComponent CameraComponent;
    private StateMachineComponent StateMachineComponent;
    private ThirdPersonMovementComponent MovementComponent;

	public override void _Ready()
	{
        // ViewAreaComponent = GetNode<ViewArea3DComponent>("ViewArea3DComponent"); // TODO
        // CameraComponent = GetNode<ThirdPersonCameraComponent>("ThirdPersonCameraComponent");
        StateMachineComponent = GetNode<StateMachineComponent>("StateMachineComponent");
        MovementComponent = GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack"))
			GD.Print("ok");
    }

    public override void _PhysicsProcess(double delta)
    {
        /* VELOCITY HANDLER FROM MOVEMENT TODO */ 
        if( 
            StateMachineComponent.CurrentState.Name == "WalkingState" || 
            StateMachineComponent.CurrentState.Name == "BoxWalkingState" ||
            StateMachineComponent.CurrentState.Name == "CrouchWalkingState" ||
            StateMachineComponent.CurrentState.Name == "RunningState" ){
            Velocity = MovementComponent.Velocity;
            MoveAndSlide();
        }
    }

}
