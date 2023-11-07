using Godot;
using System;

public partial class WalkingState : State
{
	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if ( releasedMovementInput() && !isHoldingAnyMovementInput() )
			EmitSignal( SignalName.Transitioned, this , "IdleState" );
		if ( Input.IsActionJustPressed("unsheath")  )
			EmitSignal( SignalName.Transitioned, this, "BoxWalkingState" );
		if ( Input.IsActionJustPressed("crouch")  )
			EmitSignal( SignalName.Transitioned, this, "CrouchWalkingState" );
		if ( Input.IsActionJustPressed("run") )
			EmitSignal( SignalName.Transitioned, this, "RunningState" );
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}
	
}
