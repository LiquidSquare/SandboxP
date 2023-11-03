using Godot;
using System;

public partial class CrouchIdleState : State
{

	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event)
	{
		if(@event is InputEventKey)
		{
			if (Input.IsActionPressed("crouch"))
				EmitSignal( SignalName.Transitioned, this , "IdleState" );
			if (isHoldingAnyMovementInput())
				EmitSignal( SignalName.Transitioned, this , "CrouchWalkingState" );
		}
	}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}
}
