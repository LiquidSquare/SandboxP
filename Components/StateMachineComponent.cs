using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachineComponent : Node3D
{
	[Export]
	public State InitialState;
	public State CurrentState;
	public Dictionary<string, State> States = new Dictionary<string, State>();
	//TODO
	public Label3D Label;
	
	public override void _Ready()
	{
		//TODO
		Label = GetNode<Label3D>("%Label3D");
		foreach (State state in GetChildren())
		{
			if ( state is State ) {
				States.Add( state.Name, (State)state );
				GD.Print(state.Name);
				state.Transitioned += OnStateTransition;
			}
		}
		if (InitialState!= null)
		{
			OnStateTransition(null, InitialState.Name);
		}
		
	}

    public override void _Input(InputEvent @event)
    {
        if ( CurrentState != null )
			CurrentState.Call("UserInput", @event);
    }

	public override void _Process(double delta)
	{
		if ( CurrentState != null ) 
			CurrentState.Call("Update", delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		if ( CurrentState != null ) 
			CurrentState.Call("PhysicsUpdate",delta);
	}

	public void OnStateTransition( State state, string newStateName )
	{
		var newState = States[newStateName];

		// if ( state != CurrentState ) return;
		
		if ( newState == null ) return;

		if ( CurrentState != null ) CurrentState.Exit();

		newState.Enter();

		CurrentState = newState;

		//TODO
		Label.Text = $"{CurrentState.Name}";
	}
}
