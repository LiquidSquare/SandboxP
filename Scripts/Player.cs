using Godot;
using System;
using System.Diagnostics;

public partial class Player : CharacterBody3D
{	
	public ThirdPersonMovementComponent ThirdPersonMovementComponent;

	public override void _Ready()
	{
		ThirdPersonMovementComponent = GetNode<ThirdPersonMovementComponent>("ThirdPersonMovementComponent");
	}

    public override void _PhysicsProcess(double delta)
    {
		Velocity = ThirdPersonMovementComponent.Velocity;
		MoveAndSlide();
    }

}
