using Godot;
using System;

public partial class BoxingIdleState : State
{
	public void UserInput(InputEvent @event)
	{
		if(@event is InputEventKey)
		{
			if (Input.IsActionPressed("unsheath"))
				EmitSignal( SignalName.Transitioned, this , "IdleState" ); 

			if(isHoldingAnyMovementInput())
				EmitSignal( SignalName.Transitioned, this , "BoxWalkingState" ); 
		}
	}
	private bool isHoldingAnyMovementInput() => 
			Input.IsActionPressed("up")   ||
			Input.IsActionPressed("down") ||
			Input.IsActionPressed("left") ||
			Input.IsActionPressed("right");
}
