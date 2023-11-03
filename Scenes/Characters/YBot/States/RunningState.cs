using Godot;
using System;

public partial class RunningState : State
{
	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if ( releasedMovementInput() && !isHoldingAnyMovementInput() )
			EmitSignal( SignalName.Transitioned, this , "IdleState" );
		if ( Input.IsActionJustPressed("unsheath")  )
			EmitSignal( SignalName.Transitioned, this, "BoxWalkingState" );
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}

}
