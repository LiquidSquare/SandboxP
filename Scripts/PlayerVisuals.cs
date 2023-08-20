using Godot;
using System;

public partial class PlayerVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;

	//Nodes References
	CharacterBody3D Player;
	AnimationTree AnimationTree;

	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>("../");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _Process(double delta)
	{
		if ( (Vector3)Player.Get("Direction") != Vector3.Zero )
		{
			newVisualsRotation = new Vector3()
			{
				X = Rotation.X,
				Y = Mathf.LerpAngle(Rotation.Y, Mathf.Atan2(Player.Velocity.X, Player.Velocity.Z), 0.15f),
				Z = Rotation.Z
			};
		}
		
		Rotation = newVisualsRotation;
    	
		AnimationTree.Set("parameters/BlendSpace1D/blend_position", Player.Velocity.Length() / (float)Player.Get("Speed") );

	}
}
