using Godot;
using System;

public partial class PlayerVisuals : Node3D
{
	//Inner Variables
	Vector3 newVisualsRotation;

	//Nodes References
	CharacterBody3D Player;
	AnimationTree AnimationTree;
	Skeleton3D Skeleton;

	float HorizontalTargetAngle = 0.0f;
	float VerticalTargetAngle = 0.0f;
	float RotationSpeed = 10f;
	Node3D TargetNode;

	public override void _Ready()
	{
		Player = GetNode<CharacterBody3D>("../");
		Skeleton = GetNode<Skeleton3D>("%Skeleton3D");
		AnimationTree = GetNode<AnimationTree>("AnimationTree");
		TargetNode = GetParent().GetParent().GetNode<Node3D>("%Enemy");
	}

	public override void _Process(double delta)
	{
		if ( (Vector3)Player.Get("Direction") != Vector3.Zero )
		{
			newVisualsRotation = new Vector3()
			{
				X = Rotation.X,
				Y = Mathf.Atan2(Player.Velocity.X, Player.Velocity.Z) + Mathf.Pi,
				Z = Rotation.Z
			};
		}
		
		Rotation = newVisualsRotation;
    	
		AnimationTree.Set("parameters/BlendSpace1D/blend_position", Player.Velocity.Length() / (float)Player.Get("Speed") );

		HorizontalTargetAngle = Mathf.Atan2( TargetNode.GlobalPosition.Z - Player.GlobalPosition.Z, Player.GlobalPosition.X - TargetNode.GlobalPosition.X );

		Skeleton.SetBonePoseRotation(5, new Quaternion(Vector3.Up, HorizontalTargetAngle + Mathf.Pi/2 - Rotation.Y));

	}
}
