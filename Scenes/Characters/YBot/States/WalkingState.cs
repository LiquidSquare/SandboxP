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
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}

	private bool releasedMovementInput() => 
			Input.IsActionJustReleased("up")   ||
			Input.IsActionJustReleased("down") ||
			Input.IsActionJustReleased("left") ||
			Input.IsActionJustReleased("right");
	
	private bool isHoldingAnyMovementInput() => 
			Input.IsActionPressed("up")   ||
			Input.IsActionPressed("down") ||
			Input.IsActionPressed("left") ||
			Input.IsActionPressed("right");
}
