using Godot;
using System;

public partial class IdleState : State
{
	public void UserInput(InputEvent @event)
	{
		if(@event is InputEventKey)
		{
			if (Input.IsActionPressed("unsheath"))
			{ 
				GD.Print("");
				EmitSignal( SignalName.Transitioned, this , "BoxingIdleState" );
			}
		}
	}

}
