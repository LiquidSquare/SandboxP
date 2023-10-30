using Godot;
using System;

public partial class IdleState : State
{
	public void UserInput(InputEvent @event)
	{
		if(@event is InputEventKey)
		{
			if (Input.IsActionPressed("unsheath"))
				EmitSignal( SignalName.Transitioned, this , "BoxingIdleState" );
			if (Input.IsActionPressed("crouch"))
				EmitSignal( SignalName.Transitioned, this , "CrouchIdleState" );
		}
	}

}
