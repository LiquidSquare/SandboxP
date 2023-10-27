using Godot;
using System;
using System.Diagnostics;

public partial class Y_Bot : CharacterBody3D
{	
    /* COMPONENTS */
    private ViewArea3DComponent ViewAreaComponent;
    private ThirdPersonCameraComponent CameraComponent;
    private ThirdPersonMovementComponent MovementComponent;

	public override void _Ready()
	{
        ViewAreaComponent = GetNode<ViewArea3DComponent>("ViewArea3DComponent");
        CameraComponent = GetNode<ThirdPersonCameraComponent>("ThirdPersonCameraComponent");
        MovementComponent = GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
	}

    public override void _PhysicsProcess(double delta)
    {
        /* VELOCITY HANDLER FROM MOVEMENT */
        Velocity = MovementComponent.Velocity;
		MoveAndSlide();
    }

}
