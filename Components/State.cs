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
}
