using Godot;
using System;

public partial class BoxingIdleState : State
{
	public void UserInput(InputEvent @event)
	{
		if (Input.IsActionPressed("unsheath"))
				EmitSignal( SignalName.Transitioned, this , "IdleState" ); 

		if (isHoldingAnyMovementInput())
			EmitSignal( SignalName.Transitioned, this , "BoxWalkingState" );
		
		if (Input.IsActionJustPressed("attack"))
			EmitSignal( SignalName.Transitioned, this , "AttackingState" );
	}
	
}
