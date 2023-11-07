using Godot;
using System;

public partial class BoxWalkingState : State
{
	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if ( releasedMovementInput() && !isHoldingAnyMovementInput() )
			EmitSignal( SignalName.Transitioned, this , "BoxingIdleState" );
		if ( Input.IsActionJustPressed("unsheath") )
			EmitSignal( SignalName.Transitioned, this , "WalkingState" );
	}
	
}
