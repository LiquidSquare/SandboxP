using Godot;
using System;

public partial class CrouchWalkingState : State
{
	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if ( releasedMovementInput() && !isHoldingAnyMovementInput() )
		{
			EmitSignal( SignalName.Transitioned, this , "CrouchIdleState" );
		}
		if ( Input.IsActionJustPressed("crouch") && isHoldingAnyMovementInput() )
		{
			EmitSignal( SignalName.Transitioned, this , "WalkingState" );
		}
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}
}
