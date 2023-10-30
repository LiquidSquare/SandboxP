using Godot;
using System;

public partial class BoxingIdleState : State
{
	public void UserInput(InputEvent @event)
	{
		if(@event is InputEventKey)
		{
			if (Input.IsActionPressed("unsheath"))
			{ 
				GD.Print("");
				EmitSignal( SignalName.Transitioned, this , "IdleState" );
			}
		}
	}
}
