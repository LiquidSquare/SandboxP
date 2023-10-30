using Godot;
using System;

public partial class WalkingState : State
{
	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if (
			Input.IsActionJustReleased("up")   ||
			Input.IsActionJustReleased("down") ||
			Input.IsActionJustReleased("left") ||
			Input.IsActionJustReleased("right")
		)
			EmitSignal( SignalName.Transitioned, this , "IdleState" );
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}
}
