using Godot;
using System;

//TODO: mudar para interface
public partial class State : Node3D
{
	[Signal]
	public delegate void TransitionedEventHandler(State state, string newStateName);

	public void Enter(){}

	public void Exit (){}

	public void UserInput(InputEvent @event){}

	public void Update(double delta){}

	public void PhysicsUpdate(double delta){}

	//TODO
	public bool releasedMovementInput() => 
			Input.IsActionJustReleased("up")   ||
			Input.IsActionJustReleased("down") ||
			Input.IsActionJustReleased("left") ||
			Input.IsActionJustReleased("right");
	
	public bool isHoldingAnyMovementInput() => 
			Input.IsActionPressed("up")   ||
			Input.IsActionPressed("down") ||
			Input.IsActionPressed("left") ||
			Input.IsActionPressed("right");
}
